using DaoDatabase;
using DaoDatabase.AutoMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using TCP_Server.Models;
using TCP_Server.Server;

namespace TCP_Server.Core
{
    public class DataManager
    {
        //private GlobalVar _global;
        private string _sqlServer;
        private string _sqlDbName;
        private string _sqlUser;
        private string _sqlPwd;

        private readonly CancellationTokenSource _cancelSyncAccount = new CancellationTokenSource();
        private Task _taskSyncAccount;

        public DataManager()
        {
            //_global = global;

            _sqlServer = GlobalVar.configObject.SqlServer;
            _sqlDbName = GlobalVar.configObject.SqlDbName;
            _sqlUser = GlobalVar.configObject.SqlUser;
            _sqlPwd = GlobalVar.configObject.SqlPwd;

            var mapEntity = Assembly.GetAssembly(typeof(RawData))
                    .GetTypes()
                    .Where(m => m.GetInterfaces().FirstOrDefault(x => x == typeof(IEntity)) != null && m.IsClass)
                    .Select(m =>
                    {
                        var makeme = typeof(FactoryMap<>).MakeGenericType(m);
                        return makeme;
                    }).ToList();

            UnitOfWorkFactory.RegisterDatabase(_sqlDbName, new NhibernateConfig
            {
                Maps = mapEntity,
                Config =
                    DatabaseConfigFactory.GetDataConfig(false, _sqlServer, 0, _sqlDbName,
                        _sqlUser, _sqlPwd, false, null),
                IsUpdateTable = true
            });


            // chạy task đồng bộ dữ liệu account lên memory
            //_taskSyncAccount = new Task(SyncAccount, _cancelSyncAccount.Token);
            //_taskSyncAccount.Start();
        }

        private async void SyncAccount()
        {
            while (true)
            {
                if (_cancelSyncAccount.IsCancellationRequested)
                    return;
                try
                {
                    using (var ctx = GetContext())
                    {
                        var accs = ctx.CreateQuery<RawData>().Where(m => m.Data != "").Execute();
                        foreach (var acc in accs)
                        {

                            //if (_global.AllAccounts.ContainsKey(acc.Data))
                            //{
                            //    _global.AllAccounts[acc.Data].Data = acc.Data;
                            //    _global.AllAccounts[acc.Data].Datetime = acc.Datetime;
                            //}
                            //else
                            //{
                            //    _global.AllAccounts.TryAdd(acc.Data, acc);
                            //}
                        }
                    }
                }
                catch (Exception)
                {

                }
                await Task.Delay(10000);
            }
        }

        private Reponsitory GetContext()
        {
            return UnitOfWorkFactory.GetUnitOfWork(_sqlDbName, DbSupportType.MicrosoftSqlServer);
        }


        public void Dispose()
        {
            _cancelSyncAccount.Cancel();
        }

    }
}