using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Threading;

namespace ProjectEulerSolvers
{
    delegate long Executor();

    class Program
    {
        static Stopwatch watch = new Stopwatch();

        static void outputLine(string s) { watch.Stop(); Console.WriteLine(s); watch.Start(); }
        static void output(string s) { watch.Stop(); Console.Write(s); watch.Start(); }

        static void Main(string[] args)
        {
            Executor e = new Executor(prob057);
            watch.Start();
            long rst = e();
            watch.Stop();
            Console.WriteLine("anwser of {1} is: {0:D}", rst, e.Method.Name);
            Console.WriteLine("time cost is: {0}ms", watch.ElapsedMilliseconds);
        }

        static long prob001()
        {
            long counter = 0;
            for (int i = 1; i < 1000; i++ )
            {
                if (i % 3 == 0 || i % 5 == 0)
                {
                    counter += i;
                }
            }
            return counter;
        }

        static long prob002()
        {
            long counter = 0;
            long fab0 = 1;
            long fab1 = 1;
            long tmp = 0;
            while (fab1 <= 4000000)
            {
                counter += (fab1 % 2 == 0) ? fab1 : 0;
                tmp = fab0 + fab1;
                fab0 = fab1;
                fab1 = tmp;
            }
            return counter;
        }

        static long prob003()
        {
            long number = 600851475143L;
            int divisor = 2;
            while (number > 1)
            {
                if (0 == (number % divisor))
                {
                    number /= divisor;
                    divisor--;
                }
                divisor++;
            }
            return divisor;
        }

        static long prob004()
        {
            long ceil = 999 * 999;
            for (long i = ceil; i > 0; i-- )
            {
                if (prob004IsPalindromic((int) i) && prob004IsResolvable((int) i))
                {
                    return i;
                }
            }
            return 1;
        }

        static bool prob004IsResolvable(int num)
        {
            for (int i = 999; i > 99; i-- )
            {
                if ((num % i == 0) && (num / i > 99 && num / i < 1000))
                {
                    Console.WriteLine("{0:D} = {1:D} * {2:D}", num, i, num / i);
                    return true;
                }
            }
            return false;
        }

        static bool prob004IsPalindromic(int num)
        {
            return prob004IsPalindromic(Convert.ToString(num));
        }

        static bool prob004IsPalindromic(string s)
        {
            int count = s.Length == 1 ? 0 : (s.Length % 2 == 0) ? (s.Length / 2) : ((s.Length / 2) + 1);
            for (int i = 0; i <= count; i++)
            {
                if (s[i] != s[s.Length - i - 1])
                {
                    return false;
                }
            }
            return true;
        }

        static long prob005()
        {
            return prob005Recur(20);
        }

        static long prob005Recur(int n)
        {
            if (n == 1)
            {
                return 1;
            }
            return Tools.LCM(n, prob005Recur(n - 1));
        }

        static long prob005GCD(long a, long b)
        {
            if (a % b == 0) return b;
            return prob005GCD(b, a % b);
        }

        static long prob006()
        {
            long sumSquare = 0;
            long squareSum = 0;
            for (int i = 1; i <= 100; i++ )
            {
                sumSquare += (i * i);
                squareSum += i;
            }
            squareSum = squareSum * squareSum;
            return squareSum - sumSquare;
        }

        static long prob007()
        {
            int counter = 0;
            long n = 1;
            while (counter < 10001)
            {
                n++;
                if (prob007IsPrime(n))
                {
                    counter++;
                }
            }
            return n;
        }

