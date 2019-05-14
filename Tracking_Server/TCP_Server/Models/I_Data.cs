using DaoDatabase;
using DaoDatabase.AutoMapping.Enums;
using DaoDatabase.AutoMapping.MapAtribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCP_Server.Models
{
    public class I_Data : IEntity
    {
        [PrimaryKey(KeyGenerateType = KeyGenerateType.Auto)]
        public virtual long Id { get; set; }

        [BasicColumn(IsIndex = true)]
        public virtual string Serial { get; set; }

        [BasicColumn]
        public virtual string IDCard { get; set; }

        [BasicColumn]
        public virtual DateTime Datetime { get; set; }

        public I_Data()
        {
        }

        public I_Data(string serial, string iDCard, DateTime datetime)
        {
            Serial = serial;
            IDCard = iDCard;
            Datetime = datetime;
        }

        public virtual void FixNullObject()
        {
        }
    }
}