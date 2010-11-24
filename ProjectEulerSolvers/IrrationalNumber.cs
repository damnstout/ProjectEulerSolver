using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emil.GMP;

namespace ProjectEulerSolvers
{
    public interface ExpandSequence
    {
        int Ratio(int n);

        List<int> RatioList(int length);
    }

    public abstract class DefaultExpandSequence : ExpandSequence
    {

        #region ExpandSequence 成员

        public abstract int Ratio(int n);

        public List<int> RatioList(int length)
        {
            List<int> rst = new List<int>(length);
            for (int i = 0; i < length; i++ )
            {
                rst.Add(Ratio(i + 1));
            }
            return rst;
        }

        #endregion
    }

    public class SqrtExpandSequence : DefaultExpandSequence
    {
        private List<int> _ratioCycle;

        public SqrtExpandSequence(List<int> rationCycle) { _ratioCycle = rationCycle; }

        #region ExpandSequence 成员

        public override int Ratio(int n) { return _ratioCycle[(n - 1) % _ratioCycle.Count]; }

        #endregion

        public override string ToString()
        {
            return string.Format("({0})", string.Join(",", (from ratio in _ratioCycle select ratio.ToString()).ToArray()));
        }
    }

    class IrrationalNumber
    {
        private Fraction _base;
        private ExpandSequence _expSeq;

        public IrrationalNumber(BigInt baseNum, ExpandSequence expSeq)
        {
            _base = Fraction.Integer(baseNum);
            _expSeq = expSeq;
        }

        public Fraction ToFraction(int expandLevel)
        {
            Fraction irrationPart = Fraction.Integer(0);
            List<int> ratios = _expSeq.RatioList(expandLevel);
            for (int i = expandLevel - 1; i >= 0; i--)
            {
                irrationPart = Fraction.ONE / (Fraction.Integer(ratios[i]) + irrationPart);
            }
            return _base + irrationPart;
        }

        public override string ToString()
        {
            return string.Format("[{0};{1}]", _base, _expSeq);
        }
    }
}