        static bool prob007IsPrime(long n)
        {
            double k;
            if (n == 2 || n == 3)
            {
                return true;
            }
            if (n == 1)
            {
                return false;
            }
            k = Math.Sqrt(n) + 1;
            for (int i = 2; i <= k; i++ )
            {
                if (n % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        static long prob008()
        {
            int[] num = {7,3,1,6,7,1,7,6,5,3,1,3,3,0,6,2,4,9,1,9,2,2,5,1,1,9,6,7,4,4,2,6,5,7,4,7,4,2,3,5,5,3,4,9,1,9,4,9,3,4,9,6,9,8,3,5,2,0,3,1,2,7,7,4,5,0,6,3,2,6,2,3,9,5,7,8,3,1,8,0,1,6,9,8,4,8,0,1,8,6,9,4,7,8,8,5,1,8,4,3,8,5,8,6,1,5,6,0,7,8,9,1,1,2,9,4,9,4,9,5,4,5,9,5,0,1,7,3,7,9,5,8,3,3,1,9,5,2,8,5,3,2,0,8,8,0,5,5,1,1,1,2,5,4,0,6,9,8,7,4,7,1,5,8,5,2,3,8,6,3,0,5,0,7,1,5,6,9,3,2,9,0,9,6,3,2,9,5,2,2,7,4,4,3,0,4,3,5,5,7,6,6,8,9,6,6,4,8,9,5,0,4,4,5,2,4,4,5,2,3,1,6,1,7,3,1,8,5,6,4,0,3,0,9,8,7,1,1,1,2,1,7,2,2,3,8,3,1,1,3,6,2,2,2,9,8,9,3,4,2,3,3,8,0,3,0,8,1,3,5,3,3,6,2,7,6,6,1,4,2,8,2,8,0,6,4,4,4,4,8,6,6,4,5,2,3,8,7,4,9,3,0,3,5,8,9,0,7,2,9,6,2,9,0,4,9,1,5,6,0,4,4,0,7,7,2,3,9,0,7,1,3,8,1,0,5,1,5,8,5,9,3,0,7,9,6,0,8,6,6,7,0,1,7,2,4,2,7,1,2,1,8,8,3,9,9,8,7,9,7,9,0,8,7,9,2,2,7,4,9,2,1,9,0,1,6,9,9,7,2,0,8,8,8,0,9,3,7,7,6,6,5,7,2,7,3,3,3,0,0,1,0,5,3,3,6,7,8,8,1,2,2,0,2,3,5,4,2,1,8,0,9,7,5,1,2,5,4,5,4,0,5,9,4,7,5,2,2,4,3,5,2,5,8,4,9,0,7,7,1,1,6,7,0,5,5,6,0,1,3,6,0,4,8,3,9,5,8,6,4,4,6,7,0,6,3,2,4,4,1,5,7,2,2,1,5,5,3,9,7,5,3,6,9,7,8,1,7,9,7,7,8,4,6,1,7,4,0,6,4,9,5,5,1,4,9,2,9,0,8,6,2,5,6,9,3,2,1,9,7,8,4,6,8,6,2,2,4,8,2,8,3,9,7,2,2,4,1,3,7,5,6,5,7,0,5,6,0,5,7,4,9,0,2,6,1,4,0,7,9,7,2,9,6,8,6,5,2,4,1,4,5,3,5,1,0,0,4,7,4,8,2,1,6,6,3,7,0,4,8,4,4,0,3,1,9,9,8,9,0,0,0,8,8,9,5,2,4,3,4,5,0,6,5,8,5,4,1,2,2,7,5,8,8,6,6,6,8,8,1,1,6,4,2,7,1,7,1,4,7,9,9,2,4,4,4,2,9,2,8,2,3,0,8,6,3,4,6,5,6,7,4,8,1,3,9,1,9,1,2,3,1,6,2,8,2,4,5,8,6,1,7,8,6,6,4,5,8,3,5,9,1,2,4,5,6,6,5,2,9,4,7,6,5,4,5,6,8,2,8,4,8,9,1,2,8,8,3,1,4,2,6,0,7,6,9,0,0,4,2,2,4,2,1,9,0,2,2,6,7,1,0,5,5,6,2,6,3,2,1,1,1,1,1,0,9,3,7,0,5,4,4,2,1,7,5,0,6,9,4,1,6,5,8,9,6,0,4,0,8,0,7,1,9,8,4,0,3,8,5,0,9,6,2,4,5,5,4,4,4,3,6,2,9,8,1,2,3,0,9,8,7,8,7,9,9,2,7,2,4,4,2,8,4,9,0,9,1,8,8,8,4,5,8,0,1,5,6,1,6,6,0,9,7,9,1,9,1,3,3,8,7,5,4,9,9,2,0,0,5,2,4,0,6,3,6,8,9,9,1,2,5,6,0,7,1,7,6,0,6,0,5,8,8,6,1,1,6,4,6,7,1,0,9,4,0,5,0,7,7,5,4,1,0,0,2,2,5,6,9,8,3,1,5,5,2,0,0,0,5,5,9,3,5,7,2,9,7,2,5,7,1,6,3,6,2,6,9,5,6,1,8,8,2,6,7,0,4,2,8,2,5,2,4,8,3,6,0,0,8,2,3,2,5,7,5,3,0,4,2,0,7,5,2,9,6,3,4,5,0};
            long rst = 0;
            long tmp = 0;
            for (int i = 0; i <= num.Length - 5; i++ )
            {
                tmp = num[i] * num[i + 1] * num[i + 2] * num[i + 3] * num[i + 4];
                if (tmp > rst)
                {
                    rst = tmp;
                }
            }
            return rst;
        }

        static long prob009()
        {
            int a = 1, b, c;
            while (a < 333)
            {
                b = a + 1;
                while (b < 500)
                {
                    c = 1000 - b - a;
                    if ((a * a) + (b * b) == (c * c))
                    {
                        Console.WriteLine("{0:D},{1:D},{2:D}", a, b, c);
                        return a * b * c;
                    }
                    b++;
                }
                a++;
            }
            return 0;
        }

        static long prob010()
        {
            long n = 2;
            long sum = 0;
            while (n < 2000000)
            {
                if (prob007IsPrime(n))
                {
                    sum += n;
                }
                n++;
            }
            return sum;
        }

        static long prob011()
        {
            int[,] input = new int[,] { { 08, 02, 22, 97, 38, 15, 00, 40, 00, 75, 04, 05, 07, 78, 52, 12, 50, 77, 91, 08 }, { 49, 49, 99, 40, 17, 81, 18, 57, 60, 87, 17, 40, 98, 43, 69, 48, 04, 56, 62, 00 }, { 81, 49, 31, 73, 55, 79, 14, 29, 93, 71, 40, 67, 53, 88, 30, 03, 49, 13, 36, 65 }, { 52, 70, 95, 23, 04, 60, 11, 42, 69, 24, 68, 56, 01, 32, 56, 71, 37, 02, 36, 91 }, { 22, 31, 16, 71, 51, 67, 63, 89, 41, 92, 36, 54, 22, 40, 40, 28, 66, 33, 13, 80 }, { 24, 47, 32, 60, 99, 03, 45, 02, 44, 75, 33, 53, 78, 36, 84, 20, 35, 17, 12, 50 }, { 32, 98, 81, 28, 64, 23, 67, 10, 26, 38, 40, 67, 59, 54, 70, 66, 18, 38, 64, 70 }, { 67, 26, 20, 68, 02, 62, 12, 20, 95, 63, 94, 39, 63, 08, 40, 91, 66, 49, 94, 21 }, { 24, 55, 58, 05, 66, 73, 99, 26, 97, 17, 78, 78, 96, 83, 14, 88, 34, 89, 63, 72 }, { 21, 36, 23, 09, 75, 00, 76, 44, 20, 45, 35, 14, 00, 61, 33, 97, 34, 31, 33, 95 }, { 78, 17, 53, 28, 22, 75, 31, 67, 15, 94, 03, 80, 04, 62, 16, 14, 09, 53, 56, 92 }, { 16, 39, 05, 42, 96, 35, 31, 47, 55, 58, 88, 24, 00, 17, 54, 24, 36, 29, 85, 57 }, { 86, 56, 00, 48, 35, 71, 89, 07, 05, 44, 44, 37, 44, 60, 21, 58, 51, 54, 17, 58 }, { 19, 80, 81, 68, 05, 94, 47, 69, 28, 73, 92, 13, 86, 52, 17, 77, 04, 89, 55, 40 }, { 04, 52, 08, 83, 97, 35, 99, 16, 07, 97, 57, 32, 16, 26, 26, 79, 33, 27, 98, 66 }, { 88, 36, 68, 87, 57, 62, 20, 72, 03, 46, 33, 67, 46, 55, 12, 32, 63, 93, 53, 69 }, { 04, 42, 16, 73, 38, 25, 39, 11, 24, 94, 72, 18, 08, 46, 29, 32, 40, 62, 76, 36 }, { 20, 69, 36, 41, 72, 30, 23, 88, 34, 62, 99, 69, 82, 67, 59, 85, 74, 04, 36, 16 }, { 20, 73, 35, 29, 78, 31, 90, 01, 74, 31, 49, 71, 48, 86, 81, 16, 23, 57, 05, 54 }, { 01, 70, 54, 71, 83, 51, 54, 69, 16, 92, 33, 48, 61, 43, 52, 01, 89, 19, 67, 48 }};
            long rst = 0;
            for (int i = 0; i < 20; i++ )
            {
                for (int j = 0; j < 20; j++)
                {
                    rst = Math.Max(rst, prob011PointVal(input, i, j));
                }
            }
            return rst;
        }

        static long prob011PointVal(int[,] input, int i, int j)
        {
            long bottomLeft = 0, bottom = 0, bottomRight = 0, right = 0;
            if (i > 2 && j < 17)
            {
                bottomLeft = input[i, j] * input[i - 1, j + 1] * input[i - 2, j + 2] * input[i - 3, j + 3];
            }
            if (j < 17)
            {
                bottom = input[i, j] * input[i, j + 1] * input[i, j + 2] * input[i, j + 3];
            }
            if (i < 17 && j < 17)
            {
                bottomRight = input[i, j] * input[i + 1, j + 1] * input[i + 2, j + 2] * input[i + 3, j + 3];
            }
            if (i < 17)
            {
                right = input[i, j] * input[i + 1, j] * input[i + 2, j] * input[i + 3, j];
            }
            return Math.Max(bottomLeft, Math.Max(bottom, Math.Max(bottomRight, right)));
        }

        static long prob012()
        {
            int i = 1;
            long rst = prob012TriangleN(i);
            while(prob012DivisorCount(rst) < 500)
            {
                i++;
                rst = prob012TriangleN(i);
            }
            return rst;
        }

        static long prob012TriangleN(int n)
        {
            long rst = 0;
            for (int i = 1; i <= n; i++)
            {
                rst += i;
            }
            return rst;
        }

        static int prob012DivisorCount(long n)
        {
            int rst = 0;
            for (long i = 1; i <= (n / 2 + 1) && i < n; i++ )
            {
                if (n % i == 0)
                {
                    rst++;
                }
            }
            rst++;
            Console.WriteLine("{0:D},{1:D}", n, rst);
            return rst;
        }

        static string prob013()
        {
            string[] vals = { "37107287533902102798797998220837590246510135740250", "46376937677490009712648124896970078050417018260538", "74324986199524741059474233309513058123726617309629", "91942213363574161572522430563301811072406154908250", "23067588207539346171171980310421047513778063246676", "89261670696623633820136378418383684178734361726757", "28112879812849979408065481931592621691275889832738", "44274228917432520321923589422876796487670272189318", "47451445736001306439091167216856844588711603153276", "70386486105843025439939619828917593665686757934951", "62176457141856560629502157223196586755079324193331", "64906352462741904929101432445813822663347944758178", "92575867718337217661963751590579239728245598838407", "58203565325359399008402633568948830189458628227828", "80181199384826282014278194139940567587151170094390", "35398664372827112653829987240784473053190104293586", "86515506006295864861532075273371959191420517255829", "71693888707715466499115593487603532921714970056938", "54370070576826684624621495650076471787294438377604", "53282654108756828443191190634694037855217779295145", "36123272525000296071075082563815656710885258350721", "45876576172410976447339110607218265236877223636045", "17423706905851860660448207621209813287860733969412", "81142660418086830619328460811191061556940512689692", "51934325451728388641918047049293215058642563049483", "62467221648435076201727918039944693004732956340691", "15732444386908125794514089057706229429197107928209", "55037687525678773091862540744969844508330393682126", "18336384825330154686196124348767681297534375946515", "80386287592878490201521685554828717201219257766954", "78182833757993103614740356856449095527097864797581", "16726320100436897842553539920931837441497806860984", "48403098129077791799088218795327364475675590848030", "87086987551392711854517078544161852424320693150332", "59959406895756536782107074926966537676326235447210", "69793950679652694742597709739166693763042633987085", "41052684708299085211399427365734116182760315001271", "65378607361501080857009149939512557028198746004375", "35829035317434717326932123578154982629742552737307", "94953759765105305946966067683156574377167401875275", "88902802571733229619176668713819931811048770190271", "25267680276078003013678680992525463401061632866526", "36270218540497705585629946580636237993140746255962", "24074486908231174977792365466257246923322810917141", "91430288197103288597806669760892938638285025333403", "34413065578016127815921815005561868836468420090470", "23053081172816430487623791969842487255036638784583", "11487696932154902810424020138335124462181441773470", "63783299490636259666498587618221225225512486764533", "67720186971698544312419572409913959008952310058822", "95548255300263520781532296796249481641953868218774", "76085327132285723110424803456124867697064507995236", "37774242535411291684276865538926205024910326572967", "23701913275725675285653248258265463092207058596522", "29798860272258331913126375147341994889534765745501", "18495701454879288984856827726077713721403798879715", "38298203783031473527721580348144513491373226651381", "34829543829199918180278916522431027392251122869539", "40957953066405232632538044100059654939159879593635", "29746152185502371307642255121183693803580388584903", "41698116222072977186158236678424689157993532961922", "62467957194401269043877107275048102390895523597457", "23189706772547915061505504953922979530901129967519", "86188088225875314529584099251203829009407770775672", "11306739708304724483816533873502340845647058077308", "82959174767140363198008187129011875491310547126581", "97623331044818386269515456334926366572897563400500", "42846280183517070527831839425882145521227251250327", "55121603546981200581762165212827652751691296897789", "32238195734329339946437501907836945765883352399886", "75506164965184775180738168837861091527357929701337", "62177842752192623401942399639168044983993173312731", "32924185707147349566916674687634660915035914677504", "99518671430235219628894890102423325116913619626622", "73267460800591547471830798392868535206946944540724", "76841822524674417161514036427982273348055556214818", "97142617910342598647204516893989422179826088076852", "87783646182799346313767754307809363333018982642090", "10848802521674670883215120185883543223812876952786", "71329612474782464538636993009049310363619763878039", "62184073572399794223406235393808339651327408011116", "66627891981488087797941876876144230030984490851411", "60661826293682836764744779239180335110989069790714", "85786944089552990653640447425576083659976645795096", "66024396409905389607120198219976047599490197230297", "64913982680032973156037120041377903785566085089252", "16730939319872750275468906903707539413042652315011", "94809377245048795150954100921645863754710598436791", "78639167021187492431995700641917969777599028300699", "15368713711936614952811305876380278410754449733078", "40789923115535562561142322423255033685442488917353", "44889911501440648020369068063960672322193204149535", "41503128880339536053299340368006977710650566631954", "81234880673210146739058568557934581403627822703280", "82616570773948327592232845941706525094512325230608", "22918802058777319719839450180888072429661980811197", "77158542502016545090413245809786882778948721859617", "72107838435069186155435662884062257473692284509516", "20849603980134001723930671666823555245252804609722", "53503534226472524250874054075591789781264330331690" };
            string rst = "";
            foreach (string val in vals)
            {
                rst = prob013BignumSum(rst, val);
            }
            return rst.Substring(0, 10);
        }

        static string prob013BignumSum(string n1, string n2)
        {
            int loop = Math.Max(n1.Length, n2.Length);
            char carry = (char) 0;
            string rst = "";
            char[] n1a = n1.ToCharArray();
            char[] n2a = n2.ToCharArray();
            Array.Reverse(n1a);
            Array.Reverse(n2a);
            for (int i = 0; i < loop; i++ )
            {
                char sum = (char) ((i < n1a.Length ? n1a[i] : '0') + (i < n2a.Length ? n2a[i] : '0') + carry - '0');
                if (sum > '9')
                {
                    sum = (char) (sum - 10);
                    carry = (char) 1;
                }
                else
                {
                    carry = (char) 0;
                }
                rst = sum + rst;
            }
            if (carry > 0)
            {
                rst = '1' + rst;
            }
            //Console.WriteLine("{0}\n+{1}=\n{2}", n1, n2, rst);
            return rst;
        }

        static long prob014()
        {
            long rst = 0;
            int currCount = 0;
            for (long i = 999999; i > 0; i-- )
            {
                int tmpCount = prob014CountCollatz(i);
                if (currCount < tmpCount)
                {
                    rst = i;
                    currCount = tmpCount;
                    Console.WriteLine("Current longest is {0:D} for {1:D} steps", rst, currCount);
                }
            }
            return rst;
        }

        static int prob014CountCollatz(long n)
        {
            int counter = 0;
            while(n != 1)
            {
                if (n % 2 == 0)
                {
                    n = n / 2;
                }
                else
                {
                    n = 3 * n + 1;
                }
                counter++;
            }
            return counter;
        }

        static long[,] prob015Register = new long[50, 50];

        static long prob015(int x, int y)
        {
            if (0 != prob015Register[x, y])
            {
                return prob015Register[x, y];
            }
            Console.WriteLine("{0},{1}", x, y);
            long rst = x * y == 0 ? 1 : prob015(x - 1, y) + prob015(x, y - 1);
            prob015Register[x, y] = rst;
            return  rst;
        }

        static long prob016()
        {
            string n = "1";
            for (int i = 0; i < 1000; i++ )
            {
                n = prob013BignumSum(n, n);
            }
            long rst = 0;
            char[] na = n.ToCharArray();
            for (int i = 0; i < na.Length; i++ )
            {
                rst += (na[i] - '0');
            }
            return rst;
        }

        static long prob017()
        {
            long rst = 0;
            for (int i = 1; i <= 1000; i++ )
            {
                string a = prob017ItoA(i);
                Console.WriteLine(a);
                rst += a.Replace("-", "").Replace(" ", "").Length;
            }
            return rst;
        }

        static string prob017ItoA(int i)
        {
            if (i == 1000)
            {
                return "one thousand";
            }
            else if (i < 10)
            {
                return prob017ItoAUnit(i);
            }
            else if (i >= 10 && i < 100)
            {
                return prob017ItoADecade(i);
            }
            else if (i >= 100 && i < 1000)
            {
                return prob017ItoAHundreds(i);
            }
            else
            {
                return "";
            }
        }

        static string prob017ItoAHundreds(int i)
        {
            return prob017ItoAUnit(i / 100) + " hundred" + (i % 100 == 0 ? "" : (" and " + prob017ItoADecade(i % 100)));
        }

        static string prob017ItoADecade(int i)
        {
            if (i < 10)
            {
                return prob017ItoAUnit(i);
            }
            switch (i)
            {
                case 10:
                    return "ten";
                case 11:
                    return "eleven";
                case 12:
                    return "twelve";
                case 13:
                    return "thirteen";
                case 14:
                    return "fourteen";
                case 15:
                    return "fifteen";
                case 16:
                    return "sixteen";
                case 17:
                    return "seventeen";
                case 18:
                    return "eighteen";
                case 19:
                    return "nineteen";
                default:
                    return prob017ItoADecadeNormal(i) + (i % 10 == 0 ? "" : ("-" + prob017ItoAUnit(i % 10)));
            }
        }

        static string prob017ItoADecadeNormal(int i)
        {
            switch(i / 10)
            {
                case 2:
                    return "twenty";
                case 3:
                    return "thirty";
                case 4:
                    return "forty";
                case 5:
                    return "fifty";
                case 6:
                    return "sixty";
                case 7:
                    return "seventy";
                case 8:
                    return "eighty";
                case 9:
                    return "ninety";
                default:
                    return "";
            }
        }

        static string prob017ItoAUnit(int i)
        {
            switch (i)
            {
                case 1:
                    return "one";
                case 2:
                    return "two";
                case 3:
                    return "three";
                case 4:
                    return "four";
                case 5:
                    return "five";
                case 6:
                    return "six";
                case 7:
                    return "seven";
                case 8:
                    return "eight";
                case 9:
                    return "nine";
                default:
                    return "";
            }
        }

        static long prob018()
        {
            int size = 100;
            int mask = 100;
            int[,] data = new int[size, size];
            prob018Init("prob067_large_triangle.txt", data, size, mask);
            int[,] drst = new int[size, size];
            bool[,] solved = new bool[size, size];
            for (int i = 0; i < size; i++ )
            {
                for (int j = 0; j < size; j++ )
                {
                    drst[i, j] = int.MaxValue;
                    solved[i, j] = false;
                }
            }
            drst[0, 0] = data[0, 0];
            solved[0, 0] = true;
            prob018Dijkstra(data, drst, size, solved);
            prob018DijkstraPrint(drst, size, mask);
            long min = int.MaxValue;
            for (int j = 0; j < size; j++ )
            {
                if (drst[size - 1, j] < min)
                {
                    min = drst[size - 1, j];
                }
            }
            return (mask * size) - min;
        }

        static void prob018Init(string filename, int[,] data, int size, int mask)
        {
            for (int i = 0; i < size; i++ )
            {
                for (int j = 0; j < size; j++ )
                {
                    data[i, j] = mask;
                }
            }
            int counter = 0;
            StreamReader r = new StreamReader(filename);
            string line = r.ReadLine();
            while (line != null && counter < size)
            {
                string[] vals = line.Split(' ');
                for (int i = 0; i < size && i <= counter; i++ )
                {
                    data[counter, i] = mask - Convert.ToInt32(vals[i]);
                }
                line = r.ReadLine();
                counter++;
            }
        }

        static void prob018Dijkstra(int[,] data, int[,] drst, int size, bool[,] solvedd)
        {
            KeyValuePair<int, int> min = new KeyValuePair<int, int>(0, 0);
            prob018DijkstraRelax(data, drst, size, min);
            int totalNodes = ((size + 1) * size / 2 - 1);
            for (int i = 0; i < totalNodes; i++ )
            {
                Console.WriteLine("current progress: {0:D}/{1:D}", i, totalNodes);
                min = prob018DijkstraFindMin(drst, size, solvedd);
                solvedd[min.Value, min.Key] = true;
                prob018DijkstraRelax(data, drst, size, min);
            }
        }

        private static void prob018DijkstraRelax(int[,] data, int[,] drst, int size, KeyValuePair<int, int> min)
        {
            foreach (KeyValuePair<int, int> p in prob018NextPositions(min, size))
            {
                int newVal = drst[min.Value, min.Key] + data[p.Value, p.Key];
                if (drst[p.Value, p.Key] > newVal)
                {
                    drst[p.Value, p.Key] = newVal;
                }
            }
        }

        static KeyValuePair<int, int> prob018DijkstraFindMin(int[,] drst, int size, bool[,] solvedd)
        {
            int minVal = int.MaxValue;
            KeyValuePair<int, int> min = new KeyValuePair<int, int>(0, 0);
            for (int i = 0; i < size; i++ )
            {
                for (int j = 0; j <= i; j++ )
                {
                    KeyValuePair<int, int> tmp = new KeyValuePair<int, int>(j, i);
                    if (drst[i,j] < minVal && !solvedd[i, j])
                    {
                        minVal = drst[i, j];
                        min = tmp;
                    }
                }
            }
            Console.WriteLine("min is: {0}, its value: {1}", min, drst[min.Value, min.Key]);
            return min;
        }

        static void prob018DijkstraPrint(int[,] drst, int size, int mask)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write("{0:D2},", drst[i, j] == int.MaxValue ? 0 : mask * (i + 1) - drst[i, j]);
                }
                Console.WriteLine();
            }
        }

