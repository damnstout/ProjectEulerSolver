using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Threading;

using Emil.GMP;
using System.Reflection;

namespace ProjectEulerSolvers
{
    partial class Program
    {
        static long Prob021()
        {
            ICollection<int> amicableNumbers = new HashSet<int>();
            long rst = 0;
            for (int i = 1; i <= 10000; i++)
            {
                if (amicableNumbers.Contains(i))
                {
                    continue;
                }
                int factorsSum = Prob021SumFactors(Prob021ProbFactors(i));
                if (factorsSum == i)
                {
                    continue;
                }
                if (Prob021SumFactors(Prob021ProbFactors(factorsSum)) == i)
                {
                    OutputLine("find 1 pair of amicable numbers: {0:D} and {1:D}", i, factorsSum);
                    amicableNumbers.Add(i);
                    amicableNumbers.Add(factorsSum);
                }
            }
            foreach (int n in amicableNumbers)
            {
                rst += n;
            }
            return rst;
        }

        static int Prob021SumFactors(HashSet<int> factors)
        {
            int rst = 0;
            foreach (int n in factors)
            {
                rst += n;
            }
            return rst;
        }

        static HashSet<int> Prob021ProbFactors(int n)
        {
            int limit = (int)Math.Sqrt(n) + 1;
            HashSet<int> rst = new HashSet<int>();
            rst.Add(1);
            for (int i = 2; i <= limit; i++)
            {
                if (n % i == 0 && i < n)
                {
                    rst.Add(i);
                    rst.Add(n / i);
                }
            }
            return rst;
        }

        static long Prob022()
        {
            StreamReader r = new StreamReader(@"data\Prob022_names.txt");
            string line = r.ReadLine();
            line = line.Replace("\"", "");
            string[] a = line.Split(',');
            List<string> lst = a.ToList();
            lst.Sort();
            int counter = 1;
            long rst = 0;
            foreach (string s in lst)
            {
                char[] ca = s.ToCharArray();
                int tmp = 0;
                foreach (char c in ca)
                {
                    tmp += c - 'A' + 1;
                }
                rst += tmp * counter;
                counter++;
            }
            return rst;
        }

        static long Prob023()
        {
            List<int> abundantList = new List<int>();
            HashSet<int> abundantSet = new HashSet<int>();
            Prob023ProbAbundantNums(abundantList, abundantSet);
            long rst = 0;
            for (int n = 1; n <= 28123; n++)
            {
                if (!Prob023IsAbundantSum(abundantList, abundantSet, n))
                {
                    rst += n;
                }
            }
            return rst;
        }

        static bool Prob023IsAbundantSum(List<int> l, HashSet<int> s, int n)
        {
            foreach (int i in l)
            {
                if (i >= n)
                {
                    continue;
                }
                if (s.Contains(n - i))
                {
                    return true;
                }
            }
            OutputLine("found unabundant sumable number: {0}", n);
            return false;
        }

        static void Prob023ProbAbundantNums(List<int> l, HashSet<int> s)
        {
            for (int i = 1; i <= 28123; i++)
            {
                if (i < Prob021SumFactors(Prob021ProbFactors(i)))
                {
                    l.Add(i);
                    s.Add(i);
                }
            }
        }

        static long Prob024()
        {
            string rst = "";
            int[] initA = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int sortAll = initA.Length - 1;
            long target = 1000000;
            target -= 1;
            List<int> lst = initA.ToList();
            while (target != 0)
            {
                long factorial = Prob024Factorial(sortAll);
                int sortAllCount = (int)(target / factorial);
                rst += Convert.ToString(lst[sortAllCount]);
                lst.RemoveAt(sortAllCount);
                target -= factorial * sortAllCount;
                sortAll--;
                OutputLine("current num is: {0}", rst);
            }
            foreach (int n in lst)
            {
                rst += Convert.ToString(n);
            }
            return Convert.ToInt64(rst);
        }

        static long Prob024Factorial(int n)
        {
            if (n == 0)
            {
                return 1;
            }
            if (n == 1)
            {
                return 1;
            }
            return Prob024Factorial(n - 1) * n;
        }

