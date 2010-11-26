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

        #region Output Substitution for Debug
        [Conditional("DEBUG")]
        static void OutputLine(string format, params object[] arg) { OutputLine(string.Format(format, arg)); }
        [Conditional("DEBUG")]
        static void OutputLine(object o) { OutputLine(o.ToString()); }
        [Conditional("DEBUG")]
        static void OutputLine() { OutputLine(""); }
        [Conditional("DEBUG")]
        static void OutputLine(string s)
        {
            watch.Stop();
            Console.WriteLine(s);
            watch.Start();
        }
        [Conditional("DEBUG")]
        static void Output(string format, params object[] arg) { Output(string.Format(format, arg)); }
        [Conditional("DEBUG")]
        static void Output(object o) { Output(o.ToString()); }
        [Conditional("DEBUG")]
        static void Output(string s) 
        {
            watch.Stop(); 
            Console.Write(s); 
            watch.Start();
        }
        #endregion

        static void Main(string[] args)
        {
            List<Executor> runners = GetRunners();
            if (null == runners) return;
            TimeSpan totalTime = new TimeSpan(0);
            foreach (Executor e in runners)
            {
                Console.Write("Working on {0}", e.Method.Name);
                watch.Reset();
                watch.Start();
                object rst = e();
                watch.Stop();
                totalTime += watch.Elapsed;
                Console.WriteLine(" ==> {0}\t{1}ms", rst.ToString().PadRight(20), watch.ElapsedMilliseconds);
            }
            Console.WriteLine("Total time: {0}", totalTime.ToString());
        }

        static List<Executor> GetRunners()
        {
            List<Executor> rst = new List<Executor>();
            List<MethodInfo> methods = GetAllSolvers();
            Console.Write("Which problem to run({0}) ? ", methods[0].Name.Substring(4));
            MethodInfo userMethod = null;
            try 
            {
                string input = Console.ReadLine();
                if ("ALL".Equals(input.Trim().ToUpper())) methods.ForEach(x => rst.Insert(0, CreateExecutor(x)));
                else if (!string.IsNullOrEmpty(input.Trim())) userMethod = methods.Where(x => x.Name.Equals(string.Format("Prob{0:D3}", int.Parse(input)))).First();
                else if (!string.IsNullOrEmpty(input.Trim()) && null == userMethod) { Console.WriteLine("Method not found"); return null; }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return null;
            };
            if (0 == rst.Count) rst.Add(CreateExecutor(null == userMethod ? methods[0] : userMethod));
            return rst;
        }

        static List<MethodInfo> GetAllSolvers()
        {
            return typeof(Program).GetMethods(MethodFlag)
                .Where(x => x.Name.StartsWith("Prob") && x.Name.Length == 7)
                .OrderByDescending(x => x.Name).ToList();
        }

        static Executor CreateExecutor(MethodInfo m)
        {
            return (Executor)Delegate.CreateDelegate(typeof(Executor), m);
        }
        
    }

}
