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
using ProjectEulerSolvers.ThreadRunnerClasses;

namespace ProjectEulerSolvers
{
    partial class Program
    {
        static long Prob061()
        {
            int size = 6;
            watch.Reset(); watch.Start();
            Stack<long> rstStack = new Stack<long>();
            Stack<char> seriesStack = new Stack<char>();
            Dictionary<long, List<KeyValuePair<long, char>>> dict = new Dictionary<long, List<KeyValuePair<long, char>>>();
            List<long> triangles = Prob061PolygonList(new PolygonN(Tools.TriangleN));
            Prob061InitCandiMap(dict, Prob061PolygonList(new PolygonN(Tools.SquareN)), '4');
            Prob061InitCandiMap(dict, Prob061PolygonList(new PolygonN(Tools.PentagonalN)), '5');
            Prob061InitCandiMap(dict, Prob061PolygonList(new PolygonN(Tools.HexagonalN)), '6');
            Prob061InitCandiMap(dict, Prob061PolygonList(new PolygonN(Tools.HeptagonalN)), '7');
            Prob061InitCandiMap(dict, Prob061PolygonList(new PolygonN(Tools.OctagonalN)), '8');

            foreach (long n in triangles)
            {
                rstStack.Push(n);
                OutputLine("working on: {0}", n);
                if (Prob061Recur(dict, rstStack, seriesStack, size, Prob061Prefix(n))) break;
                rstStack.Pop();
            }
            long rst = 0;
            foreach (long n in rstStack) OutputLine(n);
            foreach (long n in rstStack) rst += n;
            return rst;
        }

        static bool Prob061Recur(Dictionary<long, List<KeyValuePair<long, char>>> dict, Stack<long> rstStack, Stack<char> seriesStack, int size, long prefix)
        {
            if (size == rstStack.Count && Prob061Suffix(rstStack.Peek()) == prefix) return true;
            long key = Prob061Suffix(rstStack.Peek());
            if (!dict.ContainsKey(key)) return false;
            foreach (KeyValuePair<long, char> pair in dict[key])
            {
                if (seriesStack.Contains(pair.Value)) continue;
                rstStack.Push(pair.Key);
                seriesStack.Push(pair.Value);
                if (Prob061Recur(dict, rstStack, seriesStack, size, prefix)) return true;
                seriesStack.Pop();
                rstStack.Pop();
            }
            return false;
        }

        static void Prob061InitCandiMap(Dictionary<long, List<KeyValuePair<long, char>>> dict, List<long> nums, char type)
        {
            foreach (long n in nums) Prob061PrepareCandiList(dict, Prob061Prefix(n)).Add(new KeyValuePair<long, char>(n, type));
        }

        static long Prob061Prefix(long n)
        {
            return (n - Prob061Suffix(n)) / 100;
        }

        static long Prob061Suffix(long n)
        {
            return n % 100;
        }

        static List<KeyValuePair<long, char>> Prob061PrepareCandiList(Dictionary<long, List<KeyValuePair<long, char>>> dict, long key)
        {
            if (!dict.ContainsKey(key)) dict.Add(key, new List<KeyValuePair<long, char>>());
            return dict[key];
        }

        static List<long> Prob061PolygonList(PolygonN algorithm)
        {
            List<long> rst = new List<long>();
            for (long n = 1; ; n++)
            {
                long pn = algorithm(n);
                if (1000 > pn) continue;
                if (9999 < pn) break;
                if ('0' == pn.ToString()[2]) continue;
                rst.Add(pn);
            }
            return rst;
        }

        static long Prob062()
        {
            Dictionary<string, List<long>> cubes = new Dictionary<string, List<long>>();
            string key = "";
            long l = 0;
            for (long n = 1; n <= 100000; n++)
            {
                long cube = n * n * n;
                List<char> nList = cube.ToString().ToCharArray().ToList<char>();
                nList.Sort();
                key = new string(nList.ToArray<char>());
                List<long> tail;
                if (cubes.ContainsKey(key))
                {
                    tail = cubes[key];
                    string exists = "";
                    foreach (long t in tail) exists += string.Format("{0},", t);
                    OutputLine("working on {0} made up by {1}, exists: {2}", cube, n, exists.Substring(0, exists.Length - 1));
                }
                else
                {
                    tail = new List<long>();
                    cubes.Add(key, tail);
                }
                tail.Add(n);
                if (5 <= tail.Count)
                {
                    l = tail[0];
                    break;
                }
            }
            return l * l * l;
        }

