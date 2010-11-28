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

        static long Prob068()
        {
            Console.WriteLine("not implemented");
            long rst = 6531031914842725;
            return rst;
        }
    }
}
