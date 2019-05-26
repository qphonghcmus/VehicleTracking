using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TCP_Server.Models.Api;
using TCP_Server.Service;

namespace TCP_Server.Controllers
{
    public class ReportController : ApiController
    {
        private IBaoCaoService baoCaoService = new BaoCaoService();

        [HttpGet]
        [ActionName("GetHanhTrinh")]
        public IEnumerable<HanhTrinhRes> GetHanhTrinh(string serial, string beginTime, string endTime)
        {
            DateTime from, to;
            if (DateTime.TryParse(beginTime.Replace('-', ' '), out from) && DateTime.TryParse(endTime.Replace('-', ' '), out to))
            {
                return baoCaoService.GetHanhtrinh(serial, from, to);
            }
            return null;

        }

        // Bao cao dung do
        [HttpGet]
        [ActionName("GetDungDo")]
        public IEnumerable<DungDoRes> GetDungDo(string serial, string beginTime, string endTime)
        {
            DateTime from, to;
            if (DateTime.TryParse(beginTime.Replace('-', ' '), out from) && DateTime.TryParse(endTime.Replace('-', ' '), out to))
            {
                return baoCaoService.GetDungdo(serial, from, to);
            }
            return null;
        }
    }
}
