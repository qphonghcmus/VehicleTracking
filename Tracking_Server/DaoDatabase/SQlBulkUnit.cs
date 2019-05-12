using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using DaoDatabase.AutoMapping.MapAtribute;
using FluentNHibernate.Utils;
using NHibernate;

namespace DaoDatabase
{
    public class SQlBulkUnit : Reponsitory
    {
        private readonly IStatelessSession _session;

        private static IDictionary<Type, IDictionary<string, PlatPropInfo>> _cacheEntity =
            new Dictionary<Type, IDictionary<string, PlatPropInfo>>();
        public SQlBulkUnit(IStatelessSession session)
        {
            _session = session;
        }

        public override void Dispose()
        {
            if (_session != null)
            {
                _session.Close();
                _session.Dispose();
            }
        }

        private void Refresh()
        {
            if (_session != null && !_session.IsOpen) _session.Connection.Open();
            LastTimeUsing = DateTime.Now;
        }

        public override void Commit()
        {
            Refresh();
            lock (this) /*lock đối tượng lại không cho các thread khác truy cập*/
            {
                var ssTransaction = _session.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    _session.SetBatchSize(1000);
                    ssTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Commit Session Fail : {0}", ex);
                    ssTransaction.Rollback();

                    throw ex;
                }
                finally
                {
                    ssTransaction.Dispose();
                }
            }

            LastTimeUsing = DateTime.Now;
        }

        public override void Insert<T>(T obj)
        {
            throw new NotImplementedException();
        }


        private void PlatColumn(Type type, IDictionary<string, PlatPropInfo> pNames,string index="")
        {
            foreach (var p in type.GetProperties())
                if (p.GetMethod.IsVirtual)
                {
                    var c = p.GetCustomAttribute<BasicColumnAttribute>();
                    if (c != null)
                    {
                        var t = new DataColumn(string.IsNullOrEmpty(c.Name) ? p.Name + index : c.Name + index,
                            p.PropertyType == typeof(TimeSpan) ? typeof(long) : p.PropertyType);
                        pNames.Add(t.ColumnName,
                            new PlatPropInfo() {IsPlat = false, Name = t.ColumnName, Property = p, Column = t});
                    }

                    var k = p.GetCustomAttribute<PrimaryKeyAttribute>();
                    if (k != null)
                    {
                        var t = new DataColumn(string.IsNullOrEmpty(k.Name) ? p.Name + index : k.Name + index, p.PropertyType);
                        pNames.Add(t.ColumnName,
                            new PlatPropInfo() { IsPlat = false, Name = t.ColumnName, Property = p, Column = t });
                    }

                    var cc = p.GetCustomAttribute<ComponentColumnAttribute>();
                    if (cc != null)
                    {
                        pNames.Add(p.Name,
                            new PlatPropInfo() { IsPlat = true, Name = p.Name, Property = p,Props = new Dictionary<string, PlatPropInfo>()});
                        PlatColumn(p.PropertyType, pNames[p.Name].Props, cc.Index <= 0 ? index+"" : index+cc.Index);
                    }
                }
        }

        class PlatPropInfo
        {
            public IDictionary<string,PlatPropInfo> Props { get; set; }=new Dictionary<string, PlatPropInfo>();
            public string Name { get; set; }
            public PropertyInfo Property { get; set; }
            public bool IsPlat { get; set; }
            public DataColumn Column { get; set; }
        }
        private void ReadValue(DataRow r, object val,IDictionary<string, PlatPropInfo> props)
        {
            if(val==null) return;
            foreach (var prop in props)
            {
                var tmpVal = prop.Value.Property.GetValue(val);
                if (prop.Value.IsPlat)
                {
                    ReadValue(r, tmpVal, prop.Value.Props);
                }
                else
                {
                    // nếu giá trị và timespan thì đổi qua long
                    if (tmpVal is TimeSpan)
                    {
                        r[prop.Value.Name] = (tmpVal is TimeSpan ? (TimeSpan) tmpVal : new TimeSpan()).Ticks;
                    }
                    else
                        r[prop.Value.Name] = tmpVal;
                }

               
            }
        }