        static long Prob063()
        {
            long rst = 0;
            for (int exp = 1; ; exp++)
            {
                int counter = 0;
                for (int x = 1; x < 10; x++)
                {
                    BigInt pow = BigInt.Power(x, exp);
                    if (exp != pow.ToString().Length) continue;
                    counter++;
                    OutputLine("pow({0},{1})={2}", x, exp, pow);
                }
                if (0 == counter) break;
                rst += counter;
            }
            return rst;
        }

        static long Prob064()
        {
            HashSet<int> squares = new HashSet<int>(Enumerable.Range(1, 100).Select(x => x * x));
            return Enumerable.Range(1, 10000)
                .Where(n => !(new BigInt(n).IsPerfectSquare()) && 0 != (new Sqrt(n)).RatioCycle.Count % 2)
                .Count();
        }

        static long Prob065()
        {
            return new ContinuedFraction(2, new EExpandSequence())
                .ToFraction(99).Numerator.ToString().ToCharArray()
                .Select(x => (int)(x - '0')).Sum();
        }

        static long Prob066()
        {
            BigInt max = 0;
            long rst = 0;
            Enumerable.Range(1, 1000).Where(n => !(new BigInt(n).IsPerfectSquare()))
                .ToList<int>().ForEach(n => rst = Prob066MinimalPellSolution(n, ref max) ? n : rst);
            return rst;
        }

        static bool Prob066MinimalPellSolution(int n, ref BigInt rst)
        {
            Output("working on {0}: ", n);
            ContinuedFraction cf = new Sqrt(n).ContinuedFraction;
            int counter = 0;
            Fraction expend = cf.ToFraction(counter);
            BigInt x = expend.Numerator, y = expend.Denominator;
            while ((x * x) - (n * y * y) != 1)
            {
                counter++;
                expend = cf.ToFraction(counter);
                x = expend.Numerator; y = expend.Denominator;
            }
            OutputLine("{0}^2 - {1}*{2}^2 = 1", x, n, y);
            if (rst < x)
            {
                rst = x;
                return true;
            }
            else
            {
                return false;
            }
        }

        static long Prob067()
        {
            int size = 100;
            int mask = 100;
            int[,] data = new int[size, size];
            Prob018Init(@"data\Prob067_large_triangle.txt", data, size, mask);
            int[,] drst = new int[size, size];
            bool[,] solved = new bool[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    drst[i, j] = int.MaxValue;
                    solved[i, j] = false;
                }
            }
            drst[0, 0] = data[0, 0];
            solved[0, 0] = true;
            Prob018Dijkstra(data, drst, size, solved);
            Prob018DijkstraPrint(drst, size, mask);
            long min = int.MaxValue;
            for (int j = 0; j < size; j++)
            {
                if (drst[size - 1, j] < min)
                {
                    min = drst[size - 1, j];
                }
            }
            return (mask * size) - min;
        }

        //static int _prob068ResultSize = 9;
        //static int _prob068Size = 3;
        //static int[,] _prob068Indexes = new int[,] {
        //    { 4, 3, 2 },
        //    { 5, 1, 3 },
        //    { 6, 2, 1 } };
        static int _prob068ResultSize = 16;
        static int _prob068Size = 5;
        static int[,] _prob068Indexes = new int[,] {
            { 6, 5, 4 },
            { 7, 1, 5 },
            { 8, 2, 1 },
            { 9, 3, 2 },
            { 10, 4, 3 } };

        static long Prob068()
        {
            long rst = 0;
            foreach (List<int> ring in new Permutater<List<int>, int>(Enumerable.Range(1, _prob068Size * 2).ToList()))
            {
                if (!Prob068IsMagicRing(ring)) continue;
                long num = Prob068MakeRingNumber(ring);
                if (_prob068ResultSize != num.ToString().Length) continue;
                OutputLine(num);
                rst = Math.Max(rst, num);
            }
            return rst;
        }