        static KeyValuePair<int, int>[] prob018NextPositions(KeyValuePair<int, int> p, int size)
        {
            int i = p.Key, j = p.Value;
            if (j >= size - 1 || i > j)
            {
                return new KeyValuePair<int, int>[0];
            }
            if (i == 0)
            {
                return new KeyValuePair<int, int>[] { new KeyValuePair<int, int>(0, j + 1), new KeyValuePair<int, int>(1, j + 1) };
            }
            return new KeyValuePair<int, int>[] { new KeyValuePair<int, int>(i, j + 1), new KeyValuePair<int, int>(i + 1, j + 1) };
        }

        static long prob019()
        {
            DateTime dt = DateTime.Parse("1901-01-01");
            DateTime end = DateTime.Parse("2000-12-31");
            long rst = 0;
            while(dt < end)
            {
                if (dt.DayOfWeek == 0 && dt.Day == 1)
                {
                    rst++;
                    Console.WriteLine("{0:g}", dt);
                }
                dt = dt.AddDays(1);
            }
            return rst;
        }

        static long prob020()
        {
            string n = "1";
            for (int i = 0; i < 100; i++ )
            {
                Console.Write("{0:D},", n);
                string tmp = n;
                for (int j = 0; j < i; j++ )
                {
                    n = prob013BignumSum(n, tmp);
                }
            }
            Console.WriteLine(n);
            long rst = 0;
            char[] na = n.ToCharArray();
            for (int i = 0; i < na.Length; i++)
            {
                rst += (na[i] - '0');
            }
            return rst;
        }