        static long Prob025()
        {
            BigInt f1 = 1, f2 = 1;
            long counter = 3;
            while (true)
            {
                BigInt tmp = f1 + f2;
                if (counter % 2 == 0)
                {
                    f2 = tmp;
                }
                else
                {
                    f1 = tmp;
                }
                OutputLine(string.Format("{0} is {1}th Fibonacci", tmp, counter));
                if (tmp.ToString().Length >= 1000)
                {
                    return counter;
                }
                counter++;
            }
        }

        static long Prob026()
        {
            long rst = 1;
            int lagestLen = 1;
            for (int n = 1; n <= 1000; n++)
            {
                int currLen = Prob026DecimalFractionLen(n);
                if (currLen > lagestLen)
                {
                    rst = n;
                    lagestLen = currLen;
                    OutputLine("current is {0}", n);
                }
            }
            return rst;
        }

        static int Prob026DecimalFractionLen(int n)
        {
            Output("probing {0}, ", n);
            HashSet<int> divisorHistory = new HashSet<int>();
            int counter = 0;
            int divisor = 1;
            while (divisor % n != 0 && !divisorHistory.Contains(divisor % n))
            {
                divisor = divisor % n;
                while (divisor < n)
                {
                    divisorHistory.Add(divisor);
                    divisor *= 10;
                    counter++;
                }
            }
            OutputLine("result: {0}", counter);
            return counter;
        }

        static int Prob026DivisorTransfer(int divisor, int n)
        {
            while (divisor < n)
            {
                divisor *= 10;
            }
            return divisor;
        }

        static long Prob027()
        {
            List<int> candiPrimes = new List<int>();
            HashSet<long> primes = new HashSet<long>();
            int currPrimeAmount = 0;
            int rst = 0;
            for (int i = 2; i < 1000; i++)
            {
                if (Tools.IsPrime(i))
                {
                    candiPrimes.Insert(0, -i);
                    candiPrimes.Add(i);
                    primes.Add(i);
                }
            }
            for (int a = 1 - 1000; a < 1000; a++)
            {
                Output("a={0}...", a);
                foreach (int b in candiPrimes)
                {
                    int n = 0;
                    long currPrime = Prob027Formula(n, a, b);
                    while (primes.Contains(Math.Abs(currPrime)) || Tools.IsPrime(Math.Abs(currPrime)))
                    {
                        n++;
                        currPrime = Prob027Formula(n, a, b);
                    }
                    if (n > currPrimeAmount)
                    {
                        currPrimeAmount = n;
                        rst = a * b;
                        OutputLine("current result is: n={0}, a={1}, b={2}", n, a, b);
                    }
                }
            }
            return rst;
        }

        static long Prob027Formula(int n, int a, int b)
        {
            return (n * n + a * n + b);
        }

        static long Prob028()
        {
            long rst = 1;
            int n = 1;
            int stepLen = 2;
            int size = 1001;
            int ceil = size * size;
            while (n < ceil)
            {
                for (int i = 0; i < 4; i++)
                {
                    n = n + stepLen;
                    rst += n;
                }
                stepLen += 2;
            }
            return rst;
        }

        static long Prob029Simple()
        {
            HashSet<double> rst = new HashSet<double>();
            for (double a = 2; a <= 100; a++)
            {
                for (double b = 2; b <= 100; b++)
                {
                    rst.Add(Math.Pow(a, b));
                    OutputLine("adding: pow({0}, {1}), total: {2}", a, b, rst.Count);
                }
            }
            return rst.Count;
        }

        static long Prob029()
        {
            HashSet<BigInt> rst = new HashSet<BigInt>();
            for (int a = 2; a <= 100; a++)
            {
                for (int b = 2; b <= 100; b++)
                {
                    rst.Add(BigInt.Power(a, b));
                    OutputLine(string.Format("adding: pow({0}, {1}), total: {2}", a, b, rst.Count));
                }
            }
            return rst.Count;
        }