        static bool Prob068IsMagicRing(List<int> ring)
        {
            int tmp = Prob068SumBranch(ring, 1);
            for (int i = 2; i <= _prob068Size; i++ )
            {
                if (tmp != Prob068SumBranch(ring, i)) return false;
            }
            return true;
        }

        static int Prob068SumBranch(List<int> ring, int index)
        {
            return ring[_prob068Indexes[index - 1, 0] - 1]
                + ring[_prob068Indexes[index - 1, 1] - 1]
                + ring[_prob068Indexes[index - 1, 2] - 1];
        }

        static long Prob068MakeRingNumber(List<int> ring)
        {
            string rst = "";
            int start = Prob068FindRingNumberStartPosition(ring);
            for (int i = start; i >= 0; i-- )
            {
                rst += Prob068ConcatenateBranch(ring, i);
            }
            for (int j = _prob068Size - 1; j > start; j--)
            {
                rst += Prob068ConcatenateBranch(ring, j);
            }
            return long.Parse(rst);
        }

        static string Prob068ConcatenateBranch(List<int> ring, int branch)
        {
            return string.Format("{0}{1}{2}", 
                ring[_prob068Indexes[branch, 0] - 1],
                ring[_prob068Indexes[branch, 1] - 1],
                ring[_prob068Indexes[branch, 2] - 1]);
        }

        static int Prob068FindRingNumberStartPosition(List<int> ring)
        {
            int rst = _prob068Size;
            foreach (int p in Enumerable.Range(_prob068Size, _prob068Size).ToList())
            {
                if (ring[p] >= ring[rst]) continue;
                rst = p;
            }
            return rst - _prob068Size;
        }

        /// <summary>
        /// Let N = P1^A1*P2^A2*...*Pn^An. P1 < P2 < ... < Pn
        /// Then Phi(N) = (P1^A1 - P1^(A1 - 1))*(P2^A2 - P2^(A2 - 1))*...*(Pn^An - Pn^(An - 1))
        /// So N/Phi(N) = P1/(P1 - 1)*P2/(P2 - 1)*...*Pn/(Pn - 1)
        /// Say we can receive largest result when A1 = A2 = A3 = ... = An
        /// So the number should be product of first K primes
        /// </summary>
        /// <returns></returns>
        static long Prob069()
        {
            long rst = 1;
            for (long p = 2; ; p++ )
            {
                if (!PrimeHelper.IsPrime(p)) continue;
                if (rst * p > 1000000) break;
                rst *= p;
            }
            return rst;
        }

        static long Prob069BruteForce()
        {
            long rst = 2;
            double maxRatio = 2;
            for (long n = 2; n <= 1000000; n++)
            {
                double ratio = n / (double)Tools.EulerPhi((int)n);
                if (ratio <= maxRatio) continue;
                maxRatio = ratio;
                rst = n;
                OutputLine("Current max ratio is {0} from {1}", ratio, n);
            }
            return rst;
        }

        static long Prob069ThreadingBruteForce()
        {
            return TRProb069.Calculate(20000);
        }

        /// <summary>
        /// We can reduce our search significantly by selecting prime pairs (p1, p2 ) and calculate n as n = p1 x p2. 
        /// This allows the totient to be calculated as φ(n) = (p1-1)(p2-1) for n. 
        /// Now, just find the minimum ratio n/φ(n) for those n and φ(n) that are permutations of one another.
        /// The range of primes was selected by taking the square root of the upper bound, 10,000,000, 
        /// which is about 3162 and taking ±30% for a range of primes from 2000 to 4000 (247 primes).
        /// 
        /// The minimal solution for n/phi(n) would be if n was prime giving n/(n-1) but since n-1 never is a permutation of n it cannot be prime. 
        /// The next best thing would be if n only consisted of 2 prime factors close to (in this case) sqrt(10000000).
        /// </summary>
        /// <returns></returns>
        static long Prob070()
        {
            List<long> candis = Prob070FindPrimeFactors();
            long upperBound = 10000000;
            long rst = 1;
            double minRatio = double.MaxValue;
            foreach (long l1 in candis)
            {
                foreach (long l2 in candis)
                {
                    long n = l1 * l2;
                    if (n >= upperBound) break;
                    long phiN = (l1 - 1) * (l2 - 1);
                    double ratio = ((double) n) / ((double) phiN);
                    if (ratio > minRatio) continue;
                    if (!Tools.IsPermutation(n, phiN)) continue;
                    minRatio = ratio;
                    rst = n;
                }
            }
            return rst;
        }

