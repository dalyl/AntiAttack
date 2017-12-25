using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AntiAttack.Core
{
    internal static class Extensions
    {
        /// <summary>
        /// IP 转 uint 值
        /// </summary>
        /// <param name="ipAddr">IP 地址</param>
        /// <returns></returns>
        public static uint IpToUint(this string ipAddr)
        {
            byte[] b = IPAddress.Parse(ipAddr).GetAddressBytes();
            Array.Reverse(b);
            return BitConverter.ToUInt32(b, 0);
        }
    }
}
