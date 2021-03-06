﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emil.GMP;

namespace ProjectEulerSolvers
{
    class Tools
    {
        static List<int> intList = new List<int>();

        public static int EulerPhi(int n)
        {
            Dictionary<int, int> factorsContainer = new Dictionary<int, int>();
            return EulerPhi(n, factorsContainer);
        }

        public static int EulerPhi(int n, Dictionary<int, int> factorsContainer)
        {
            if (1 == n) return 1;
            if (IsPrime(n)) return n - 1;
            int rst = 1;
            FindPrimeFactors2(n, factorsContainer);
            factorsContainer.ToList().ForEach(p => rst *= (int)(Math.Pow(p.Key, p.Value) - Math.Pow(p.Key, p.Value - 1)));
            return rst;
        }

        public static void FindPrimeFactors(int n, Dictionary<int, int> result)
        {
            result.Clear();
            if (PrimeHelper.IsPrime(n)) return;
            int number = n;
            int divisor = 2;
            while (number > 1)
            {
                if (0 == (number % divisor))
                {
                    number /= divisor;
                    AddPrimeFactor(divisor, result);
                    divisor--;
                }
                divisor++;
            }
        }

        public static Dictionary<int, int> FindPrimeFactors2(int n)
        {
            Dictionary<int, int> result = new Dictionary<int, int>();
            FindPrimeFactors2(n, result);
            return result;
        }

        public static void FindPrimeFactors2(int n, Dictionary<int, int> result)
        {
            result.Clear();
            if (PrimeHelper.IsPrime(n)) return;
            int number = n;
            foreach (long p in PrimeHelper.Primes)
            {
                if (PrimeHelper.IsPrime(number))
                {
                    AddPrimeFactor(number, result);
                    break;
                }
                if (1 == number) break;
                if (p * p > number) break;
                while (0 == (number % p))
                {
                    number /= (int) p;
                    AddPrimeFactor((int)p, result);
                }
            }
        }

        private static void AddPrimeFactor(int factor, Dictionary<int, int> result)
        {
            if (result.ContainsKey(factor))
            {
                result[factor] = result[factor] + 1;
            }
            else
            {
                result.Add(factor, 1);
            }
        }

        /// <summary>
        /// 判断两数是否互为全排列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        /// <returns></returns>
        public static bool IsPermutation<T>(T n1, T n2)
        {
            List<char> lst1 = n1.ToString().ToCharArray().ToList();
            lst1.Sort();
            List<char> lst2 = n2.ToString().ToCharArray().ToList();
            lst2.Sort();
            if (lst1.Count != lst2.Count) return false;
            for (int i = 0; i < lst1.Count; i++)
            {
                if (lst1[i] != lst2[i]) return false;
            }
            return true;
        }

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

        public static int GCD(int a, int b)
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

        public static long SquareN(long n)
        {
            return n * n;
        }

        public static long PentagonalN(long n)
        {
            return n * (3 * n - 1) / 2;
        }

        public static long HexagonalN(long n)
        {
            return n * (2 * n - 1);
        }

        public static long HeptagonalN(long n)
        {
            return n * (5 * n - 3) / 2;
        }

        public static long OctagonalN(long n)
        {
            return n * (3 * n - 2);
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