        private void CreateColumnTable(DataTable tmp, IDictionary<string, PlatPropInfo> pNames)
        {
            foreach (var p in pNames)
            {
                if (!p.Value.IsPlat)
                    tmp.Columns.Add(new DataColumn(p.Value.Column.ColumnName, p.Value.Column.DataType));
                else
                {
                    CreateColumnTable(tmp, p.Value.Props);
                }
            }
        }
        /// <summary>
        /// hàm nãy insert duy nhất 1 loại dữ liệu
        /// </summary>
        /// <param name="objs"></param>
        private void InsertIndentType(IList<object> objs)
        {

            using (var tmp = new DataTable())
            {
                var type = objs.FirstOrDefault()?.GetType();
                if (type == null) return;
                // build các column
                IDictionary<string, PlatPropInfo> pNames = new Dictionary<string, PlatPropInfo>();

                if (_cacheEntity.ContainsKey(type))
                    pNames = _cacheEntity[type];
                else
                {
                    PlatColumn(type, pNames);
                    _cacheEntity.Add(type, pNames);
                }
                // create column
                CreateColumnTable(tmp, pNames);
                foreach (var m in objs)
                {
                    var r = tmp.NewRow();
                    ReadValue(r, m, pNames);
                    tmp.Rows.Add(r);
                }

                using (var bulkcopy = new SqlBulkCopy(_session.Connection as SqlConnection))
                {
                    var t = type.GetCustomAttribute<TableAttribute>();
                    string name;
                    if (t != null)
                        name = string.IsNullOrEmpty(t.Name) ? type.Name : t.Name;
                    else
                        name = type.Name;
                    bulkcopy.DestinationTableName = name;
                    bulkcopy.BulkCopyTimeout = 50000;
                    foreach (DataColumn c in tmp.Columns)
                    {

                        bulkcopy.ColumnMappings.Add(c.ColumnName, c.ColumnName);
                    }
                    try
                    {
                        var st = new Stopwatch();
                        st.Start();
                        bulkcopy.WriteToServer(tmp);
                        st.Stop();
                        Debug.WriteLine($"batch insert {type} : {objs.Count} records {st.ElapsedMilliseconds} ms");
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
        public override void InsertAll<T>(ICollection<T> objs)
        {
            Refresh();

            // phân loại type
            foreach (var obj in objs.GroupBy(m=>m.GetType()))
            {
                var tmp=new List<object>();
                using (var e = obj.GetEnumerator())
                {
                    while (e.MoveNext())
                    {
                        tmp.Add(e.Current);
                    }

                    if (tmp.Count > 0)
                    {
                        InsertIndentType(tmp);
                    }
                }
            }
        }

        public override void Delete<T>(T obj)
        {
            throw new NotImplementedException();
        }

        public override T Update<T>(T obj)
        {
            throw new NotImplementedException();
        }

        public override bool Update<T>(T obj, params Expression<Func<T, object>>[] poperties)
        {
            throw new NotImplementedException();
        }

        public override void InsertOrUpdate<T>(T obj)
        {
            throw new NotImplementedException();
        }

        public override T Get<T>(object key)
        {
            throw new NotImplementedException();
        }

        public override ICollection<T> GetWhere<T>(Expression<Func<T, bool>> expression,
            params Expression<Func<T, object>>[] fetch)
        {
            throw new NotImplementedException();
        }

        public override ICollection<T> GetWhere<T>(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public override ICollection<T> GetWhereSpecial<T>(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public override IDictionary<Tkey, T> GetAll<Tkey, T>(Func<T, Tkey> key)
        {
            throw new NotImplementedException();
        }

        public override void Clear()
        {
        }

        public override void CustomHandle<TSql>(Action<TSql> action)
        {
            throw new NotImplementedException();
        }

        public override IList<T> TakeDest<T>(Expression<Func<T, bool>> expression, int take,
            Expression<Func<T, object>> order = null)
        {
            throw new NotImplementedException();
        }

        public override IList<T> TakeAsc<T>(Expression<Func<T, bool>> expression, int take,
            Expression<Func<T, object>> order = null)
        {
            throw new NotImplementedException();
        }

        public override IList<T> TakeDestMinimum<T>(Expression<Func<T, bool>> expression, int take,
            Expression<Func<T, object>> order, params Expression<Func<T, object>>[] propertiSelect)
        {
            throw new NotImplementedException();
        }

        public override IReponsitoryQuery<T> CreateQuery<T>()
        {
            throw new NotImplementedException();
        }
    }
}