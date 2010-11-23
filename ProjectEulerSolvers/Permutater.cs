using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEulerSolvers
{
    class Permutater<T, K> : IEnumerable<T>, IEnumerator<T> where T : IList<K>
    {
        private T a;


        #region IDisposable 成员

        public void Dispose()
        {
            K k = a[0];
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerator 成员

        public T Current
        {
            get { throw new NotImplementedException(); }
        }

        object System.Collections.IEnumerator.Current
        {
            get { throw new NotImplementedException(); }
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<T> 成员

        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
