using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ThingsBook.DataAccessInterface
{
    public class GuidUtils
    {
        private static BigInteger _count = 0;

        public static Guid NewGuid()
        {
            var a = _count.ToByteArray();
            var array = new List<byte>(a);
            for (int i = a.Length; i < 16; i++)
            {
                array.Add(0x00);
            }
            if (array.Count > 16)
            {
                array = array.Take(16).ToList();
                _count = 0;
            }
            _count++;
            return new Guid(array.ToArray());
        }
    }
}
