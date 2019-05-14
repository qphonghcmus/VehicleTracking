using DaoDatabase;
using DaoDatabase.AutoMapping.Enums;
using DaoDatabase.AutoMapping.MapAtribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCP_Server.Models
{
    public class CT_Data : IEntity
    {
        [PrimaryKey(KeyGenerateType = KeyGenerateType.Auto)]
        public virtual long Id { get; set; }

        [BasicColumn(IsIndex = true)]
        public virtual string Serial { get; set; }

        [BasicColumn]
        public virtual string _From { get; set; }

        [BasicColumn]
        public virtual string SDT { get; set; }

        [BasicColumn]
        public virtual int MaLenh { get; set; }

        [BasicColumn]
        public virtual DateTime Datetime { get; set; }

        public CT_Data()
        {
        }

        public CT_Data(string serial, string from, string sDT, int maLenh, DateTime datetime)
        {
            Serial = serial;
            _From = from;
            SDT = sDT;
            MaLenh = maLenh;
            Datetime = datetime;
        }

        public virtual void FixNullObject()
        {
        }
    }
}