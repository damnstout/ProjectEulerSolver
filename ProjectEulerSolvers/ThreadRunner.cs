using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using ProjectEulerSolvers.ThreadRunnerClasses;

namespace ProjectEulerSolvers
{
    class ThreadRunner<T>
    {
        static readonly int MaxWorkThread = 10;
        public void Run(IThreadProblemRunner<T>[] t)
        {
            ThreadPool.SetMaxThreads(MaxWorkThread, t.Length);
            ManualResetEvent[] doneEvents = new ManualResetEvent[t.Length];
            for (int i = 0; i < t.Length; i++)
            {
                doneEvents[i] = new ManualResetEvent(false);
                t[i].SetDoneNotifier(doneEvents[i]);
            }
            t.ToList().ForEach(x => ThreadPool.QueueUserWorkItem(x.Run));
            WaitHandle.WaitAll(doneEvents);
        }
    }
}
