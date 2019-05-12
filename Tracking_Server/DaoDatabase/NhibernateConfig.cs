using System;
using System.Collections.Generic;
using FluentNHibernate.Cfg.Db;

namespace DaoDatabase
{
    public class NhibernateConfig:DatabaseConfig
    {
        public IPersistenceConfigurer Config { set; get; }
        public ICollection<Type> Maps { get; set; }
        /// <summary>
        /// tạo mới toàn bộ bảng dữ liệu
        /// </summary>
        public bool IsCreateNew { get; set; }
        /// <summary>
        /// Cập nhật lại bảng dữ liệu khi có sự thay đổi
        /// </summary>
        public bool IsUpdateTable { get; set; }
    }
}