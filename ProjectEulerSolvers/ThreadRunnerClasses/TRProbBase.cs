using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ProjectEulerSolvers.ThreadRunnerClasses
{
    abstract class TRProbBase<T> : IThreadProblemRunner<T>
    {

        public string Tag { get; set; }
        private ManualResetEvent _doneEvent;

        protected abstract void Run();

        #region IThreadProblemRunner<T> 成员

        void IThreadProblemRunner<T>.Run(object context)
        {
            _doneEvent.Reset();
            Program.OutputLine("working thread: {0}", Tag);
            Run();
            _doneEvent.Set();
        }

        public void SetDoneNotifier(ManualResetEvent doneEvent)
        {
            _doneEvent = doneEvent;
        }

        public string GetTag()
        {
            return Tag;
        }

        #endregion
    }
}
