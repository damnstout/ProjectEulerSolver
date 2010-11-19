using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Oyster.Math;

namespace ProjectEulerSolvers
{
    class Tools
    {
        /// <summary>
        /// 最大公约数
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static long GCD(long a, long b)
        {
            if (a % b == 0) return b;
            return GCD(b, a % b);
        }

        public static IntX GCD (IntX a, IntX b)
        {
            if (a % b == 0) return b;
            return GCD(b, a % b);
        }

        /// <summary>
        /// 最小公倍数
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static long LCM(long a, long b)
        {
            return (a * b) / GCD(a, b);
        }
    }
}
