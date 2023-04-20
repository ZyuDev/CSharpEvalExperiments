using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System;
using System.Data;


namespace EvalExperiments.Roslyn
{
    public class SimpleEvaluator
    {
        public async Task<decimal> Eval(decimal a, decimal b)
        {
            var code = @"A+B";
            var globalsObject = new SimpleEvaluatorGlobals()
            {
                A = a,
                B = b
            };

            long memoryBefore = GC.GetTotalMemory(true);
         

            Script<decimal> script = CSharpScript.Create<decimal>(code, ScriptOptions.Default.WithImports("System"), globalsObject.GetType());
            script.Compile();
         
          
            long memoryAfter = GC.GetTotalMemory(true);
            long objectSize = memoryAfter - memoryBefore;
            Console.WriteLine($"Total memory for script: {objectSize} bytes, {objectSize / (1024*1024)} Mb");


            ScriptState<decimal> state = await script.RunAsync(globals: globalsObject);
            decimal result = state.ReturnValue;

            var dataTable = new DataTable();

            return result;
        }

        public async Task<decimal> EvalDelegate(decimal a, decimal b)
        {
            var code = @"A+B";
            var globalsObject = new SimpleEvaluatorGlobals()
            {
                A = a,
                B = b
            };


            //Script<decimal> script = CSharpScript.Create<decimal>(code,
            //    ScriptOptions.Default.WithImports("System"), globalsObject.GetType());

            long memoryBefore = GC.GetTotalMemory(true);
            //ScriptRunner<decimal> runner = script.CreateDelegate();
            var runner = InitScriptRunner(code);
            GC.Collect();

            long memoryAfter = GC.GetTotalMemory(true);
            long objectSize = memoryAfter - memoryBefore;
            Console.WriteLine($"Total memory for script: {objectSize} bytes, {objectSize / (1024 * 1024)} Mb");


            var result = await runner(globalsObject);

          

            return result;
        }

        public ScriptRunner<decimal> InitScriptRunner(string code)
        {
            Script<decimal> script = CSharpScript.Create<decimal>(code,
               ScriptOptions.Default.WithImports("System").WithEmitDebugInformation(false), typeof(SimpleEvaluatorGlobals));

            ScriptRunner<decimal> runner = script.CreateDelegate();

            return runner;
        }

    }

    public class SimpleEvaluatorGlobals
    {
        public decimal A { get; set; }
        public decimal B { get; set; }
    }
}