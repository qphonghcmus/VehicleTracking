using DaoDatabase;
using DaoDatabase.AutoMapping.Enums;
using DaoDatabase.AutoMapping.MapAtribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCP_Server.Models
{
    public class S1_Data : IEntity
    {
        [PrimaryKey(KeyGenerateType = KeyGenerateType.Auto)]
        public virtual long Id { get; set; }

        [BasicColumn(IsIndex = true)]
        public virtual string Serial { get; set; }

        [BasicColumn]
        public virtual byte TrangThai { get; set; }

        [BasicColumn]
        public virtual float DienApBinh { get; set; }

        [BasicColumn]
        public virtual float DienApPin { get; set; }

        [BasicColumn]
        public virtual float CuongDoGSM { get; set; }

        [BasicColumn]
        public virtual string LoiTheNho { get; set; }

        [BasicColumn]
        public virtual DateTime Datetime { get; set; }

        public S1_Data()
        {
        }

        public S1_Data(string serial, byte trangThai, float dienApBinh, float dienApPin, float cuongDoGSM, string loiTheNho, DateTime datetime)
        {
            Serial = serial;
            TrangThai = trangThai;
            DienApBinh = dienApBinh;
            DienApPin = dienApPin;
            CuongDoGSM = cuongDoGSM;
            LoiTheNho = loiTheNho;
            Datetime = datetime;
        }

        public virtual void FixNullObject()
        {
            
        }
    }
}