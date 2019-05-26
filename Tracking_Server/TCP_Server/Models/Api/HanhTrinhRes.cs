using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCP_Server.Models.Api
{
    public class HanhTrinhRes
    {
        public string ToaDo { get; set; }
        public float VanToc { get; set; }
        public bool TrangThaiGPS { get; set; }
        public byte TrangThaiGSM { get; set; }
        public DateTime Datetime { get; set; }

        public HanhTrinhRes(string toaDo, float vanToc, bool trangThaiGPS, byte trangThaiGSM, DateTime datetime)
        {
            ToaDo = toaDo;
            VanToc = vanToc;
            TrangThaiGPS = trangThaiGPS;
            TrangThaiGSM = trangThaiGSM;
            Datetime = datetime;
        }

        public HanhTrinhRes(string viDo, string kinhDo, float vanToc, bool trangThaiGPS, byte trangThaiGSM, DateTime datetime)
        {
            ToaDo = viDo + " ; " + kinhDo;
            VanToc = vanToc;
            TrangThaiGPS = trangThaiGPS;
            TrangThaiGSM = trangThaiGSM;
            Datetime = datetime;
        }
    }
}