using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEulerSolvers
{
    class ExpandFraction : IEquatable<ExpandFraction>
    {
        private int _n;
        private int _floor;
        private int _base;
        private int _subtrahend;
        private int _deno;

        public int Base { get { return _base; } }

        public ExpandFraction(int n, int floor, int basePart, int subtrahend, int deno)
        {
            _n = n;
            _floor = floor;
            _base = basePart;
            _subtrahend = subtrahend;
            _deno = deno;
        }

        public ExpandFraction Next()
        {
            int deno = (_n - (_subtrahend * _subtrahend)) / _deno;
            int subtrahend = Enumerable.Range(0, _floor + 1)
                .Where(x => (x + _subtrahend) % deno == 0 && _floor - x <= deno - 1)
                .Last();
            int basePart = (subtrahend + _subtrahend) / deno;
            return new ExpandFraction(_n, _floor, basePart, subtrahend, deno);
        }

        #region object 重写
        public override bool Equals(object obj)
        {
            if (!this.GetType().Equals(obj.GetType())) return false;
            return this.Equals((ExpandFraction) obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", _base, _subtrahend, _deno);
        }
        #endregion

        #region IEquatable<ExpandFraction> 成员

        public bool Equals(ExpandFraction other)
        {
            return _subtrahend == other._subtrahend && _deno == other._deno;
        }

        #endregion

        #region 运算符重载
        public static bool operator ==(ExpandFraction a, ExpandFraction b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(ExpandFraction a, ExpandFraction b)
        {
            return !a.Equals(b);
        }
        #endregion
    }

    class Sqrt
    {
        private int _n;
        private int _floor;
        private List<int> _ratioCycle;
        private IrrationalNumber _irraNum;

        private ExpandFraction _baseFraction;

        public int Floor { get { return _floor; } }
        public List<int> RatioCycle { get { return _ratioCycle; } }
        public IrrationalNumber IrrationNumber { get { return _irraNum; } }

        public Sqrt(int n)
        {
            _ratioCycle = new List<int>();
            _n = n;
            _floor = (int) Math.Floor(Math.Sqrt(_n));
            _baseFraction = new ExpandFraction(n, _floor, _floor, _floor, 1);
            ExpandFraction ef = new ExpandFraction(n, _floor, _floor, _floor, 1);

            while (true)
            {
                ef = ef.Next();
                _ratioCycle.Add(ef.Base);
                if (_baseFraction == ef) break;
            }
            _irraNum = new IrrationalNumber(_floor, new SqrtExpandSequence(_ratioCycle));
        }

        public override string ToString()
        {
            return _irraNum.ToString();
        }
    }
}
