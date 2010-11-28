using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emil.GMP;

namespace ProjectEulerSolvers
{
    class PrimeHelper
    {
        private static HashSet<long> longPrimes = new HashSet<long>();

        public static bool IsPrime(long n)
        {
            if (longPrimes.Contains(n)) return true;
            if (!JudgePrime(n)) return false;
            longPrimes.Add(n);
            return true;
        }

        private static bool JudgePrime(long n)
        {
            return Tools.IsPrime(n);
        }

    }
}