        static long Prob030()
        {
            int ratio = 5;
            int ceiling = Prob030FindCeiling(ratio);
            long rst = 0;
            for (int n = 2; n < ceiling; n++)
            {
                if (n == Prob030CalcDigSum(n, ratio))
                {
                    rst += n;
                    OutputLine("found number: {0}", n);
                }
            }
            return rst;
        }

        static int Prob030CalcDigSum(int n, int ratio)
        {
            string nstr = n.ToString();
            int rst = 0;
            foreach (char c in nstr.ToCharArray())
            {
                rst += (int)Math.Pow((int)(c - '0'), ratio);
            }
            return rst;
        }

        static int Prob030FindCeiling(int ratio)
        {
            double ninePow = Math.Pow(9, ratio);
            int n = 1;
            while (ninePow * n > Prob030FindCeilingNineNum(n))
            {
                n++;
            }
            return (int)ninePow * (n - 1);
        }

        static int Prob030FindCeilingNineNum(int n)
        {
            string rst = "";
            for (int i = 0; i < n; i++)
            {
                rst += "9";
            }
            return Convert.ToInt32(rst);
        }

        static long Prob031()
        {
            int t = 200;
            int[] candis = { 200, 100, 50, 20, 10, 5, 2, 1 };
            Stack<int> s = new Stack<int>();
            List<int> counter = new List<int>();
            Prob031Recur(t, candis, s, counter);
            return counter.Count;
        }

        static void Prob031Recur(int t, int[] candis, Stack<int> s, List<int> counter)
        {
            foreach (int c in candis)
            {
                if (c <= t && (s.Count == 0 || c <= s.Peek()))
                {
                    s.Push(c);
                    if (c < t)
                    {
                        Prob031Recur(t - c, candis, s, counter);
                    }
                    else
                    {
                        counter.Add(1);
                        if (counter.Count % 100 == 0)
                        {
                            Output("Found 100 combination, and last one is: ");
                            foreach (int i in s)
                            {
                                Output("{0},", i);
                            }
                            OutputLine();
                        }
                    }
                    s.Pop();
                }
            }
        }

        static long Prob032()
        {
            long rst = 0;
            HashSet<int> s = new HashSet<int>();
            for (int a = 1; a < 9999; a++)
            {
                if (a % 100 == 0)
                {
                    OutputLine("working on {0} to {1}...", a, a + 100);
                }
                int limit = 9999 / a + 1;
                for (int b = 0; b < 999 && b < limit; b++)
                {
                    int c = a * b;
                    if (Prob032IsPandigital(a, b, c, s))
                    {
                        OutputLine("found pandigitals: {0} * {1} = {2}", a, b, c);
                        rst += c;
                    }
                }
            }
            return rst;
        }

        static bool Prob032IsPandigital(int a, int b, int c, HashSet<int> s)
        {
            if (s.Contains(c))
            {
                return false;
            }
            HashSet<char> ss = new HashSet<char>();
            char[] aa = a.ToString().ToCharArray();
            char[] ba = b.ToString().ToCharArray();
            char[] ca = c.ToString().ToCharArray();
            if (aa.Length + ba.Length + ca.Length != 9)
            {
                return false;
            }
            foreach (char chr in aa)
            {
                if (chr == '0')
                {
                    return false;
                }
                ss.Add(chr);
            }
            foreach (char chr in ba)
            {
                if (chr == '0')
                {
                    return false;
                }
                ss.Add(chr);
            }
            foreach (char chr in ca)
            {
                if (chr == '0')
                {
                    return false;
                }
                ss.Add(chr);
            }
            if (ss.Count == 9)
            {
                s.Add(c);
                return true;
            }
            else
            {
                return false;
            }
        }

        static long Prob033()
        {
            int numerator = 1;
            int denominator = 1;
            for (int a = 11; a < 99; a++)
            {
                if (a % 10 == 0)
                {
                    continue;
                }
                for (int b = a + 1; b < 99; b++)
                {
                    if (b % 10 == 0)
                    {
                        continue;
                    }
                    if (Prob033IsCurious(a, b))
                    {
                        numerator *= a;
                        denominator *= b;
                    }
                }
            }
            return Prob033MinFactor(numerator, denominator, denominator);
        }

