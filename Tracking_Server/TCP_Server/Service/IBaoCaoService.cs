using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCP_Server.Models.Api;

namespace TCP_Server.Service
{
    public interface IBaoCaoService
    {
        // Bao cao hanh trinh tu from den to
        List<HanhTrinhRes> GetHanhtrinh(string serial, DateTime from, DateTime to);

        // Bao cao dung do
        List<DungDoRes> GetDungdo(string serial, DateTime from, DateTime to);
    }
}
