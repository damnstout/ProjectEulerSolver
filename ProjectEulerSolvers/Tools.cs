using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emil.GMP;

namespace ProjectEulerSolvers
{
    class Tools
    {
        static List<int> intList = new List<int>();

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

        public static BigInt GCD(BigInt a, BigInt b)
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

        public static BigInt LCM(BigInt a, BigInt b)
        {
            return (a * b) / GCD(a, b);
        }

        /// <summary>
        /// 判断是否回文数
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool IsPalindromic(string n)
        {
            int count = n.Length == 1 ? 0 : (n.Length % 2 == 0) ? (n.Length / 2) : ((n.Length / 2) + 1);
            for (int i = 0; i <= count; i++)
            {
                if (n[i] != n[n.Length - i - 1])
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsPalindromic(object n)
        {
            return IsPalindromic(n.ToString());
        }

        /// <summary>
        /// 判断是否素数
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool IsPrime(long n)
        {
            double k;
            if (n == 2 || n == 3)
            {
                return true;
            }
            if (n == 1)
            {
                return false;
            }
            k = Math.Sqrt(n) + 1;
            for (int i = 2; i <= k; i++)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 求第N个三角数（Triangle Number）
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long TriangleN(long n)
        {
            return n * (n + 1) / 2;
        }

        /// <summary>
        /// 整除数个数
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int DivisorCount(long n)
        {
            intList.Clear();
            int p = 2, rst = 1, tmp = 0;
            while (p <= n)
            {
                if (n % p == 0)
                {
                    n = n / p;
                    tmp++;
                }
                else
                {
                    if (0 != tmp) intList.Add(tmp + 1);
                    p++;
                    tmp = 0;
                }
            }
            if (tmp > 0) intList.Add(tmp + 1);
            foreach (int i in intList) rst *= i;
            return rst;
        }
    }
}
