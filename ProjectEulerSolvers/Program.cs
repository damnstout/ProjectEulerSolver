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
    delegate long PolygonN(long n);

    delegate long Executor();

    partial class Program
    {
        const BindingFlags MethodFlag = BindingFlags.Static | BindingFlags.NonPublic;

        static Stopwatch watch = new Stopwatch();

        static void OutputLine(string format, params object[] arg) { OutputLine(string.Format(format, arg)); }
        static void OutputLine(object o) { OutputLine(o.ToString()); }
        static void OutputLine() { OutputLine(""); }
        static void OutputLine(string s)
        {
#if DEBUG
            watch.Stop();
            Console.WriteLine(s);
            watch.Start();
#endif
        }
        static void Output(string format, params object[] arg) { Output(string.Format(format, arg)); }
        static void Output(object o) { Output(o.ToString()); }
        static void Output(string s) 
        {
#if DEBUG
            watch.Stop(); 
            Console.Write(s); 
            watch.Start();
#endif
        }

        static void Main(string[] args)
        {
            List<Executor> runners = GetRunners();
            if (null == runners) return;
            foreach (Executor e in runners)
            {
                Console.Write("Working on {0}", e.Method.Name);
                watch.Reset();
                watch.Start();
                object rst = e();
                watch.Stop();
                string s = string.Format("\t ==> {0:D}", rst);
                Console.Write(s.PadRight(20));
                Console.WriteLine("\t{0}ms", watch.ElapsedMilliseconds);
            }
        }

        static List<Executor> GetRunners()
        {
            List<Executor> rst = new List<Executor>();
            IEnumerable<MethodInfo> methods = GetAllSolvers();
            Console.Write("Which problem to run({0}) ? ", methods.Last().Name.Substring(4));
            MethodInfo userMethod = null;
            try 
            {
                string input = Console.ReadLine();
                if ("ALL".Equals(input.Trim().ToUpper())) methods.ToList().ForEach(x => rst.Add(CreateExecutor(x)));
                else if (!string.IsNullOrEmpty(input.Trim())) userMethod = methods.Where(x => x.Name.Equals(string.Format("Prob{0:D3}", int.Parse(input)))).First();
                else if (!string.IsNullOrEmpty(input.Trim()) && null == userMethod) { Console.WriteLine("Method not found"); return null; }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return null;
            };
            if (0 == rst.Count) rst.Add(CreateExecutor(null == userMethod ? methods.Last() : userMethod));
            return rst;
        }

        static IEnumerable<MethodInfo> GetAllSolvers()
        {
            return typeof(Program).GetMethods(MethodFlag)
                .Where(x => x.Name.StartsWith("Prob") && x.Name.Length == 7)
                .OrderBy(x => x.Name);
        }

        static Executor CreateExecutor(MethodInfo m)
        {
            return (Executor)Delegate.CreateDelegate(typeof(Executor), m);
        }
        
    }

}