        static List<long> Prob070FindPrimeFactors()
        {
            List<long> rst = new List<long>();
            for (long i = 2003; i < 4000; i++) { if (PrimeHelper.IsPrime(i)) rst.Add(i); }
            return rst;
        }

        /// <summary>
        /// With brute force
        /// </summary>
        /// <returns></returns>
        static long Prob071()
        {
            long rst = 0, dd = 0;
            double limit = 3 / 7d;
            double minDiff = double.MaxValue;
            for (double d = 1000000; d > 7; d -= 1 )
            {
                if ((((int)d) % 10000) == 0) OutputLine(string.Format("working on d={0}, current n is {1} and d is {2}", d, rst, dd));
                int n = (int) Math.Floor(3 * d / 7);
                if ((((int)d) % 7) == 0) n--;
                while (1 != Tools.GCD(n, (int)d)) n--;
                double f = n / d;
                double diff = limit - f;
                if (diff < 0) break;
                if (diff > minDiff) continue;
                minDiff = diff;
                rst = n;
                dd = (long)d;
            }
            return rst;
        }

        static long Prob072()
        {
            PrimeHelper.PrimeFloor = 1000000;
            return TRProb072.Calculate(20000);
        }

        static long Prob072BruteForce()
        {
            PrimeHelper.PrimeLimit = 1000000;
            long rst = 0;
            (from x in Enumerable.Range(2, 1000000 - 1) select Tools.EulerPhi(x)).ToList().ForEach(x => rst += x);
            return rst;
        }

        static long Prob073()
        {
            return TRProb073.Calculate(200);
        }

        static long Prob073SingleThread()
        {
            long rst = 0;
            for (int n = 4; n <= 12000; n++ )
            {
                int ceil = (int) Math.Floor(n / 2.0);
                int floor = (int) Math.Ceiling(n / 3.0);
                for (int p = floor; p <= ceil; p++ )
                {
                    rst += 1 == Tools.GCD(n, p) ? 1 : 0;
                }
            }
            return rst;
        }


        static Dictionary<char, int> prob074FactorialRegister = new Dictionary<char, int>();
        static Dictionary<int, int> prob074LenRegister = new Dictionary<int, int>();
        static HashSet<int> prob074PathRegister = new HashSet<int>();

        static long Prob074()
        {
            return TRProb074.Calculate(20000);
        }

        private static int max = 1500000;
        private static byte[] count = new byte[max + 1];

        static long Prob075()
        {
            Prob075Transform(3, 4, 5);
            long total = 0;
            for (int i = 0; i <= max; i++) if (count[i] == 1) total++;
            return total;
        }

        private static void Prob075Transform(int x, int y, int z)
        {
            int len = x + y + z;
            if (len > max) return;
            for (int l = len; l <= max; l += len) count[l]++;
            Prob075Transform(x - 2 * y + 2 * z, 2 * x - y + 2 * z, 2 * x - 2 * y + 3 * z);
            Prob075Transform(x + 2 * y + 2 * z, 2 * x + y + 2 * z, 2 * x + 2 * y + 3 * z);
            Prob075Transform(-x + 2 * y + 2 * z, -2 * x + y + 2 * z, -2 * x + 2 * y + 3 * z);
        }

        static long Prob076()
        {
            return Prob076Dcompose(40);
        }

        static long Prob076Dcompose(int n)
        {
            if (1 == n) return 0;
            return (from x in Enumerable.Range(1, n / 2) select Prob076Dcompose(x) + Prob076Dcompose(n - x) + 1).Sum();
        }
    }
}
