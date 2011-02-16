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
        static long Prob041()
        {
            /************************************************************************/
            /* 9、8位的pandigital都可以被3整除，所以从7位开始（7654321）            */
            /************************************************************************/
            for (long n = 7654319; n > 1; n--)
            {
                if (Prob041IsPanDigital(n) && Tools.IsPrime(n))
                {
                    return n;
                }
            }
            return 0;
        }

        static bool Prob041IsPanDigital(long n)
        {
            HashSet<char> rst = new HashSet<char>();
            string s = n.ToString();
            foreach (char c in s)
            {
                if (c == '0' || c > ('0' + s.Length))
                {
                    return false;
                }
                rst.Add(c);
            }
            return rst.Count == s.Length;
        }

        static long Prob042()
        {
            long rst = 0;
            HashSet<int> triangleNums = Prob042InitTriangleNums(100);
            StreamReader r = new StreamReader(@"data\Prob042_words.txt");
            string line = r.ReadLine();
            line = line.Replace("\"", "");
            string[] a = line.Split(',');
            foreach (string word in a)
            {
                if (Prob042IsTriangleWord(word, triangleNums))
                {
                    rst++;
                }
            }
            return rst;
        }

        static bool Prob042IsTriangleWord(string word, HashSet<int> triangleNums)
        {
            int count = 0;
            foreach (char c in word)
            {
                count += c - 'A' + 1;
            }
            if (triangleNums.Contains(count))
            {
                OutputLine("found 1 triangle word: {0} for {1}", word, count);
                return true;
            }
            else
            {
                return false;
            }
        }

        static HashSet<int> Prob042InitTriangleNums(int ceil)
        {
            HashSet<int> rst = new HashSet<int>();
            for (int i = 1; i <= ceil; i++)
            {
                rst.Add((i + 1) * i / 2);
            }
            return rst;
        }

        static long Prob043()
        {
            int[] factors = new int[] { 17, 13, 11, 7, 5, 3, 2 };
            HashSet<string> results = new HashSet<string>();
            Prob043Recur(0, factors, new StringBuilder(""), results);
            long rst = 0;
            foreach (string s in results)
            {
                rst += long.Parse(s);
            }
            return rst;
        }

        static void Prob043Recur(int n, int[] factors, StringBuilder builder, HashSet<string> results)
        {
            if (n >= factors.Length)
            {
                string pandigital = Prob043ToPandigital(builder.ToString());
                if (!pandigital.Equals(""))
                {
                    results.Add(pandigital);
                    OutputLine(pandigital);
                }
                return;
            }
            int currNum = factors[n];
            while (currNum < 1000)
            {
                string currStr = string.Format("{0:D3}", currNum);
                bool connectable = false;
                if (n == 0)
                {
                    builder.Append(currStr);
                    Prob043Recur(n + 1, factors, builder, results);
                }
                else if (Prob043Connectable(builder, currStr))
                {
                    builder.Insert(0, currStr[0]);
                    connectable = true;
                    Prob043Recur(n + 1, factors, builder, results);
                }

                if (n == 0)
                {
                    builder.Remove(0, builder.Length);
                }
                else if (connectable)
                {
                    builder.Remove(0, 1);
                }
                currNum += factors[n];
            }
        }

        static string Prob043ToPandigital(string s)
        {
            HashSet<char> container = new HashSet<char>();
            foreach (char c in s)
            {
                container.Add(c);
            }
            if (container.Count != s.Length)
            {
                return "";
            }
            if (!container.Contains('0'))
            {
                return "";
            }
            for (char n = '0'; n <= '9'; n++)
            {
                if (!container.Contains(n))
                {
                    return n + s;
                }
            }
            return "";
        }

        static bool Prob043Connectable(StringBuilder builder, string curr)
        {
            return curr[2] == builder[1] && curr[1] == builder[0];
        }

        static long Prob044()
        {
            int size = 3000;
            List<long> pentagonalList = new List<long>(size);
            HashSet<long> pentagonalSet = new HashSet<long>();
            for (long n = 1; n <= size; n++)
            {
                long pentagonal = n * (3 * n - 1) / 2;
                pentagonalList.Add(pentagonal);
                pentagonalSet.Add(pentagonal);
            }
            long rst = long.MaxValue;
            for (int i = 1; i < size; i++)
            {
                if (i % 1000 == 0)
                {
                    OutputLine("work from {0} to {1}...", i, i + 1000);
                }
                for (int j = i + 1; j < size; j++)
                {
                    long sum = pentagonalList[i] + pentagonalList[j];
                    long diff = pentagonalList[j] - pentagonalList[i];
                    if (pentagonalSet.Contains(sum) && pentagonalSet.Contains(diff))
                    {
                        OutputLine("found 1 pair: {0}, {1}", pentagonalList[i], pentagonalList[j]);
                        if (diff < rst)
                        {
                            rst = diff;
                        }
                    }
                }
            }
            return rst;
        }

        static long Prob045()
        {
            long n = 286;
            while (true)
            {
                if (n % 100 == 0)
                {
                    OutputLine("work from {0} to {1}...", n, n + 100);
                }
                long t = n * (n + 1) / 2;
                if (Prob045IsPentagonal(t) && Prob045IsHexagonal(t))
                {
                    return t;
                }
                n++;
            }
        }

        static bool Prob045IsPentagonal(long t)
        {
            double n = (1 + Math.Sqrt(24 * t + 1)) / 6;
            return Prob045IsInteger(n);
        }

        static bool Prob045IsHexagonal(long t)
        {
            double n = (1 + Math.Sqrt(t * 8 + 1)) / 4;
            return Prob045IsInteger(n);
        }

        static bool Prob045IsInteger(double d)
        {
            return d % 1 < 0.0000001;
        }

        static long Prob046()
        {
            List<long> primes = new List<long>();
            primes.Add(2);
            long n = 3;
            while (true)
            {
                if (!Tools.IsPrime(n))
                {
                    if (!Prob046Decomposeable(n, primes))
                    {
                        return n;
                    }
                }
                else
                {
                    primes.Add(n);
                }
                n += 2;
            }
        }

        static bool Prob046Decomposeable(long prime, List<long> primes)
        {
            foreach (long p in primes)
            {
                double num = Math.Sqrt((prime - p) / 2);
                if (num > 0 && Prob045IsInteger(num))
                {
                    OutputLine("{0} is made up by: {1} + 2 * {2} * {2}", prime, p, (int)num);
                    return true;
                }
            }
            return false;
        }

        static long Prob047()
        {
            PrimeHelper.PrimeFloor = 150000;
            List<long> candidates = new List<long>();
            long n = 2;
            while (true)
            {
                if (n % 10000 == 0)
                {
                    OutputLine("work from {0} to {1}...", n, n + 10000);
                }
                if (candidates.Count == 4)
                {
                    return candidates[0];
                }
                if (Prob047CouldDecomposeTo4Primes(n))
                {
                    candidates.Add(n);
                }
                else
                {
                    candidates.Clear();
                }
                n++;
            }
        }

        static bool Prob047CouldDecomposeTo4Primes(long number)
        {
            return Tools.FindPrimeFactors2((int) number).Count == 4;
        }

        static long Prob048()
        {
            long rst = 0;
            long ceil = 10000000000;
            for (int n = 1; n <= 1000; n++)
            {
                long curr = 1;
                for (int m = 1; m <= n; m++)
                {
                    curr *= n;
                    curr %= ceil;
                }
                rst += curr;
            }
            return rst % ceil;
        }

        static long Prob049()
        {
            List<long> found = new List<long>();
            for (long n = 1488; n < 9999; n++ )
            {
                found.Clear();
                found.Add(n);
                foreach (List<char> ca in new Permutater<List<char>, char>(n.ToString().ToCharArray().ToList()))
                {
                    long nn = long.Parse(new string(ca.ToArray()));
                    if (!found.Contains(nn) && nn > found[0] && PrimeHelper.IsPrime(nn)) found.Add(nn);
                }
                if (3 != found.Count) continue;
                found.Sort();
                if (found[2] - found[1] != found[1] - found[0]) continue;
                break;
            }
            return long.Parse(string.Join(string.Empty, found.Select(x => x.ToString()).ToArray()));
        }

        static long Prob050_Old()
        {
            List<long> primes = new List<long>(80000);
            for (long n = 2; n <= 1000000; n++)
            {
                if (Tools.IsPrime(n))
                {
                    primes.Add(n);
                }
            }
            long rst = primes[0];
            int currLen = 0;
            for (int i = 12; i < primes.Count; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    long sum = 0;
                    int index = j;
                    while (sum < primes[i])
                    {
                        sum += primes[index];
                        index++;
                    }
                    if (sum != primes[i])
                    {
                        continue;
                    }
                    if (index - j <= currLen)
                    {
                        continue;
                    }
                    currLen = index - j;
                    rst = primes[i];
                    Output("{0} = {1}", primes[i], primes[j]);
                    for (int k = j + 1; k < index; k++)
                    {
                        Output(" + {0}", primes[k]);
                    }
                    OutputLine();
                }
            }
            return rst;
        }

        static long Prob050()
        {
            List<long> primes = new List<long>(80000);
            HashSet<long> primeSet = new HashSet<long>();
            for (long n = 2; n <= 1000000; n++)
            {
                if (Tools.IsPrime(n))
                {
                    primes.Add(n);
                    primeSet.Add(n);
                }
            }

            long rst = 2;
            int currLen = 0;
            for (int i = 0; i < primes.Count; i++)
            {
                long sum = 0;
                int index = i;
                while (sum < primes[primes.Count - 1])
                {
                    sum += primes[index];
                    index++;
                    if (!primeSet.Contains(sum) || index - i <= currLen)
                    {
                        continue;
                    }
                    currLen = index - i;
                    rst = sum;
                    Output("{0} = {1}", sum, primes[i]);
                    for (int k = i + 1; k < index; k++)
                    {
                        Output(" + {0}", primes[k]);
                    }
                    OutputLine(", ({0} consecutive)", index - i);
                }
            }
            return rst;
        }

        static long Prob051()
        {
            long begin = DateTime.Now.Ticks;
            long n = 11;
            int threshold = 8;
            HashSet<long> primes = new HashSet<long>();
            while (true)
            {
                if (!Prob051IsPrime(n, primes))
                {
                    n++;
                    continue;
                }
                int len = n.ToString().Length;
                //Output("Working on {0}...", n);
                for (int i = 1; i <= len - 1; i++)
                {
                    List<int[]> combinations = new List<int[]>();
                    Prob051Combination(len, i, 0, combinations, null);
                    foreach (int[] substitues in combinations)
                    {
                        if (Prob051IsThatPrime(n, substitues, threshold, primes))
                        {
                            long end = DateTime.Now.Ticks;
                            OutputLine("Time elapsed: {0}ms", (end - begin) / 10000);
                            return n;
                        }
                    }
                }
                n++;
            }
        }

        static bool Prob051IsThatPrime(long prime, int[] substitues, int threshold, HashSet<long> primes)
        {
            char[] ca = prime.ToString().ToCharArray();
            char tmp = ca[substitues[0]];
            foreach (int sub in substitues)
            {
                if (tmp != ca[sub])
                {
                    return false;
                }
            }
            int counter = 0;
            for (char c = '0'; c <= '9'; c++)
            {
                foreach (int sub in substitues)
                {
                    ca[sub] = c;
                }
                if ('0' == ca[0])
                {
                    continue;
                }
                if (Prob051IsPrime(long.Parse(new string(ca)), primes))
                {
                    counter++;
                }
            }
            return counter >= threshold;
        }

        static bool Prob051IsPrime(long n, HashSet<long> primes)
        {
            if (primes.Contains(n))
            {
                return true;
            }
            if (Tools.IsPrime(n))
            {
                primes.Add(n);
                return true;
            }
            else
            {
                return false;
            }
        }

        static void Prob051Combination(int m, int n, int p, List<int[]> rst, List<int> inter)
        {
            if (null == inter)
            {
                inter = new List<int>();
            }
            if (n == inter.Count)
            {
                int[] tmp = new int[n];
                for (int i = 0; i < inter.Count; i++)
                {
                    tmp[i] = inter[i];
                }
                rst.Add(tmp);
                return;
            }
            for (int i = p; i < m; i++)
            {
                if (!inter.Contains(i))
                {
                    inter.Add(i);
                    Prob051Combination(m, n, i + 1, rst, inter);
                    inter.Remove(i);
                }
            }
        }

        static long Prob052()
        {
            long begin = DateTime.Now.Ticks;
            long step = 10;
            HashSet<char> container = new HashSet<char>(), worker = new HashSet<char>();
            while (true)
            {
                long nextStep = step * 10;
                long currLimit = nextStep / 6;
                for (long n = step; n <= currLimit; n++)
                {
                    if (Prob052IsThatNumber(n))
                    {
                        long end = DateTime.Now.Ticks;
                        OutputLine("Time elapsed: {0}ms", (end - begin) / 10000);
                        return n;
                    }
                }
                step = nextStep;
            }
        }

        static bool Prob052IsThatNumber(long n)
        {
            HashSet<char> container = new HashSet<char>(), worker = new HashSet<char>();
            List<char> standard = n.ToString().ToCharArray().ToList();
            standard.Sort();
            for (int multiplier = 2; multiplier <= 6; multiplier++)
            {
                List<char> curr = (n * multiplier).ToString().ToCharArray().ToList();
                curr.Sort();
                if (standard.Count != curr.Count)
                {
                    return false;
                }
                for (int j = 0; j < standard.Count; j++)
                {
                    if (standard[j] != curr[j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        static long Prob053()
        {
            long begin = DateTime.Now.Ticks;
            long rst = 0;
            for (int n = 1; n <= 100; n++)
            {
                for (int r = 1; r < n; r++)
                {
                    if (Prob053Factorial(n) / Prob053Factorial(r) / Prob053Factorial(n - r) >= 1000000)
                    {
                        rst++;
                    }
                }
            }
            long end = DateTime.Now.Ticks;
            OutputLine("Time elapsed: {0}ms", (end - begin) / 10000);
            return rst;
        }

        static double Prob053Factorial(double n)
        {
            if (n == 1)
            {
                return 1;
            }
            return n * Prob053Factorial(n - 1);
        }

        static long Prob054()
        {
            long rst = 0;
            StreamReader r = new StreamReader(@"data\Prob054_poker.txt");
            string line = r.ReadLine();
            while (line != null)
            {
                string[] porkes1 = line.Substring(0, 14).Split(' ');
                string[] porkes2 = line.Substring(15).Split(' ');
                Prob054PorkerSuite ps1 = new Prob054PorkerSuite(porkes1);
                Prob054PorkerSuite ps2 = new Prob054PorkerSuite(porkes2);
                int comp = ps1.CompareTo(ps2);
                if (comp > 0) rst++;
                if (comp == 0) OutputLine(string.Format("p1:{0} -- p2:{1} = {2}", ps1.Resolver, ps2.Resolver, comp));
                line = r.ReadLine();
            }
            return rst;
        }

        static long Prob055()
        {
            long rst = 0;
            for (int i = 1; i < 10000; i++)
            {
                BigInt num = i;
                int counter = 0;
                while (counter < 50)
                {
                    num += Prob055ReverseString(num);
                    if (Tools.IsPalindromic(num))
                    {
                        break;
                    }
                    counter++;
                }
                if (50 == counter) OutputLine(i);
                rst += (counter == 50 ? 1 : 0);
            }
            return rst;
        }

        static BigInt Prob055ReverseString(BigInt n)
        {
            char[] ca = n.ToString().ToCharArray();
            Array.Reverse(ca);
            return new BigInt(new string(ca));
        }

        static long Prob056()
        {
            long rst = 0;
            for (int i = 1; i < 100; i++)
            {
                for (int j = 1; j < 100; j++)
                {
                    BigInt n = BigInt.Power(i, j);
                    long snd = Prob056SumNumberDigital(n.ToString());
                    if (snd > rst)
                    {
                        rst = snd;
                        OutputLine(string.Format("current top is {0}(for {1} composited by {2}pow{3})", rst, n, i, j));
                    }
                }
            }
            return rst;
        }

        static long Prob056SumNumberDigital(string n)
        {
            long rst = 0;
            char[] ca = n.ToCharArray();
            foreach (char c in ca)
            {
                rst += (c - '0');
            }
            return rst;
        }

        static long Prob057()
        {
            ContinuedFraction sqrtTwo = new ContinuedFraction(1, new SqrtExpandSequence((new int[] { 2 }).ToList()));
            long rst = 0;
            Fraction suffix = new Fraction(1, 2), ONE = Fraction.ONE, TWO = Fraction.Integer(2);
            int counter = 0;
            while (counter < 1000)
            {
                Fraction sqrt2 = ONE + suffix;
                if (sqrt2.Numerator.ToString().Length > sqrt2.Denominator.ToString().Length)
                {
                    rst++;
                    OutputLine(string.Format("{0}th,{1}", counter + 1, sqrt2.ToString()));
                }
                counter++;
                suffix = ONE / (TWO + suffix);
            }
            return rst;
        }

        static long Prob058()
        {
            long rst = 1;
            long node4 = 1, node3, node2, node1;
            long increment;
            double denominator = 1, numerator = 0;
            while (true)
            {
                rst += 2;
                denominator += 4;
                increment = rst - 1;
                node1 = node4 + increment;
                node2 = node1 + increment;
                node3 = node2 + increment;
                node4 = node3 + increment;
                numerator += PrimeHelper.IsPrime(node1) ? 1 : 0;
                numerator += PrimeHelper.IsPrime(node2) ? 1 : 0;
                numerator += PrimeHelper.IsPrime(node3) ? 1 : 0;
                numerator += PrimeHelper.IsPrime(node4) ? 1 : 0;
                double ratio = numerator / denominator;
                OutputLine(string.Format("size: {0}, ratio: {1:N6}", rst, ratio));
                if (0.1 > ratio) break;
            }
            return rst;
        }

        static long Prob059()
        {
            watch.Stop();
            byte[] cipher = Prob059ReadCipher(), plain = new byte[cipher.Length];
            long rst = 0;
            watch.Start();
            List<byte[]> candiKeyList = Prob059GenerateCandiKey();
            foreach (byte[] key in candiKeyList)
            {
                if (!Prob059Decrypt(cipher, plain, key)) continue;
                foreach (byte b in plain) rst += b;
                OutputLine(string.Format("Key: {0}\nContent:\n{1}", Encoding.ASCII.GetString(key), Encoding.ASCII.GetString(plain)));
                break;
            }
            return rst;
        }

        static bool Prob059Decrypt(byte[] cipher, byte[] plain, byte[] key)
        {
            for (int i = 0; i < cipher.Length; )
            {
                for (int j = 0; j < key.Length && i < cipher.Length; j++)
                {
                    byte b = (byte)(cipher[i] ^ key[j]);
                    if (Prob059IsInvalidChar(b)) return false;
                    plain[i] = b;
                    i++;
                }
            }
            return true;
        }

        static bool Prob059IsInvalidChar(byte b)
        {
            return 26 > b || 126 < b || '%' == b || '}' == b || '+' == b || '/' == b || '{' == b;
        }

        static byte[] Prob059ReadCipher()
        {
            StreamReader r = new StreamReader(@"data\Prob059_cipher1.txt");
            string line = r.ReadLine();
            string[] cipherStrArray = line.Split(',');
            byte[] cipher = new byte[cipherStrArray.Length];
            for (int i = 0; i < cipherStrArray.Length; i++)
            {
                cipher[i] = byte.Parse(cipherStrArray[i]);
            }
            return cipher;
        }

        static List<byte[]> Prob059GenerateCandiKey()
        {
            List<byte[]> rst = new List<byte[]>(26 * 26 * 26);
            for (char i = 'a'; i <= 'z'; i++)
            {
                for (char j = 'a'; j <= 'z'; j++)
                {
                    for (char k = 'a'; k <= 'z'; k++)
                    {
                        rst.Add(new byte[] { (byte)i, (byte)j, (byte)k });
                    }
                }
            }
            return rst;
        }

        static long Prob060()
        {
            long rst = long.MaxValue;
            int limit = 1051;
            HashSet<long> cache = new HashSet<long>();
            List<long> candidates = Prob060Candidates(limit, cache);
            int counter = 0;
            foreach (long c1 in candidates)
            {
                OutputLine(string.Format("working for {0}", ++counter));
                foreach (long c2 in candidates)
                {
                    if (c1 >= c2) continue;
                    if (!Prob060Check(c1, c2, cache)) continue;
                    foreach (long c3 in candidates)
                    {
                        if (c2 >= c3) continue;
                        if (!Prob060Check(c1, c3, cache)
                            || !Prob060Check(c2, c3, cache)
                            ) continue;
                        foreach (long c4 in candidates)
                        {
                            if (c3 >= c4) continue;
                            if (!Prob060Check(c1, c4, cache)
                                || !Prob060Check(c2, c4, cache)
                                || !Prob060Check(c3, c4, cache)
                                ) continue;
                            foreach (long c5 in candidates)
                            {
                                if (c4 >= c5) continue;
                                if (!Prob060Check(c1, c5, cache)
                                    || !Prob060Check(c2, c5, cache)
                                    || !Prob060Check(c3, c5, cache)
                                    || !Prob060Check(c4, c5, cache)
                                    ) continue;
                                OutputLine(string.Format("found primes: {0}, {1}, {2}, {3}, {4}", c1, c2, c3, c4, c5));
                                long tmp = c1 + c2 + c3 + c4 + c5;
                                rst = tmp < rst ? tmp : rst;
                            }
                        }
                    }
                }
            }
            return rst;
        }

        static bool Prob060Check(long c1, long c2, HashSet<long> cache)
        {
            return Prob051IsPrime(Prob060Concate(c1, c2), cache)
                && Prob051IsPrime(Prob060Concate(c2, c1), cache);
        }

        static long Prob060Concate(long c1, long c2) { return long.Parse(c1.ToString() + c2.ToString()); }

        static List<long> Prob060Candidates(int limit, HashSet<long> cache)
        {
            List<long> rst = new List<long>(limit);
            int counter = 0;
            long n = 1;
            while (limit > counter)
            {
                n++;
                if (Prob051IsPrime(n, cache))
                {
                    rst.Add(n);
                    counter++;
                }
            }
            return rst;
        }
    }
}
