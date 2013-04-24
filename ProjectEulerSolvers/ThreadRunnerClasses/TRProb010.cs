using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEulerSolvers.ThreadRunnerClasses
{
    class TRProb010 : TRProbBase<long>
    {

        static readonly int limit = 2000000;
        static ThreadResultHolder<long> ResultHolder = new ThreadResultHolder<long>();

        private int _begin;
        private int _count;
        private ThreadResultHolder<long> _holder;

        public TRProb010(int begin, int count, ThreadResultHolder<long> holder, string tag)
        {
            _begin = begin;
            _count = count;
            _holder = holder;
            Tag = tag;
        }

        protected override void Run()
        {
            long rst = 0;
            int end = _begin + _count;
            for (int n = _begin; n < end; n++) rst += PrimeHelper.IsPrime(n) ? n : 0;
            _holder.AddResult(rst);
        }


        private static TRProb010[] Slice(int fragmentSize)
        {
            return (
                from x in Enumerable.Range(0, limit / fragmentSize)
                select new TRProb010(x * fragmentSize + 1, fragmentSize, ResultHolder, x.ToString())
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
