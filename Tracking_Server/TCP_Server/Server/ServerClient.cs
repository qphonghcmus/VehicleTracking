using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TCP_Server.Server
{
    /// <summary>
    /// Object represents a client that connect to server
    /// </summary>
    public class ServerClient : IDisposable
    {
        #region properties

        private readonly IScsServerClient _client;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Constructor

        public ServerClient(IScsServerClient client)
        {
            _client = client;

            // Registered event when client send message to server and client disconected
            client.MessageReceived += Client_MessageReceived;
            client.Disconnected += Client_Disconnected;
        }

        #endregion

        #region Private methods

        private void Client_Disconnected(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void Client_MessageReceived(object sender, Hik.Communication.Scs.Communication.Messages.MessageEventArgs e)
        {
            // log
            log.Info("Receiving message from client");
            try
            {
                // Message used to send or received in StreamWireProtol is ScsRawDataMessage
                var msg = e.Message as ScsRawDataMessage;
                if (msg == null) return;

                // Convert byte array to string
                var data = Encoding.ASCII.GetString(msg.MessageData);

                // Response to Remote App
                //var rep = "\r\nOK!\r\n";
                //_client.SendMessage(new ScsRawDataMessage(Encoding.ASCII.GetBytes(rep)));

                // Handle Package
                HandlePackage(data);

                log.Info("Replying message to client successfully");

            }
            catch(Exception ex)
            {
                log.Error("Replying message failed", ex);
            }
        }

        private void SendPackageToClient(byte[] rep)
        {
            _client.SendMessage(new ScsRawDataMessage(rep));

            log.Info("Replying message to client successfully");
        }

        /// <summary>
        /// Xu ly goi dư lieu nhan duoc tu Client
        /// </summary>
        /// <param name="data"></param>
        private void HandlePackage(string data)
        {
            // Save Rawdata
            SaveRawData(data);

            // Reply to client
            SendPackageToClient(Encoding.ASCII.GetBytes("r\nOK!\r\n"));


            // Split data and Save
            SaveSplittedData(data);
        }

        // Spli and Save data
        private static void SaveSplittedData(string data)
        {
            try
            {

                string[] dataArray = data.Split('&');

                // Khong du 4 truong bat buoc
                // Khi split('&') luon co phan tu dau tien rong "" trong mang dataArray
                if(dataArray.Length < 5)
                {
                    return;
                }

                DateTime dateTime = DateTime.Now;
                string connectionString = TCP_Server_Program._connectionString;
                var dataprovider = new DataProvider(connectionString);
                string sqlCommand = "";

                // &I
                string[] stringI = dataArray[1].Trim().Substring(2).Split(';');
                string idcard = stringI[0].Trim();
                string serial = stringI[1].Trim();
                sqlCommand = string.Format("INSERT INTO I(ID,Serial,Datetime) VALUES('{0}','{1}','{2}')", idcard, serial, dateTime);
                dataprovider.ExecuteNonQuery(sqlCommand);

                // &G
                string[] stringG = dataArray[2].Trim().Substring(2).Split(';');
                string status = stringG[0].Trim();
                string vido = stringG[1].Trim();
                string kinhdo = stringG[2].Trim();
                string vantocgps = stringG[3].Trim();
                string khoangcachgps = stringG[4].Trim();
                string tongkhoangcach = stringG[5].Trim();
                sqlCommand = string.Format("INSERT INTO G(Status,Vido,Kinhdo,Vantoc,Khoangcach,TongKhoangcach,Datetime) " +
                    "VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", status, vido, kinhdo, vantocgps, khoangcachgps, tongkhoangcach, dateTime);
                dataprovider.ExecuteNonQuery(sqlCommand);

                // &S1
                string[] stringS1 = dataArray[3].Trim().Substring(3).Split(';');
                string trangthai = stringS1[0].Trim();
                string dienapbinh = stringS1[1].Trim();
                string dienappin = stringS1[2].Trim();
                string cuongdoGSM = stringS1[3].Trim();
                string loithenho = stringS1[4].Trim();
                sqlCommand = string.Format("INSERT INTO S1(Trangthai,Dienapbinh,Dienappin,CuongdoGSM,Loithenho,Datetime) " +
                    "VALUES('{0}','{1}','{2}','{3}','{4}','{5}')", trangthai, dienapbinh, dienappin, cuongdoGSM, loithenho, dateTime);
                dataprovider.ExecuteNonQuery(sqlCommand);

                // &D
                string[] stringD = dataArray[4].Trim().Substring(2).Split(';');
                string cuocxe = stringD[0].Trim();
                string thoigian = stringD[1].Trim();
                sqlCommand = string.Format("INSERT INTO D(IDCuocxe,Thoigian,Datetime) " +
                    "VALUES('{0}','{1}','{2}')", cuocxe, thoigian, dateTime);
                dataprovider.ExecuteNonQuery(sqlCommand);

                // &H1
                if (data.IndexOf("&H1") != -1)
                {
                    string[] stringH1 = dataArray[5].Trim().Substring(3).Split(';');
                    string maUID = stringH1[0].Trim();
                    string giaypheplaixe = stringH1[1].Trim();
                    string vantocxe = stringH1[2].Trim();
                    sqlCommand = string.Format("INSERT INTO H1(MaUID,Giaypheplaixe,Vantocxe,Datetime) " +
                        "VALUES('{0}','{1}','{2}','{3}')", maUID, giaypheplaixe, vantocxe, dateTime);
                    dataprovider.ExecuteNonQuery(sqlCommand);
                }

                // &CT
                if (data.IndexOf("&CT") != -1)
                {
                    string[] stringCT = dataArray[6].Trim().Substring(3).Split(';');
                    string from = stringCT[0].Trim();
                    string sdt = stringCT[1].Trim();
                    string malenh = stringCT[2].Trim();
                    string okerror = stringCT[3].Trim();
                    sqlCommand = string.Format("INSERT INTO CT(_From,SDT,Malenh,[OK/ERROR],Datetime) " +
                        "VALUES('{0}','{1}','{2}','{3}','{4}')", from, sdt, malenh, okerror, dateTime);
                    dataprovider.ExecuteNonQuery(sqlCommand);
                }

                log.Info("Save data to database");
            }
            catch (Exception e)
            {
                log.Error("Save data failed ", e);
            }
        }

        // Save Raw data
        private static void SaveRawData(string data)
        {
            DateTime dateTime = DateTime.Now;

            string connectionString = TCP_Server_Program._connectionString;
            string sql = string.Format("INSERT INTO RawData(RawData,DateTime) VALUES('{0}','{1}')", data, dateTime);
            var dataprovider = new DataProvider(connectionString);
            dataprovider.ExecuteNonQuery(sql);
            log.Info("Save Raw data to database");
        }

        #endregion

        #region Implement IDisposable

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}