using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emil.GMP;

namespace ProjectEulerSolvers
{
    class Fraction
    {
        private BigInt _num;
        private BigInt _deno;
        public BigInt Numerator { get { return _num; } set { _deno = value; } }
        public BigInt Denominator 
        {
            get { return _deno; }
            set
            {
                if (0 == value) throw new System.ArgumentException("denominator should not be 0");
                _deno = value;
            }
        }

        public Fraction(BigInt num, BigInt deno)
        {
            BigInt gcd = BigInt.Gcd(num, deno);
            _num = num / gcd;
            _deno = deno / gcd;
        }

        public static Fraction operator +(Fraction a, Fraction b)
        {
            BigInt deno = a.Denominator * b.Denominator;
            BigInt num = (a.Numerator * b.Denominator) + (b.Numerator * a.Denominator);
            return new Fraction(num, deno);
        }

        public static Fraction operator -(Fraction a, Fraction b)
        {
            BigInt deno = a.Denominator * b.Denominator;
            BigInt num = (a.Numerator * b.Denominator) - (b.Numerator * a.Denominator);
            return new Fraction(num, deno);
        }

        public static Fraction operator *(Fraction a, Fraction b)
        {
            return new Fraction(a.Numerator * b.Numerator, a.Denominator * b.Denominator);
        }

        public static Fraction operator /(Fraction a, Fraction b)
        {
            return new Fraction(a.Numerator * b.Denominator, a.Denominator * b.Numerator);
        }

        public override string ToString()
        {
            return _num % _deno == 0 ? string.Format("{0}", _num / _deno) : string.Format("{0} / {1}", _num, _deno);
        }
    }
}
