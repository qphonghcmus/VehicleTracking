#region header

// /*********************************************************************************************/
// Project :DaoDatabase
// FileName : NhibernateFactory.cs
// Time Create : 2:00 PM 28/12/2015
// Author:  Văn Luật (vanluat1992@gmail.com)
// /********************************************************************************************/

#endregion

using System;
using DaoDatabase.Core;
using FluentNHibernate.Cfg;
using FluentNHibernate.Utils;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace DaoDatabase
{
    public class NhibernateFactory : IDbFactory
    {
        private readonly ISessionFactory _factory;

        public NhibernateFactory(NhibernateConfig cfg)
        {
            try
            {
                _factory =
                    Fluently.Configure()
                        .Database(cfg.Config)
                        .Mappings(
                            m =>
                                 cfg.Maps.Each(x => m.FluentMappings.Add(x)
                                    /*.Conventions.Setup(c =>c.Add(DefaultCascade.All()))*/))
                        //.Cache(
                        //    m =>
                        //    m.UseQueryCache()
                        //        .ProviderClass("NHibernate.Caches.SysCache.SysCacheProvider, NHibernate.Caches.SysCache"))
                        .ExposeConfiguration(
                            m =>
                            {
                                try
                                {
                                    if (!cfg.IsCreateNew)
                                    {
                                        new SchemaUpdate(m).Execute(false, cfg.IsUpdateTable);
                                    }
                                    else
                                    {
                                        //try
                                        //{
                                        //    var schema = new SchemaExport(m);
                                        //    schema.Drop(true, true);

                                        //    schema.Create(true, true);
                                        //}
                                        //catch (Exception ex)
                                        //{
                                        //    Console.WriteLine(ex);
                                        //}
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Tạo database lỗi : {0}", e);
                                }
                            }).BuildConfiguration().BuildSessionFactory();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _factory.Dispose();
        }

        public Reponsitory CreateContext(string name="")
        {
            //_factory.OpenStatelessSession();
            return new NhibernateUnit(_factory.OpenSession(new NoLockInterceptor()));
        }

        public Reponsitory CreateBulkContext(string name = "")
        {
            return new SQlBulkUnit(_factory.OpenStatelessSession());
        }
        #endregion
    }
}