using DaoDatabase;
using DaoDatabase.AutoMapping.Enums;
using DaoDatabase.AutoMapping.MapAtribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCP_Server.Models
{
    public class G_Data : IEntity
    {

        [PrimaryKey(KeyGenerateType = KeyGenerateType.Auto)] 
        public virtual long Id { get; set; }

        [BasicColumn(IsIndex = true)]
        public virtual string Serial { get; set; }

        [BasicColumn]
        public virtual bool Status { get; set; }

        [BasicColumn]
        public virtual string ViDo { get; set; }

        [BasicColumn]
        public virtual string KinhDo { get; set; }

        [BasicColumn]
        public virtual float VanToc { get; set; }

        [BasicColumn]
        public virtual long KhoangCach { get; set; }

        [BasicColumn]
        public virtual long TongKhoangCach { get; set; }

        [BasicColumn]
        public virtual DateTime Datetime { get; set; }

        public G_Data()
        {
        }

        public G_Data(string serial, bool status, string viDo, string kinhDo, float vanToc, long khoangCach, long tongKhoangCach, DateTime datetime)
        {
            Serial = serial;
            Status = status;
            ViDo = viDo;
            KinhDo = kinhDo;
            VanToc = vanToc;
            KhoangCach = khoangCach;
            TongKhoangCach = tongKhoangCach;
            Datetime = datetime;
        }

        public virtual void FixNullObject()
        {

        }
    }
}