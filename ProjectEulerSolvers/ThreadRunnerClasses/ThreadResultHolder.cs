using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEulerSolvers.ThreadRunnerClasses
{
    class ThreadResultHolder<T> : IEnumerable<T>
    {
        private List<T> holder;

        private Object mutex = new Object();

        public ThreadResultHolder()
        {
            holder = new List<T>();
        }

        public void AddResult(T value)
        {
            lock (mutex)
            {
                holder.Add(value);
            }
        }

        public void Clear()
        {
            lock (mutex)
            {
                holder.Clear();
            }
        }

        #region IEnumerable<T> 成员

        public IEnumerator<T> GetEnumerator()
        {
            return holder.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return holder.GetEnumerator();
        }

        #endregion
    }
}
