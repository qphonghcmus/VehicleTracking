using DaoDatabase;
using DaoDatabase.AutoMapping.Enums;
using DaoDatabase.AutoMapping.MapAtribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCP_Server.Models
{
    public class H1_Data : IEntity
    {
        [PrimaryKey(KeyGenerateType = KeyGenerateType.Auto)]
        public virtual long Id { get; set; }

        [BasicColumn(IsIndex = true)]
        public virtual string Serial { get; set; }

        [BasicColumn]
        public virtual string MaUID { get; set; }

        [BasicColumn]
        public virtual string GiayPhepLaiXe { get; set; }

        [BasicColumn]
        public virtual float VanTocXe { get; set; }

        [BasicColumn]
        public virtual DateTime Datetime { get; set; }

        public H1_Data()
        {
        }

        public H1_Data(string serial, string maUID, string giayPhepLaiXe, float vanTocXe, DateTime datetime)
        {
            Serial = serial;
            MaUID = maUID;
            GiayPhepLaiXe = giayPhepLaiXe;
            VanTocXe = vanTocXe;
            Datetime = datetime;
        }

        public virtual void FixNullObject()
        {
        }
    }
}