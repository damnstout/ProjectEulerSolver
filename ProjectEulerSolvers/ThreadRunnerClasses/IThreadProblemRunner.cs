using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ProjectEulerSolvers.ThreadRunnerClasses
{
    interface IThreadProblemRunner<T>
    {

        void Run(object context);

        void SetDoneNotifier(ManualResetEvent doneEvent);

        string GetTag();
    }
}
