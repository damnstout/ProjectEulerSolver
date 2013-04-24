using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ProjectEulerSolvers.ThreadRunnerClasses
{
    class TRProb072 : TRProbBase<long>
    {
        static readonly int limit = 1000000;
        static ThreadResultHolder<long> ResultHolder = new ThreadResultHolder<long>();

        private int _begin;
        private int _count;
        private ThreadResultHolder<long> _holder;

        public TRProb072(int begin, int count, ThreadResultHolder<long> holder, string tag)
        {
            _begin = begin;
            _count = count;
            _holder = holder;
            Tag = tag;
        }

        protected override void Run()
        {
            long rst = 0;
            (from x in Enumerable.Range(_begin, _count) select Tools.EulerPhi(x)).ToList().ForEach(x => rst += x);
            _holder.AddResult(rst);
        }

        private static TRProb072[] Slice(int fragmentSize)
        {
            return (
                from x in Enumerable.Range(0, limit / fragmentSize)
                select new TRProb072(x * fragmentSize + 1, fragmentSize, ResultHolder, x.ToString())
            ).ToArray();
        }

        public static long Calculate(int fragmentSize)
        {
            ThreadRunner<long> tr = new ThreadRunner<long>();
            tr.Run(Slice(fragmentSize));
            return ResultHolder.Sum() - 1;
        }

    }

}