        static bool Prob033IsCurious(int a, int b)
        {
            int numerator = Prob033MinFactor(a, b, a);
            int denominator = Prob033MinFactor(a, b, b);
            List<int> commonList = Prob033CommonFactorList(a, b);
            if (commonList.Count == 0)
            {
                return false;
            }
            foreach (int common in commonList)
            {
                int sa = Prob033RemoveCommonFactor(a, common), sb = Prob033RemoveCommonFactor(b, common);
                int numeratorS = Prob033MinFactor(sa, sb, sa);
                int denominatorS = Prob033MinFactor(sa, sb, sb);
                if (numerator == numeratorS && denominator == denominatorS)
                {
                    OutputLine("found 1 pair: {0}/{1}", a, b);
                    return true;
                }
            }
            return false;
        }

        static int Prob033RemoveCommonFactor(int n, int common)
        {
            string nstr = n.ToString();
            string commStr = common.ToString();
            string rst = nstr.Replace(commStr, "");
            if (rst.Equals(""))
            {
                rst = commStr;
            }
            return int.Parse(rst);
        }

        static List<int> Prob033CommonFactorList(int a, int b)
        {
            List<int> rst = new List<int>();
            string astr = a.ToString(), bstr = b.ToString();
            if (bstr.Contains(astr.Substring(0, 1)))
            {
                rst.Add(int.Parse(astr.Substring(0, 1)));
            }
            if (bstr.Contains(astr.Substring(1, 1)))
            {
                rst.Add(int.Parse(astr.Substring(1, 1)));
            }
            return rst;
        }

        static int Prob033MinFactor(int numerator, int demoninator, int n)
        {
            return (int)(n / Tools.GCD(numerator, demoninator));
        }

        static long Prob034()
        {
            Dictionary<char, int> factorials = new Dictionary<char, int>();
            for (int i = 0; i <= 9; i++)
            {
                factorials.Add((char)('0' + i), (int)Prob024Factorial(i));
            }
            int ceil = Prob034FindCeiling();
            long rst = 0;
            for (int i = 10; i < ceil; i++)
            {
                if (i % 10000 == 0)
                {
                    OutputLine("work from {0} to {1}...", i, i + 10000);
                }
                if (i == Prob034DigitSum(i, factorials))
                {
                    OutputLine("found 1 curious number: {0}", i);
                    rst += i;
                }
            }
            return rst;
        }

        static int Prob034DigitSum(int n, Dictionary<char, int> factorials)
        {
            int rst = 0;
            char[] na = n.ToString().ToCharArray();
            foreach (char c in na)
            {
                rst += factorials[c];
            }
            return rst;
        }

        static int Prob034FindCeiling()
        {
            int nineFactorial = (int)Prob024Factorial(9);
            int n = 1;
            while (nineFactorial * n > Prob030FindCeilingNineNum(n))
            {
                n++;
            }
            return (int)nineFactorial * (n - 1);
        }

        static long Prob035()
        {
            PrimeHelper.PrimeLimit = 1000000;
            return (from x in PrimeHelper.Primes where Prob035IsCirclePrime((int) x) select x).Count();
        }

        static bool Prob035IsCirclePrime(int n)
        {
            string nstr = n.ToString();
            for (int i = 0; i < nstr.Length; i++)
            {
                int nn = int.Parse(nstr);
                if (!PrimeHelper.IsPrime(nn)) return false;
                nstr = nstr.Length > 1 ? string.Concat(nstr.Substring(1, nstr.Length - 1), nstr.Substring(0, 1)) : nstr;
            }
            return true;
        }

        static long Prob036()
        {
            OutputLine(Convert.ToString(7, 2));
            long rst = 0;
            for (int i = 0; i < 1000000; i++)
            {
                if (i % 10000 == 0)
                {
                    OutputLine("work from {0} to {1}...", i, i + 10000);
                }
                if (Prob036IsPalindromic(i.ToString()) && Prob036IsPalindromic(Convert.ToString(i, 2)))
                {
                    OutputLine("found 1 double palindromic number: {0} ({1})", i, Convert.ToString(i, 2));
                    rst += i;
                }
            }
            return rst;
        }

