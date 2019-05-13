using DaoDatabase;
using DaoDatabase.AutoMapping.Enums;
using DaoDatabase.AutoMapping.MapAtribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCP_Server.Models
{
    public class D_Data : IEntity
    {
        [PrimaryKey(KeyGenerateType = KeyGenerateType.Auto)]
        public virtual long Id { get; set; }

        [BasicColumn(IsIndex = true)]
        public virtual string Serial { get; set; }

        [BasicColumn]
        public virtual string IDCuocXe { get; set; }

        [BasicColumn]
        public virtual int ThoiGianRecord { get; set; }

        [BasicColumn]
        public virtual DateTime Datetime { get; set; }

        public D_Data(string serial, string iDCuocXe, int thoiGianRecord, DateTime datetime)
        {
            Serial = serial;
            IDCuocXe = iDCuocXe;
            ThoiGianRecord = thoiGianRecord;
            Datetime = datetime;
        }

        public virtual void FixNullObject()
        {
        }
    }
}