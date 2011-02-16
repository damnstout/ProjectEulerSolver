using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEulerSolvers.ThreadRunnerClasses
{
    class TRProb069 : TRProbBase<KeyValuePair<double, int>>
    {
        static readonly int limit = 1000000;
        static ThreadResultHolder<KeyValuePair<double, int>> ResultHolder = new ThreadResultHolder<KeyValuePair<double, int>>();

        int _begin;
        int _count;
        ThreadResultHolder<KeyValuePair<double, int>> _holder;

        public TRProb069(int begin, int count, ThreadResultHolder<KeyValuePair<double, int>> holder, string tag)
        {
            _begin = begin;
            _count = count;
            _holder = holder;
            Tag = tag;
        }

        protected override void Run()
        {
            int rst = 2;
            double maxRatio = 2;
            int end = _begin + _count;
            for (int n = _begin; n < end; n++)
            {
                double ratio = n / (double)Tools.EulerPhi(n);
                if (ratio <= maxRatio) continue;
                maxRatio = ratio;
                rst = n;
            }
            ResultHolder.AddResult(new KeyValuePair<double, int>(maxRatio, rst));
        }

        private static TRProb069[] Slice(int fragmentSize)
        {
            return (
                from x in Enumerable.Range(0, limit / fragmentSize)
                select new TRProb069(x * fragmentSize + 1, fragmentSize, ResultHolder, x.ToString())
            ).ToArray();
        }

        public static long Calculate(int fragmentSize)
        {
            ThreadRunner<KeyValuePair<double, int>> tr = new ThreadRunner<KeyValuePair<double, int>>();
            tr.Run(Slice(fragmentSize));
            return (from x in ResultHolder orderby x.Key ascending select x).Last().Value;
        }
    }
}
