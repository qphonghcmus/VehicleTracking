using DaoDatabase;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TCP_Server.Models;

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

        // Doi tuong dai dien cho database duoc goi ra de su dung
        Reponsitory DbContext = UnitOfWorkFactory.GetUnitOfWork(TCP_Server_Program.sqlDbName, DbSupportType.MicrosoftSqlServer);

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

                // Handle Package
                HandlePackage(data);

                // Response to Remote App
                var rep = Encoding.ASCII.GetBytes("r\nOK!\r\n");
                SendPackageToClient(rep);

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
        /// Xu ly goi dư lieu nhan duoc tu Client va luu vao database
        /// </summary>
        /// <param name="data"></param>
        private void HandlePackage(string data)
        {
            #region Split Data

            //string[] dataArray = data.Split('&');
            //DateTime dateTime = DateTime.Now;

            //// &I
            //string[] stringI = dataArray[1].Trim().Substring(2).Split(';');
            //string idcard = stringI[0].Trim();
            //string serial = stringI[1].Trim();


            //// &G
            //string[] stringG = dataArray[2].Trim().Substring(2).Split(';');
            //string status = stringG[0].Trim();
            //string vido = stringG[1].Trim();
            //string kinhdo = stringG[2].Trim();
            //string vantocgps = stringG[3].Trim();
            //string khoangcachgps = stringG[4].Trim();
            //string tongkhoangcach = stringG[5].Trim();


            //// &S1
            //string[] stringS1 = dataArray[3].Trim().Substring(3).Split(';');
            //string trangthai = stringS1[0].Trim();
            //string dienapbinh = stringS1[1].Trim();
            //string dienappin = stringS1[2].Trim();
            //string cuongdoGSM = stringS1[3].Trim();
            //string loithenho = stringS1[4].Trim();


            //// &D
            //string[] stringD = dataArray[4].Trim().Substring(2).Split(';');
            //string cuocxe = stringD[0].Trim();
            //string thoigian = stringD[1].Trim();


            //// &H1
            //if (data.IndexOf("&H1") != -1)
            //{
            //    string[] stringH1 = dataArray[5].Trim().Substring(3).Split(';');
            //    string maUID = stringH1[0].Trim();
            //    string giaypheplaixe = stringH1[1].Trim();
            //    string vantocxe = stringH1[2].Trim();

            //}

            //// &CT
            //if (data.IndexOf("&CT") != -1)
            //{
            //    string[] stringCT = dataArray[6].Trim().Substring(3).Split(';');
            //    string from = stringCT[0].Trim();
            //    string sdt = stringCT[1].Trim();
            //    string malenh = stringCT[2].Trim();
            //    string okerror = stringCT[3].Trim();

            //}

            #endregion

            // Save Rawdata
            SaveRawData(data, "001-debug");

            // Checksum

            // Split data and Save
            //SaveSplittedData(data);
        }

        // Spli and Save data
        private void SaveSplittedData(string data)
        {
            try
            {
                log.Info("Save data to database");
            }
            catch (Exception e)
            {
                log.Error("Save data failed ", e);
            }
        }

        // Save Raw data
        private void SaveRawData(string data, string serial)
        {
            DateTime dateTime = DateTime.Now;
            RawData obj = new RawData(serial, data, dateTime);

            DbContext.Insert<RawData>(obj);
            DbContext.Commit();
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