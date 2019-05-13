using DaoDatabase;
using DaoDatabase.AutoMapping.Enums;
using DaoDatabase.AutoMapping.MapAtribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCP_Server.Models
{
    public class RawData : IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [PrimaryKey(KeyGenerateType = KeyGenerateType.Auto)] // Khoa chinh
        public virtual long Id { get; set; }

        /// <summary>
        /// ID 
        /// </summary>
        [BasicColumn(IsIndex = true)]
        public virtual string Serial { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        [BasicColumn]
        public virtual string Data { get; set; }

        [BasicColumn]
        public virtual DateTime Datetime { get; set; }

        public RawData()
        {
        }

        public RawData(string serial, string data, DateTime datetime)
        {
            Serial = serial;
            Data = data;
            Datetime = datetime;
        }


        public virtual void FixNullObject()
        {
        }
    }
}