using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEulerSolvers
{
    class Permutater<T, K> : IEnumerable<T>, IEnumerator<T> where T : IList<K>, new()
    {
        private T _input;

        private T _output;

        private int[] _indexes;

        public Permutater(T input)
        {
            _input = input;
            _output = new T();
            _indexes = new int[_input.Count];
        }

        #region IDisposable 成员

        public void Dispose()
        {
        }

        #endregion

        #region IEnumerator 成员

        public T Current
        {
            get 
            {
                for (int i = 0; i < _indexes.Length; i++) _output[i] = _input[_indexes[i]];
                return _output; 
            }
        }

        object System.Collections.IEnumerator.Current
        {
            get { return _output; }
        }

        public bool MoveNext()
        {
            return NextPermutation(_indexes);
        }

        public void Reset()
        {
            for (int i = 0; i < _indexes.Length; i++ )
            {
                _indexes[i] = i;
                _output.Add(_input[i]);
            }
        }

        #endregion

        #region IEnumerable<T> 成员

        public IEnumerator<T> GetEnumerator()
        {
            Reset();
            return this;
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this;
        }

        #endregion

        public static void Swap(ref int a, ref int b)
        {
            int temp;
            temp = a;
            a = b;
            b = temp;
        }

        public static bool NextPermutation(int[] p)
        {
            int n = p.Length;
            int last = n - 1;
            int i, j, k;

            //从后向前查找，看有没有后面的数大于前面的数的情况，若有则停在后一个数的位置。
            i = last;
            while (i > 0 && p[i] < p[i - 1])
                i--;
            //若没有后面的数大于前面的数的情况，说明已经到了最后一个排列，返回false。
            if (i == 0)
                return false;

            //从后查到i，查找大于p[i - 1]的最小的数，记入k
            k = i;
            for (j = last; j >= i; j--)
                if (p[j] > p[i - 1] && p[j] < p[k])
                    k = j;
            //交换p[k]和p[i - 1]
            Swap(ref p[k], ref p[i - 1]);
            //倒置p[last]到p[i]
            for (j = last, k = i; j > k; j--, k++)
                Swap(ref p[j], ref p[k]);

            return true;
        }

    }
}
