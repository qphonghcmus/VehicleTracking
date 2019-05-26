using DaoDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TCP_Server.Models;
using TCP_Server.Models.Api;
using TCP_Server.Server;

namespace TCP_Server.Service
{
    public class BaoCaoService : IBaoCaoService
    {
        Reponsitory DbContext = UnitOfWorkFactory.GetUnitOfWork(GlobalVar.configObject.SqlDbName, DbSupportType.MicrosoftSqlServer);

        public List<DungDoRes> GetDungdo(string serial, DateTime from, DateTime to)
        {
            var gTable = DbContext.GetWhere<G_Data>(m => (m.Serial == serial)).Where(
                m => DateTime.Compare(m.Datetime, from) >= 0 && (DateTime.Compare(m.Datetime, to) <= 0)).OrderBy(m => m.Datetime);

            var res = new List<DungDoRes>();

            bool check = false;
            DungDoRes temp = null;
            DateTime beginStop = new DateTime();
            foreach (var tmp in gTable)
            {

                if (tmp.VanToc == 0.0)
                {
                    if (check == false)
                    {
                        beginStop = tmp.Datetime;
                        temp = new DungDoRes(tmp.KinhDo, tmp.ViDo, tmp.Datetime, "");
                        check = true;
                    }
                }
                else
                {
                    if (check)
                    {
                        var timeStop = tmp.Datetime.Subtract(beginStop).ToString("hh\\:mm\\:ss");
                        temp.ThoiGian = timeStop;

                        res.Add(temp);
                        check = false;
                        temp = null;
                    }
                }
            }

            return res;
        }

        public List<HanhTrinhRes> GetHanhtrinh(string serial, DateTime from, DateTime to)
        {
            var gTable = DbContext.GetWhere<G_Data>(m => (m.Serial == serial)).Where(
                m => DateTime.Compare(m.Datetime, from) >= 0 && (DateTime.Compare(m.Datetime, to) <= 0));
            var s1Table = DbContext.GetWhere<S1_Data>(m => m.Serial == serial).Where(
                m => DateTime.Compare(m.Datetime, from) >= 0 && DateTime.Compare(m.Datetime, to) <= 0);

            var list = gTable.Join(s1Table, g => g.Datetime, s1 => s1.Datetime,
                (gtable, s1table) => new { vido = gtable.ViDo, kinhdo = gtable.KinhDo, vantoc = gtable.VanToc,
                    statusgps = gtable.Status, statusgsm = s1table.TrangThai, datime = gtable.Datetime });

            var res = new List<HanhTrinhRes>();
            foreach (var tmp in list)
            {
                res.Add(new HanhTrinhRes(tmp.vido, tmp.kinhdo, tmp.vantoc, tmp.statusgps, tmp.statusgsm, tmp.datime));
            }
            return res;
        }
    }
}