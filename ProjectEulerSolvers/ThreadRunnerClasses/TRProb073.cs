using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ProjectEulerSolvers.ThreadRunnerClasses
{
    class TRProb073 : TRProbBase<int>
    {
        static readonly int limit = 12000;
        static ThreadResultHolder<int> ResultHolder = new ThreadResultHolder<int>();

        private int _begin;
        private int _count;
        private ThreadResultHolder<int> _holder;

        public TRProb073(int begin, int count, ThreadResultHolder<int> holder, string tag)
        {
            _begin = begin;
            _count = count;
            _holder = holder;
            Tag = tag;
        }

        protected override void Run()
        {
            int rst = 0;
            for (int n = _begin; n < _begin + _count; n++)
            {
                int ceil = (int)Math.Floor(n / 2.0);
                int floor = (int)Math.Ceiling(n / 3.0);
                for (int p = floor; p <= ceil; p++)
                {
                    rst += (1 == Tools.GCD(n, p) ? 1 : 0);
                }
            }
            _holder.AddResult(rst);
        }

        private static TRProb073[] Slice(int fragmentSize)
        {
            return (
                from x in Enumerable.Range(0, limit / fragmentSize)
                select new TRProb073(x * fragmentSize + 1, fragmentSize, ResultHolder, x.ToString())
            ).ToArray();
        }

        public static int Calculate(int fragmentSize)
        {
            ThreadRunner<int> tr = new ThreadRunner<int>();
            tr.Run(Slice(fragmentSize));
            return ResultHolder.Sum() - 1;
        }

    }

}
