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

        private static List<long> longPrimeList = new List<long>();

        private static long _primeLimit = 0;

        private static object mutex = new object();

        public static bool IsPrime(long n)
        {
            if (longPrimes.Contains(n)) return true;
            if (!JudgePrime(n)) return false;
            lock (mutex)
            {
                if (!longPrimes.Contains(n)) longPrimes.Add(n);
            }
            return true;
        }

        private static bool JudgePrime(long n)
        {
            return Tools.IsPrime(n);
        }

        public static long PrimeLimit
        {
            get { return _primeLimit; }
            set
            {
                lock (mutex)
                {
                    if (value != _primeLimit) InitPrimeList(value);
                }
            }
        }

        public static long PrimeFloor
        {
            get { return _primeLimit; }
            set
            {
                lock (mutex)
                {
                    if (value > _primeLimit) InitPrimeList(value);
                }
            }
        }

        public static List<long> Primes
        {
            get { return longPrimeList; }
        }

        private static void InitPrimeList(long limit)
        {
            if (limit < _primeLimit)
            {
                ChopPrimeList(limit);
                return;
            }
            Program.OutputLine("PrimeHelper: initializing prime list...");
            longPrimeList.Clear();
            long[] sieve = new long[limit + 1];
            for (long i = 2; i <= limit; i++) sieve[i] = ((i % 2 == 0 && i != 2) ? 0 : 1);
            for (long p = 3; p * p <= limit; p++)
            {
                while (sieve[p] == 0) p++;
                long t = p * p, s = 2 * p;
                while (t <= limit)
                {
                    sieve[t] = 0;
                    t = t + s;
                }
            }
            for (long n = 2; n <= limit; n++)
            {
                if (1 != sieve[n]) continue;
                longPrimeList.Add(n);
            }
            _primeLimit = limit;
        }

        private static void ChopPrimeList(long limit)
        {
            int i = 0;
            while (longPrimeList[i] <= limit) i++;
            longPrimeList.RemoveRange(i, longPrimeList.Count - i);
            _primeLimit = limit;
        }

        private static void AppendPrimeList(long limit)
        {
            Program.OutputLine("PrimeHelper: appending prime list...");
            for (long n = _primeLimit + 1; n <= limit; n++)
            {
                if (IsPrime(n)) longPrimeList.Add(n);
            }
            _primeLimit = limit;
        }
    }
}
