using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TCP_Server.Core
{
    public enum InitialCrcValue { Zeros, NonZero1 = 0xffff, NonZero2 = 0x1D0F }

    public class Crc16Ccitt
    {
        private const ushort Poly = 0x1021;//4129;
        private readonly ushort[] _table = new ushort[256];
        private readonly ushort _initialValue;

        public ushort ComputeChecksum(byte[] bytes)
        {
            var crc = _initialValue;
            return bytes.Aggregate(crc, (current, t) => (ushort)((current << 8) ^ _table[((current >> 8) ^ (0xff & t))]));
        }

        public byte[] ComputeChecksumBytes(byte[] bytes)
        {
            var crc = ComputeChecksum(bytes);
            return BitConverter.GetBytes(crc);
        }

        public bool CheckChecksum(string sData)
        {
            //chi ap dung cho string dang: data1;data2;data3;...;datan;crc
            var n = sData.LastIndexOf(';');
            if (n == -1)
                return false;

            var dataString = sData.Substring(0, n + 1);
            var crcString = sData.Substring(n + 1);

            var crc = ComputeChecksum(Encoding.ASCII.GetBytes(dataString));
            return ConvertToBase64String(crc) == crcString;
        }

        public Crc16Ccitt(InitialCrcValue initialValue)
        {
            _initialValue = (ushort)initialValue;
            for (var i = 0; i < _table.Length; ++i)
            {
                ushort temp = 0;
                var a = (ushort)(i << 8);
                for (var j = 0; j < 8; ++j)
                {
                    if (((temp ^ a) & 0x8000) != 0)
                    {
                        temp = (ushort)((temp << 1) ^ Poly);
                    }
                    else
                    {
                        temp <<= 1;
                    }
                    a <<= 1;
                }
                _table[i] = temp;
            }
        }

        public static string ConvertToBase64String(int tp)
        {
            var b = new byte[4];
            var i = 0;
            if (tp == 0)
            {
                return "@";
            }

            while (tp > 0)
            {
                b[i] = (byte)((tp % 63) + 64);
                tp /= 63;
                i++;
            }

            var finalb = new byte[i];
            for (var j = 0; j < i; j++)
                finalb[j] = b[j];

            return Encoding.ASCII.GetString(finalb);
        }

        public static string ConvertToBase64String(string stp)
        {
            int tp;
            int.TryParse(stp, out tp);
            return ConvertToBase64String(tp);
        }

        public static int ConvertFromBase64String(string s64)
        {
            var tp = 0;

            int i;
            var j = s64.Length;

            for (i = (j - 1); i > 0; i--)
            {
                tp = tp * 63 + (s64[i] - 64);
            }
            tp = tp * 63 + (s64[0] - 64);

            return tp;
        }
    }
}