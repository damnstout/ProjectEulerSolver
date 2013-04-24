using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ProjectEulerSolvers.ThreadRunnerClasses
{
    class TRProb074 : TRProbBase<long>
    {
        static readonly int limit = 1000000;
        static ThreadResultHolder<int> ResultHolder = new ThreadResultHolder<int>();

        private int _begin;
        private int _count;
        private ThreadResultHolder<int> _holder;

        static Dictionary<char, int> FactorialRegister = new Dictionary<char, int>();
        static Dictionary<int, int> LenRegister = new Dictionary<int, int>();

        static object mutex = new object();

        static TRProb074()
        {
            int tmp = 1;
            for (int i = 0; i < 10; i++)
            {
                if (i != 0) tmp *= i;
                FactorialRegister.Add((char)('0' + i), tmp);
            }
        }

        public TRProb074(int begin, int count, ThreadResultHolder<int> holder, string tag)
        {
            _begin = begin;
            _count = count;
            _holder = holder;
            Tag = tag;
        }

        protected override void Run()
        {
            _holder.AddResult(
                (from x in (from n in Enumerable.Range(_begin, _count) select ProbeLength(n)) where x == 60 select x).Count());
        }

        private int CalcFactorialSum(int n)
        {
            return (from x in n.ToString().ToCharArray() select FactorialRegister[x]).Sum();
        }

        private int ProbeLength(int n)
        {
            HashSet<int> pathRegister = new HashSet<int>();
            pathRegister.Add(n);
            int counter = 1;
            int s = n;
            while (true)
            {
                s = CalcFactorialSum(s);
                if (pathRegister.Contains(s)) break;
                if (LenRegister.ContainsKey(s))
                {
                    counter += LenRegister[s];
                    break;
                }
                pathRegister.Add(s);
                counter++;
            }
            AddLen(n, counter);
            return counter;
        }

        private void AddLen(int n, int len)
        {
            lock (mutex)
            {
                LenRegister.Add(n, len);
            }
        }

        private static TRProb074[] Slice(int fragmentSize)
        {
            return (
                from x in Enumerable.Range(0, limit / fragmentSize)
                select new TRProb074(x * fragmentSize + 1, fragmentSize, ResultHolder, x.ToString())
            ).ToArray();
        }

        public static long Calculate(int fragmentSize)
        {
            ThreadRunner<long> tr = new ThreadRunner<long>();
            tr.Run(Slice(fragmentSize));
            return ResultHolder.Sum();
        }

    }

}
