using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ProjectEulerSolvers.ThreadRunnerClasses
{
    class TRProb014 : TRProbBase<int>
    {
        static readonly int limit = 1000000;
        static ThreadResultHolder<KeyValuePair<int, int>> ResultHolder = new ThreadResultHolder<KeyValuePair<int, int>>();
        static Dictionary<int, int> cache = new Dictionary<int, int>();
        static object cacheMutex = new object();

        private int _begin;
        private int _count;
        private ThreadResultHolder<KeyValuePair<int, int>> _holder;

        public TRProb014(int begin, int count, ThreadResultHolder<KeyValuePair<int, int>> holder, string tag)
        {
            _begin = begin;
            _count = count;
            _holder = holder;
            Tag = tag;
        }

        protected override void Run()
        {
            int rst = 0, maxCollatz = 0;
            for (int n = _begin; n < _begin + _count; n++)
            {
                int c = Collatz(n);
                if (c <= maxCollatz) continue;
                rst = n;
                maxCollatz = c;
            }
            _holder.AddResult(new KeyValuePair<int, int>(rst, maxCollatz));
        }

        private int Collatz(int num)
        {
            int counter = 0;
            long n = num;
            while (n != 1)
            {
                if (n % 2 == 0)
                {
                    n = n / 2;
                }
                else
                {
                    n = 3 * n + 1;
                }
                if (CheckCache((int)n))
                {
                    counter += cache[(int) n];
                    break;
                }
                counter++;
            }
            AddCache(num, counter);
            return counter;
        }

        private static bool CheckCache(int n)
        {
            //lock (cacheMutex)
            //{
            return cache.ContainsKey(n);
            //}
        }

        private static void AddCache(int n, int collatz)
        {
            lock (cacheMutex)
            {
                cache.Add(n, collatz);
            }
        }

        private static TRProb014[] Slice(int fragmentSize)
        {
            return (
                from x in Enumerable.Range(0, limit / fragmentSize)
                select new TRProb014(x * fragmentSize + 1, fragmentSize, ResultHolder, x.ToString())
            ).ToArray();
        }

        public static int Calculate(int fragmentSize)
        {
            ThreadRunner<int> tr = new ThreadRunner<int>();
            tr.Run(Slice(fragmentSize));
            return (from x in ResultHolder orderby x.Value descending select x.Key).First();
        }

    }

}