        static bool Prob036IsPalindromic(string s)
        {
            for (int i = 0; i < s.Length / 2 + 1; i++)
            {
                if (s[i] != s[s.Length - i - 1])
                {
                    return false;
                }
            }
            return true;
        }

        static long Prob037()
        {
            long rst = 0;
            int counter = 0;
            PrimeHelper.PrimeLimit = 1000000;
            foreach (long p in PrimeHelper.Primes)
            {
                if (p <= 10) continue;
                if (!Prob037IsTruncatablePrime(p)) continue;
                rst += p;
                counter++;
                if (11 == counter) break;
            }
            return rst;
        }

        static bool Prob037IsTruncatablePrime(long n)
        {
            string nstr = n.ToString();
            for (int i = 0; i < nstr.Length; i++)
            {
                long left = long.Parse(nstr.Substring(i, nstr.Length - i));
                long right = long.Parse(nstr.Substring(0, nstr.Length - i));
                if (!PrimeHelper.IsPrime(left)) return false;
                if (!PrimeHelper.IsPrime(right)) return false;
            }
            return true;
        }

        static long Prob038()
        {
            long rst = 0;
            for (int i = 5000; i <= 9999; i++)
            {
                string digit = Prob038ConcatPan(new string[] { i.ToString(), (i * 2).ToString() });
                if (!Prob038IsPandigital(digit)) continue;
                OutputLine("found 1 concatenation: {0},(1..2)", i);
                rst = Math.Max(rst, long.Parse(digit));
            }
            for (int i = 100; i <= 333; i++)
            {
                string digit = Prob038ConcatPan(new string[] { i.ToString(), (i * 2).ToString(), (i * 3).ToString() });
                if (!Prob038IsPandigital(digit)) continue;
                OutputLine("found 1 concatenation: {0},(1..2)", i);
                rst = Math.Max(rst, long.Parse(digit));
            }
            return rst;
        }

        static string Prob038ConcatPan(string[] digits) 
        {
            return string.Join(string.Empty, digits);
        }

        static HashSet<char> prob038Cache = new HashSet<char>();

        static bool Prob038IsPandigital(string digit)
        {
            if (9 != digit.Length) return false;
            prob038Cache.Clear();
            foreach (char c in digit)
            {
                if ('0' == c || prob038Cache.Contains(c)) return false;
                prob038Cache.Add(c);
            }
            return true;
        }

        static long Prob039()
        {
            long rst = 0;
            int ceil = 1000;
            int curr = 0;
            for (int n = 12; n <= ceil; n++)
            {
                int trianglesN = Prob039ProbTriangle(n);
                if (trianglesN > curr)
                {
                    curr = trianglesN;
                    rst = n;
                }
            }
            return rst;
        }

        static int Prob039ProbTriangle(int n)
        {
            int triangleCount = 0;
            Output("Probing {0}: ", n);
            for (int a = 1; a < n / 3 + 1; a++)
            {
                for (int b = a; b < n / 2 + 1; b++)
                {
                    int c = n - a - b;
                    if ((a * a) + (b * b) == c * c)
                    {
                        triangleCount++;
                        Output("{0},{1},{2};", a, b, c);
                    }
                }
            }
            OutputLine("={0} triangles", triangleCount);
            return triangleCount;
        }

        static long Prob040()
        {
            StringBuilder s = new StringBuilder("");
            int counter = 1;
            while (s.Length < 1000000)
            {
                if (counter % 10000 == 0)
                {
                    OutputLine("work from {0} to {1}...", counter, counter + 10000);
                }
                s.Append(counter.ToString());
                counter++;
            }
            int[] nums = new int[] { (int)(s[1 - 1] - '0'), (int)(s[10 - 1] - '0'), (int)(s[100 - 1] - '0'), (int)(s[1000 - 1] - '0'), (int)(s[10000 - 1] - '0'), (int)(s[100000 - 1] - '0'), (int)(s[1000000 - 1] - '0') };
            long rst = 1;
            foreach (int n in nums)
            {
                Output("{0},", n);
                rst *= n;
            }
            return rst;
        }
    }
}
