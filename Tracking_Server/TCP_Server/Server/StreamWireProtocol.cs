using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Communication.Protocols;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TCP_Server.Server
{
    public class StreamWireProtocol : IScsWireProtocol
    {
        #region properties

        private MemoryStream receiveMemoryStream;

        #endregion

        #region constructor
        public StreamWireProtocol()
        {
            receiveMemoryStream = new MemoryStream();
        }
        #endregion

        #region Public methods

        /// <summary>
        /// Build message from a byte array received from remote application
        /// </summary>
        /// <param name="receivedBytes">Received bytes from remote application</param>
        /// <returns></returns>
        public IEnumerable<IScsMessage> CreateMessages(byte[] receivedBytes)
        {
            receiveMemoryStream.Write(receivedBytes, 0, receivedBytes.Length);

            var messageList = new List<IScsMessage>();

            var msg = new ScsRawDataMessage(receivedBytes);

            messageList.Add(msg);

            return messageList;
        }

        /// <summary>
        /// Serialize a message to a byte array 
        /// </summary>
        /// <param name="message">message to serialize</param>
        /// <returns></returns>
        public byte[] GetBytes(IScsMessage message)
        {
            if(!(message is ScsRawDataMessage))
            {
                return new byte[0];
            }

            var msg = message as ScsRawDataMessage;

            return msg.MessageData;
        }

        /// <summary>
        /// Reset protocal when connnection is reset
        /// </summary>
        public void Reset()
        {
            if(receiveMemoryStream.Length > 0)
            {
                receiveMemoryStream = new MemoryStream();
            }
        }

        #endregion
    }
}