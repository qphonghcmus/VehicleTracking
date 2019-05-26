using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCP_Server.Models.Api
{
    public class DungDoRes
    {
        public string ToaDo { get; set; }
        public DateTime ThoiDiem { get; set; }
        public string ThoiGian { get; set; }

        public DungDoRes(string kinhDo, string viDo, DateTime thoiDiem, string thoiGian)
        {
            ToaDo = kinhDo + " ; " + viDo;
            ThoiDiem = thoiDiem;
            ThoiGian = thoiGian;
        }
    }
}