        static long prob021()
        {
            ICollection<int> amicableNumbers = new HashSet<int>();
            long rst = 0;
            for (int i = 1; i <= 10000; i++ )
            {
                if (amicableNumbers.Contains(i))
                {
                    continue;
                }
                int factorsSum = prob021SumFactors(prob021ProbFactors(i));
                if (factorsSum == i)
                {
                    continue;
                }
                if (prob021SumFactors(prob021ProbFactors(factorsSum)) == i)
                {
                    Console.WriteLine("find 1 pair of amicable numbers: {0:D} and {1:D}", i, factorsSum);
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

        static int prob021SumFactors(HashSet<int> factors)
        {
            int rst = 0;
            foreach (int n in factors)
            {
                rst += n;
            }
            return rst;
        }

        static HashSet<int> prob021ProbFactors(int n)
        {
            int limit = (int) Math.Sqrt(n) + 1;
            HashSet<int> rst = new HashSet<int>();
            rst.Add(1);
            for (int i = 2; i <= limit; i++ )
            {
                if (n % i == 0 && i < n)
                {
                    rst.Add(i);
                    rst.Add(n / i);
                }
            }
            return rst;
        }

        static long prob022()
        {
            StreamReader r = new StreamReader("prob022_names.txt");
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

        static long prob023()
        {
            List<int> abundantList = new List<int>();
            HashSet<int> abundantSet = new HashSet<int>();
            prob023ProbAbundantNums(abundantList, abundantSet);
            long rst = 0;
            for (int n = 1; n <= 28123; n++ )
            {
                if (!prob023IsAbundantSum(abundantList, abundantSet, n))
                {
                    rst += n;
                }
            }
            return rst;
        }

        static bool prob023IsAbundantSum(List<int> l, HashSet<int> s, int n)
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
            Console.WriteLine("found unabundant sumable number: {0}", n);
            return false;
        }

        static void prob023ProbAbundantNums(List<int> l, HashSet<int> s)
        {
            for (int i = 1; i <= 28123; i++ )
            {
                if (i < prob021SumFactors(prob021ProbFactors(i)))
                {
                    l.Add(i);
                    s.Add(i);
                }
            }
        }

        static long prob024()
        {
            string rst = "";
            int[] initA = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int sortAll = initA.Length - 1;
            long target = 1000000;
            target -= 1;
            List<int> lst = initA.ToList();
            while (target != 0)
            {
                long factorial = prob024Factorial(sortAll);
                int sortAllCount = (int) (target / factorial);
                rst += Convert.ToString(lst[sortAllCount]);
                lst.RemoveAt(sortAllCount);
                target -= factorial * sortAllCount;
                sortAll--;
                Console.WriteLine("current num is: {0}", rst);
            }
            foreach (int n in lst)
            {
                rst += Convert.ToString(n);
            }
            return Convert.ToInt64(rst);
        }

        static long prob024Factorial(int n)
        {
            if (n == 0)
            {
                return 1;
            }
            if (n == 1)
            {
                return 1;
            }
            return prob024Factorial(n - 1) * n;
        }

        static long prob025()
        {
            string f1 = "1", f2 = "1";
            long counter = 3;
            while (true)
            {
                string tmp = prob013BignumSum(f1, f2);
                if (counter % 2 == 0)
                {
                    f2 = tmp;
                }
                else
                {
                    f1 = tmp;
                }
                Console.WriteLine("{0} is {1}th Fibonacci", tmp, counter);
                if (tmp.Length >= 1000)
                {
                    return counter;
                }
                counter++;
            }
        }

        static long prob026()
        {
            long rst = 1;
            int lagestLen = 1;
            for (int n = 1; n <= 1000; n++ )
            {
                int currLen = prob026DecimalFractionLen(n);
                if ( currLen > lagestLen)
                {
                    rst = n;
                    lagestLen = currLen;
                    Console.WriteLine("current is {0}", n);
                }
            }
            return rst;
        }

        static int prob026DecimalFractionLen(int n)
        {
            Console.Write("probing {0}, ", n);
            HashSet<int> divisorHistory = new HashSet<int>();
            int counter = 0;
            int divisor = 1;
            while (divisor % n != 0 && !divisorHistory.Contains(divisor % n)) 
            {
                //divisorHistory.Add(divisor);
                divisor = divisor % n;
                while (divisor < n)
                {
                    divisorHistory.Add(divisor);
                    divisor *= 10;
                    counter++;
                }
                //counter++;
            }
            Console.WriteLine("result: {0}", counter);
            return counter;
        }

        static int prob026DivisorTransfer(int divisor, int n)
        {
            while (divisor < n)
            {
                divisor *= 10;
            }
            return divisor;
        }

        static long prob027()
        {
            List<int> candiPrimes = new List<int>();
            HashSet<long> primes = new HashSet<long>();
            int currPrimeAmount = 0;
            int rst = 0;
            for (int i = 2; i < 1000; i++ )
            {
                if (prob007IsPrime(i))
                {
                    candiPrimes.Insert(0, -i);
                    candiPrimes.Add(i);
                    primes.Add(i);
                }
            }
            for (int a = 1 - 1000; a < 1000; a++ )
            {
                Console.Write("a={0}...", a);
                foreach (int b in candiPrimes)
                {
                    int n = 0;
                    long currPrime = prob027Formula(n, a, b);
                    while (primes.Contains(Math.Abs(currPrime)) || prob007IsPrime(Math.Abs(currPrime)))
                    {
                        n++;
                        currPrime = prob027Formula(n, a, b);
                    }
                    if (n > currPrimeAmount)
                    {
                        currPrimeAmount = n;
                        rst = a * b;
                        Console.WriteLine("current result is: n={0}, a={1}, b={2}", n, a, b);
                    }
                }
            }
            return rst;
        }

        static long prob027Formula(int n, int a, int b)
        {
            return (n * n + a * n + b);
        }

        static long prob028()
        {
            long rst = 1;
            int n = 1;
            int stepLen = 2;
            int size = 1001;
            int ceil = size * size;
            while (n < ceil)
            {
                for (int i = 0; i < 4; i++ )
                {
                    n = n + stepLen;
                    rst += n;
                }
                stepLen += 2;
            }
            return rst;
        }

        static long prob029Simple()
        {
            HashSet<double> rst = new HashSet<double>();
            for (double a = 2; a <= 100; a++ )
            {
                for (double b = 2; b <= 100; b++ )
                {
                    rst.Add(Math.Pow(a, b));
                    Console.WriteLine("adding: pow({0}, {1}), total: {2}", a, b, rst.Count);
                }
            }
            return rst.Count;
        }

        static long prob029()
        {
            HashSet<string> rst = new HashSet<string>();
            for (int a = 2; a <= 100; a++ )
            {
                for (int b = 2; b <= 100; b++ )
                {
                    rst.Add(prob029BigNumPow(a, b));
                    Console.WriteLine("adding: pow({0}, {1}), total: {2}", a, b, rst.Count);
                }
            }
            return rst.Count;
        }

        static string prob029BigNumPow(int a, int b)
        {
            if (b == 0)
            {
                return "0";
            }
            string astr = a.ToString();
            string rst = astr;
            for (int i = 1; i < b; i++ )
            {
                rst = prob029StringNumMulti(rst, astr);
            }
            return rst;
        }

        static string prob029StringNumMulti(string a, string b)
        {
            string rst = a;
            int multiplier = Convert.ToInt32(b);
            for (int i = 1; i < multiplier; i++ )
            {
                rst = prob013BignumSum(rst, a);
            }
            return rst;
        }

        static long prob030()
        {
            int ratio = 5;
            int ceiling = prob030FindCeiling(ratio);
            long rst = 0;
            for (int n = 2; n < ceiling; n++ )
            {
                if (n == prob030CalcDigSum(n, ratio))
                {
                    rst += n;
                    Console.WriteLine("found number: {0}", n);
                }
            }
            return rst;
        }

        static int prob030CalcDigSum(int n, int ratio)
        {
            string nstr = n.ToString();
            int rst = 0;
            foreach (char c in nstr.ToCharArray())
            {
                rst += (int) Math.Pow((int)(c - '0'), ratio);
            }
            return rst;
        }

        static int prob030FindCeiling(int ratio)
        {
            double ninePow = Math.Pow(9, ratio);
            int n = 1;
            while (ninePow * n > prob030FindCeilingNineNum(n))
            {
                n++;
            }
            return (int) ninePow * (n - 1);
        }

        static int prob030FindCeilingNineNum(int n)
        {
            string rst = "";
            for (int i = 0; i < n; i++ )
            {
                rst += "9";
            }
            return Convert.ToInt32(rst);
        }

        static long prob031()
        {
            int t = 200;
            int[] candis = { 200, 100, 50, 20, 10, 5, 2, 1 };
            Stack<int> s = new Stack<int>();
            List<int> counter = new List<int>();
            prob031Recur(t, candis, s, counter);
            return counter.Count;
        }

        static void prob031Recur(int t, int[] candis, Stack<int> s, List<int> counter)
        {
            foreach (int c in candis)
            {
                if (c <= t && (s.Count == 0 || c <= s.Peek()))
                {
                    s.Push(c);
                    if (c < t)
                    {
                        prob031Recur(t - c, candis, s, counter);
                    }
                    else
                    {
                        counter.Add(1);
                        if (counter.Count % 100 == 0)
                        {
                            Console.Write("Found 100 combination, and last one is: ");
                            foreach (int i in s)
                            {
                                Console.Write("{0},", i);
                            }
                            Console.WriteLine();
                        }
                    }
                    s.Pop();
                }
            }
        }

        static long prob032()
        {
            long rst = 0;
            HashSet<int> s = new HashSet<int>();
            for (int a = 1; a < 9999; a++ )
            {
                if (a % 100 == 0)
                {
                    Console.WriteLine("working on {0} to {1}...", a, a + 100);
                }
                int limit = 9999 / a + 1;
                for (int b = 0; b < 999 && b < limit; b++)
                {
                    int c = a * b;
                    if (prob032IsPandigital(a, b, c, s))
                    {
                        Console.WriteLine("found pandigitals: {0} * {1} = {2}", a, b, c);
                        rst += c;
                    }
                }
            }
            return rst;
        }

        static bool prob032IsPandigital(int a, int b, int c, HashSet<int> s)
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

        static long prob033()
        {
            int numerator = 1;
            int denominator = 1;
            for (int a = 11; a < 99; a++ )
            {
                if (a % 10 == 0)
                {
                    continue;
                }
                for (int b = a + 1; b < 99; b++ )
                {
                    if (b % 10 == 0)
                    {
                        continue;
                    }
                    if (prob033IsCurious(a, b))
                    {
                        numerator *= a;
                        denominator *= b;
                    }
                }
            }
            return prob033MinFactor(numerator, denominator, denominator); 
        }

        static bool prob033IsCurious(int a, int b)
        {
            int numerator = prob033MinFactor(a, b, a);
            int denominator = prob033MinFactor(a, b, b);
            List<int> commonList = prob033CommonFactorList(a, b);
            if (commonList.Count == 0)
            {
                return false;
            }
            foreach (int common in commonList)
            {
                int sa = prob033RemoveCommonFactor(a, common), sb = prob033RemoveCommonFactor(b, common);
                int numeratorS = prob033MinFactor(sa, sb, sa);
                int denominatorS = prob033MinFactor(sa, sb, sb);
                if (numerator == numeratorS && denominator == denominatorS)
                {
                    Console.WriteLine("found 1 pair: {0}/{1}", a, b);
                    return true;
                }
            }
            return false;
        }

        static int prob033RemoveCommonFactor(int n, int common)
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

        static List<int> prob033CommonFactorList(int a, int b)
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

        static int prob033MinFactor(int numerator, int demoninator, int n)
        {
            return (int) (n / prob005GCD(numerator, demoninator));
        }

        static long prob034()
        {
            Dictionary<int, int> factorials = new Dictionary<int, int>();
            for (int i = 0; i <= 9; i++ )
            {
                factorials.Add(i, (int) prob024Factorial(i));
            }
            int ceil = prob034FindCeiling();
            long rst = 0;
            for (int i = 10; i < ceil; i++ )
            {
                if (i % 10000 == 0)
                {
                    Console.WriteLine("work from {0} to {1}...", i, i + 10000);
                }
                if (i == prob034DigitSum(i, factorials))
                {
                    Console.WriteLine("found 1 curious number: {0}", i);
                    rst += i;
                }
            }
            return rst;
        }

        static int prob034DigitSum(int n, Dictionary<int, int> factorials)
        {
            int rst = 0;
            char[] na = n.ToString().ToCharArray();
            foreach (char c in na)
            {
                rst += factorials[(int)(c - '0')];
            }
            return rst;
        }

        static int prob034FindCeiling()
        {
            int nineFactorial = (int) prob024Factorial(9);
            int n = 1;
            while (nineFactorial * n > prob030FindCeilingNineNum(n))
            {
                n++;
            }
            return (int)nineFactorial * (n - 1);
        }

        static long prob035()
        {
            HashSet<int> primes = new HashSet<int>();
            HashSet<int> composities = new HashSet<int>();
            long rst = 0;
            for (int i = 2; i < 1000000; i++ )
            {
                if (i % 10000 == 0)
                {
                    Console.WriteLine("work from {0} to {1}...", i, i + 10000);
                }
                if (prob035IsCirclePrime(i, primes, composities))
                {
                    Console.WriteLine("found circle prime: {0}", i);
                    rst++;
                }
            }
            return rst;
        }

        static bool prob035IsCirclePrime(int n, HashSet<int> primes, HashSet<int> composities)
        {
            string nstr = n.ToString();
            for (int i = 0; i < nstr.Length; i++ )
            {
                int nn = int.Parse(nstr);
                if (composities.Contains(nn))
                {
                    return false;
                }
                if (!primes.Contains(nn))
                {
                    if (prob007IsPrime(nn))
                    {
                        primes.Add(nn);
                    } 
                    else
                    {
                        composities.Add(nn);
                        return false;
                    }
                }
                nstr = nstr.Length > 1 ?  string.Concat(nstr.Substring(1, nstr.Length - 1), nstr.Substring(0, 1)) : nstr;
            }
            return true;
        }

        static long prob036()
        {
            Console.WriteLine(Convert.ToString(7, 2));
            long rst = 0;
            for (int i = 0; i < 1000000; i++ )
            {
                if (i % 10000 == 0)
                {
                    Console.WriteLine("work from {0} to {1}...", i, i + 10000);
                }
                if (prob036IsPalindromic(i.ToString()) && prob036IsPalindromic(Convert.ToString(i, 2)))
                {
                    Console.WriteLine("found 1 double palindromic number: {0} ({1})", i, Convert.ToString(i, 2));
                    rst += i;
                }
            }
            return rst;
        }

        static bool prob036IsPalindromic(string s)
        {
            for (int i = 0; i < s.Length / 2 + 1; i++ )
            {
                if (s[i] != s[s.Length - i - 1])
                {
                    return false;
                }
            }
            return true;
        }

        static long prob037()
        {
            long rst = 0;
            int counter = 0;
            int n = 11;
            HashSet<long> primes = new HashSet<long>();
            while (counter < 11)
            {
                if (prob037IsTruncatablePrime(n, primes))
                {
                    Console.WriteLine("found 1 truncatable prime: {0}", n);
                    counter++;
                    rst += n;
                }
                n++;
            }
            return rst;
        }

        static bool prob037IsTruncatablePrime(long n, HashSet<long> primes)
        {
            string nstr = n.ToString();
            for (int i = 0; i < nstr.Length; i++ )
            {
                long left = long.Parse(nstr.Substring(i, nstr.Length - i));
                long right = long.Parse(nstr.Substring(0, nstr.Length - i));
                if (!primes.Contains(left))
                {
                    if (prob007IsPrime(left))
                    {
                        primes.Add(left);
                    }
                    else
                    {
                        return false;
                    }
                }
                if (!primes.Contains(right))
                {
                    if (prob007IsPrime(right))
                    {
                        primes.Add(right);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        static long prob038()
        {
            return 0;
        }

        static long prob039()
        {
            long rst = 0;
            int ceil = 1000;
            int curr = 0;
            for (int n = 12; n <= ceil; n++ )
            {
                int trianglesN = prob039ProbTriangle(n);
                if ( trianglesN > curr)
                {
                    curr = trianglesN;
                    rst = n;
                }
            }
            return rst;
        }

        static int prob039ProbTriangle(int n)
        {
            int triangleCount = 0;
            Console.Write("Probing {0}: ", n);
            for (int a = 1; a < n / 3 + 1; a++)
            {
                for (int b = a; b < n / 2 + 1; b++)
                {
                    int c = n - a - b;
                    if ((a * a) + (b * b) == c * c)
                    {
                        triangleCount++;
                        Console.Write("{0},{1},{2};", a, b, c);
                    }
                }
            }
            Console.WriteLine("={0} triangles", triangleCount);
            return triangleCount;
        }

        static long prob040()
        {
            StringBuilder s = new StringBuilder("");
            int counter = 1;
            while (s.Length < 1000000)
            {
                if (counter % 10000 == 0)
                {
                    Console.WriteLine("work from {0} to {1}...", counter, counter + 10000);
                }
                s.Append(counter.ToString());
                counter++;
            }
            int[] nums = new int[] { (int)(s[1 - 1] - '0'), (int)(s[10 - 1] - '0'), (int)(s[100 - 1] - '0'), (int)(s[1000 - 1] - '0'), (int)(s[10000 - 1] - '0'), (int)(s[100000 - 1] - '0'), (int)(s[1000000 - 1] - '0') };
            long rst = 1;
            foreach (int n in nums)
            {
                Console.Write("{0},", n);
                rst *= n;
            }
            return rst;
        }

        static long prob041()
        {
            /************************************************************************/
            /* 9、8位的pandigital都可以被3整除，所以从7位开始（7654321）            */
            /************************************************************************/
            for (long n = 7654319; n > 1; n -- )
            {
                if (prob041IsPanDigital(n) && prob007IsPrime(n))
                {
                    return n;
                }
            }
            return 0;
        }

        static bool prob041IsPanDigital(long n)
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

        static long prob042()
        {
            long rst = 0;
            HashSet<int> triangleNums = prob042InitTriangleNums(100);
            StreamReader r = new StreamReader("prob042_words.txt");
            string line = r.ReadLine();
            line = line.Replace("\"", "");
            string[] a = line.Split(',');
            foreach (string word in a)
            {
                if (prob042IsTriangleWord(word, triangleNums))
                {
                    rst++;
                }
            }
            return rst;
        }

        static bool prob042IsTriangleWord(string word, HashSet<int> triangleNums)
        {
            int count = 0;
            foreach (char c in word)
            {
                count += c - 'A' + 1;
            }
            if (triangleNums.Contains(count))
            {
                Console.WriteLine("found 1 triangle word: {0} for {1}", word, count);
                return true;
            }
            else
            {
                return false;
            }
        }

        static HashSet<int> prob042InitTriangleNums(int ceil)
        {
            HashSet<int> rst = new HashSet<int>();
            for (int i = 1; i <= ceil; i++ )
            {
                rst.Add((i + 1) * i / 2);
            }
            return rst;
        }

        static long prob043()
        {
            int[] factors = new int[] { 17, 13, 11, 7, 5, 3, 2 };
            HashSet<string> results = new HashSet<string>();
            prob043Recur(0, factors, new StringBuilder(""), results);
            long rst = 0;
            foreach (string s in results)
            {
                rst += long.Parse(s);
            }
            return rst;
        }

        static void prob043Recur(int n, int[] factors, StringBuilder builder, HashSet<string> results)
        {
            if (n >= factors.Length)
            {
                string pandigital = prob043ToPandigital(builder.ToString());
                if (!pandigital.Equals(""))
                {
                    results.Add(pandigital);
                    Console.WriteLine(pandigital);
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
                    prob043Recur(n + 1, factors, builder, results);
                }
                else if (prob043Connectable(builder, currStr))
                {
                    builder.Insert(0, currStr[0]);
                    connectable = true;
                    prob043Recur(n + 1, factors, builder, results);
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

        static string prob043ToPandigital(string s)
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
            for (char n = '0'; n <= '9'; n++ )
            {
                if (!container.Contains(n))
                {
                    return n + s;
                }
            }
            return "";
        }

        static bool prob043Connectable(StringBuilder builder, string curr)
        {
            return curr[2] == builder[1] && curr[1] == builder[0];
        }

        static long prob044()
        {
            int size = 3000;
            List<long> pentagonalList = new List<long>(size);
            HashSet<long> pentagonalSet = new HashSet<long>();
            for (long n = 1; n <= size; n++ )
            {
                long pentagonal = n * (3 * n - 1) / 2;
                pentagonalList.Add(pentagonal);
                pentagonalSet.Add(pentagonal);
            }
            long rst = long.MaxValue;
            for (int i = 1; i < size; i++ )
            {
                if (i % 1000 == 0)
                {
                    Console.WriteLine("work from {0} to {1}...", i, i + 1000);
                }
                for (int j = i + 1; j < size; j++)
                {
                    long sum = pentagonalList[i] + pentagonalList[j];
                    long diff = pentagonalList[j] - pentagonalList[i];
                    if (pentagonalSet.Contains(sum) && pentagonalSet.Contains(diff))
                    {
                        Console.WriteLine("found 1 pair: {0}, {1}", pentagonalList[i], pentagonalList[j]);
                        if (diff < rst)
                        {
                            rst = diff;
                        }
                    }
                }
            }
            return rst;
        }

        static long prob045()
        {
            long n = 286;
            while (true)
            {
                if (n % 100 == 0)
                {
                    Console.WriteLine("work from {0} to {1}...", n, n + 100);
                }
                long t = n * (n + 1) / 2;
                if (prob045IsPentagonal(t) && prob045IsHexagonal(t))
                {
                    return t;
                }
                n++;
            }
        }

        static bool prob045IsPentagonal(long t)
        {
            double n = (1 + Math.Sqrt(24 * t + 1)) / 6;
            return prob045IsInteger(n);
        }

        static bool prob045IsHexagonal(long t)
        {
            double n = (1 + Math.Sqrt(t * 8 + 1)) / 4;
            return prob045IsInteger(n);
        }

        static bool prob045IsInteger(double d)
        {
            return d % 1 < 0.0000001;
        }

        static long prob046()
        {
            List<long> primes = new List<long>();
            primes.Add(2);
            long n = 3;
            while (true)
            {
                if (!prob007IsPrime(n))
                {
                    if (!prob046Decomposeable(n, primes))
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

        static bool prob046Decomposeable(long prime, List<long> primes)
        {
            foreach (long p in primes)
            {
                double num = Math.Sqrt((prime - p) / 2);
                if (num > 0 && prob045IsInteger(num))
                {
                    Console.WriteLine("{0} is made up by: {1} + 2 * {2} * {2}", prime, p, (int) num);
                    return true;
                }
            }
            return false;
        }

        static long prob047()
        {
            List<long> candidates = new List<long>();
            long n = 2;
            while (true)
            {
                if (n % 10000 == 0)
                {
                    Console.WriteLine("work from {0} to {1}...", n, n + 10000);
                }
                if (candidates.Count == 4)
                {
                    return candidates[0];
                }
                if (prob047CouldDecomposeTo4Primes(n))
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

        static bool prob047CouldDecomposeTo4Primes(long number)
        {
            HashSet<long> factors = new HashSet<long>();
            int divisor = 2;
            while (number > 1)
            {
                if (0 == (number % divisor))
                {
                    factors.Add(divisor);
                    number /= divisor;
                    divisor--;
                }
                divisor++;
            }
            return factors.Count == 4;
        }

        static long prob048()
        {
            return 0;
        }

        static long prob049()
        {
            return long.MaxValue;
        }

        static long prob050()
        {
            List<long> primes = new List<long>(80000);
            for (long n = 2; n <= 1000000; n++ )
            {
                if (prob007IsPrime(n))
                {
                    primes.Add(n);
                }
            }
            long rst = primes[0];
            int currLen = 0;
            for (int i = 12; i < primes.Count; i++ )
            {
                for (int j = 0; j < i; j++ )
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
                    Console.Write("{0} = {1}", primes[i], primes[j]);
                    for (int k = j + 1; k < index; k++ )
                    {
                        Console.Write(" + {0}", primes[k]);
                    }
                    Console.WriteLine();
                }
            }
            return rst;
        }

        static long prob050A()
        {
            List<long> primes = new List<long>(80000);
            HashSet<long> primeSet = new HashSet<long>();
            for (long n = 2; n <= 1000000; n++)
            {
                if (prob007IsPrime(n))
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
                    Console.Write("{0} = {1}", sum, primes[i]);
                    for (int k = i + 1; k < index; k++)
                    {
                        Console.Write(" + {0}", primes[k]);
                    }
                    Console.WriteLine(", ({0} consecutive)", index - i);
                }
            }
            return rst;
        }

        static long prob051()
        {
            long begin = DateTime.Now.Ticks;
            long n = 11;
            int threshold = 8;
            HashSet<long> primes = new HashSet<long>();
            while (true)
            {
                if (!prob051IsPrime(n, primes))
                {
                    n++;
                    continue;
                }
                int len = n.ToString().Length;
                //Console.Write("Working on {0}...", n);
                for (int i = 1; i <= len - 1; i++ )
                {
                    List<int[]> combinations = new List<int[]>();
                    prob051Combination(len, i, 0, combinations, null);
                    foreach (int[] substitues in combinations)
                    {
                        if (prob051IsThatPrime(n, substitues, threshold, primes))
                        {
                            long end = DateTime.Now.Ticks;
                            Console.WriteLine("Time elapsed: {0}ms", (end - begin) / 10000);
                            return n;
                        }
                    }
                }
                n++;
            }
        }

        static bool prob051IsThatPrime(long prime, int[] substitues, int threshold, HashSet<long> primes)
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
                if (prob051IsPrime(long.Parse(new string(ca)), primes))
                {
                    counter++;
                }
            }
            return counter >= threshold;
        }

        static bool prob051IsPrime(long n, HashSet<long> primes)
        {
            if (primes.Contains(n))
            {
                return true;
            }
            if (prob007IsPrime(n))
            {
                primes.Add(n);
                return true;
            } 
            else
            {
                return false;
            }
        }

        static void prob051Combination(int m, int n, int p, List<int[]> rst, List<int> inter)
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
            for (int i = p; i < m; i++ )
            {
                if (!inter.Contains(i))
                {
                    inter.Add(i);
                    prob051Combination(m, n, i + 1, rst, inter);
                    inter.Remove(i);
                }
            }
        }

        static long prob052()
        {
            long begin = DateTime.Now.Ticks;
            long step = 10;
            HashSet<char> container = new HashSet<char>(), worker = new HashSet<char>();
            while (true)
            {
                long nextStep = step * 10;
                long currLimit = nextStep / 6;
                //Console.WriteLine("working from {0} to {1}...", step, currLimit);
                for (long n = step; n <= currLimit; n++)
                {
                    if (prob052IsThatNumber(n))
                    {
                        long end = DateTime.Now.Ticks;
                        Console.WriteLine("Time elapsed: {0}ms", (end - begin) / 10000);
                        return n;
                    }
                }
                step = nextStep;
            }
        }

        static bool prob052IsThatNumber(long n)
        {
            HashSet<char> container = new HashSet<char>(), worker = new HashSet<char>();
            List<char> standard = n.ToString().ToCharArray().ToList();
            standard.Sort();
            for (int multiplier = 2; multiplier <= 6; multiplier++ )
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

        static long prob053()
        {
            long begin = DateTime.Now.Ticks;
            long rst = 0;
            for (int n = 1; n <= 100; n++ )
            {
                for (int r = 1; r < n; r++ )
                {
                    if (prob053Factorial(n) / prob053Factorial(r) / prob053Factorial(n - r) >= 1000000)
                    {
                        rst++;
                    }
                }
            }
            long end = DateTime.Now.Ticks;
            Console.WriteLine("Time elapsed: {0}ms", (end - begin) / 10000);
            return rst;
        }

        static double prob053Factorial(double n)
        {
            if (n == 1)
            {
                return 1;
            }
            return n * prob053Factorial(n - 1);
        }

        static long prob054()
        {
            long rst = 0;
            StreamReader r = new StreamReader("prob054_poker.txt");
            string line = r.ReadLine();
            while (line != null)
            {
                string[] porkes1 = line.Substring(0, 14).Split(' ');
                string[] porkes2 = line.Substring(15).Split(' ');
                Prob054PorkerSuite ps1 = new Prob054PorkerSuite(porkes1);
                Prob054PorkerSuite ps2 = new Prob054PorkerSuite(porkes2);
                int comp = ps1.CompareTo(ps2);
                if (comp > 0) rst++;
                if (comp == 0) outputLine(string.Format("p1:{0} -- p2:{1} = {2}", ps1.Resolver, ps2.Resolver, comp));
                line = r.ReadLine();
            }
            return rst;
        }

        static long prob055()
        {
            Console.WriteLine(prob004IsPalindromic("2"));
            Console.WriteLine(prob013BignumSum("0001", "200"));
            long rst = 0;
            for (int i = 1; i < 10000; i++ )
            {
                string num = i.ToString();
                int counter = 0;
                while (counter < 50)
                {
                    num = prob013BignumSum(num, prob055ReverseString(num));
                    if (prob004IsPalindromic(num))
                    {
                        break;
                    }
                    counter++;
                }
                if (50 == counter) Console.WriteLine(i);
                rst += (counter == 50 ? 1 : 0);
            }
            return rst;
        }

        static string prob055ReverseString(string s)
        {
            char[] ca = s.ToCharArray();
            Array.Reverse(ca);
            return new string(ca);
        }

        static long prob056()
        {
            long rst = 0;
            for (int i = 1; i < 100; i++)
            {
                for (int j = 1; j < 100; j++)
                {
                    string n = prob029BigNumPow(i, j);
                    long snd = prob056SumNumberDigital(n);
                    if (snd > rst)
                    {
                        rst = snd;
                        Console.WriteLine("current top is {0}(for {1} composited by {2}pow{3})", rst, n, i, j);
                    }
                }
            }
            return rst;
        }

        static long prob056SumNumberDigital(string n)
        {
            long rst = 0;
            char[] ca = n.ToCharArray();
            foreach (char c in ca)
            {
                rst += (c - '0');
            }
            return rst;
        }

        static long prob057()
        {
            long rst = 0;
            Fraction suffix = new Fraction(1, 2), ONE = new Fraction(1, 1), TWO = new Fraction(2, 1);
            int counter = 0;
            while (counter < 1000)
            {
                Fraction sqrt2 = ONE + suffix;
                if (sqrt2.Numerator.ToString().Length > sqrt2.Denominator.ToString().Length)
                {
                    rst++;
                    outputLine(string.Format("{0}th,{1}", counter + 1, sqrt2.ToString()));
                }
                counter++;
                suffix = ONE / (TWO + suffix);
            }
            return rst;
        }

        static long prob058()
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
                numerator += prob007IsPrime(node1) ? 1 : 0;
                numerator += prob007IsPrime(node2) ? 1 : 0;
                numerator += prob007IsPrime(node3) ? 1 : 0;
                numerator += prob007IsPrime(node4) ? 1 : 0;
                double ratio = numerator / denominator;
                watch.Stop();
                outputLine(string.Format("size: {0}, ratio: {1:N6}", rst, ratio));
                watch.Start();
                if (0.1 > ratio) break;
            }
            return rst;
        }

        static long prob059()
        {
            watch.Stop();
            byte[] cipher = prob059ReadCipher(), plain = new byte[cipher.Length];
            long rst = 0;
            watch.Start();
            List<byte[]> candiKeyList = prob059GenerateCandiKey();
            foreach (byte[] key in candiKeyList)
            {
                if (!prob059Decrypt(cipher, plain, key)) continue;
                foreach (byte b in plain) rst += b;
                outputLine(string.Format("Key: {0}\nContent:\n{1}", Encoding.ASCII.GetString(key), Encoding.ASCII.GetString(plain)));
                break;
            }
            return rst;
        }

        static bool prob059Decrypt(byte[] cipher, byte[] plain, byte[] key)
        {
            for (int i = 0; i < cipher.Length; )
            {
                for (int j = 0; j < key.Length && i < cipher.Length; j++ )
                {
                    byte b = (byte) (cipher[i] ^ key[j]);
                    if (prob059IsInvalidChar(b)) return false;
                    plain[i] = b;
                    i++;
                }
            }
            return true;
        }

        static bool prob059IsInvalidChar(byte b)
        {
            return 26 > b || 126 < b || '%' == b || '}' == b || '+' == b || '/' == b || '{' == b;
        }

        static byte[] prob059ReadCipher()
        {
            StreamReader r = new StreamReader("prob059_cipher1.txt");
            string line = r.ReadLine();
            string[] cipherStrArray = line.Split(',');
            byte[] cipher = new byte[cipherStrArray.Length];
            for (int i = 0; i < cipherStrArray.Length; i++)
            {
                cipher[i] = byte.Parse(cipherStrArray[i]);
            }
            return cipher;
        }

        static List<byte[]> prob059GenerateCandiKey()
        {
            List<byte[]> rst = new List<byte[]>(26 * 26 * 26);
            for (char i = 'a'; i <= 'z'; i++ ) { for (char j = 'a'; j <= 'z'; j++) { for (char k = 'a'; k <= 'z'; k++)
            {
                rst.Add(new byte[] { (byte) i,(byte) j,(byte) k });
            } } }
            return rst;
        }

        static long prob060()
        {
            long rst = long.MaxValue;
            int limit = 1051;
            HashSet<long> cache = new HashSet<long>();
            List<long> candidates = prob060Candidates(limit, cache);
            int counter = 0;
            foreach (long c1 in candidates)
            {
                outputLine(string.Format("working for {0}", ++counter));
                foreach (long c2 in candidates)
                {
                    if (c1 >= c2) continue;
                    if (!prob060Check(c1, c2, cache)) continue;
                    foreach (long c3 in candidates)
                    {
                        if (c2 >= c3) continue;
                        if (!prob060Check(c1, c3, cache)
                            || !prob060Check(c2, c3, cache)
                            ) continue;
                        foreach (long c4 in candidates)
                        {
                            if (c3 >= c4) continue;
                            if (!prob060Check(c1, c4, cache)
                                || !prob060Check(c2, c4, cache)
                                || !prob060Check(c3, c4, cache)
                                ) continue;
                            foreach (long c5 in candidates)
                            {
                                if (c4 >= c5) continue;
                                if (!prob060Check(c1, c5, cache)
                                    || !prob060Check(c2, c5, cache)
                                    || !prob060Check(c3, c5, cache)
                                    || !prob060Check(c4, c5, cache)
                                    ) continue;
                                outputLine(string.Format("found primes: {0}, {1}, {2}, {3}, {4}", c1, c2, c3, c4, c5));
                                long tmp = c1 + c2 + c3 + c4 + c5;
                                rst = tmp < rst ? tmp : rst;
                            }
                        }
                    }
                }
            }
            return rst;
        }

        static bool prob060Check(long c1, long c2, HashSet<long> cache)
        {
            return prob051IsPrime(prob060Concate(c1, c2), cache) 
                && prob051IsPrime(prob060Concate(c2, c1), cache); 
        }

        static long prob060Concate(long c1, long c2) { return long.Parse(c1.ToString() + c2.ToString()); }

        static List<long> prob060Candidates(int limit, HashSet<long> cache)
        {
            List<long> rst = new List<long>(limit);
            int counter = 0;
            long n = 1;
            while (limit > counter)
            {
                n++;
                if (prob051IsPrime(n, cache))
                {
                    rst.Add(n);
                    counter++;
                }
            }
            return rst;
        }

        static long prob061()
        {
            long rst = 0;
            return rst;
        }
    }

}
