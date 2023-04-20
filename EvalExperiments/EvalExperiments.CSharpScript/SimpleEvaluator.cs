using CSScriptLib;

namespace EvalExperiments.CSharpScript
{
    public class SimpleEvaluator
    {
        public decimal Eval(decimal a, decimal b)
        {
            ISimpleEvaluator calc = CSScript.Evaluator
                .LoadCode<ISimpleEvaluator>(@"using System;
                                        public class Script
                                        {
                                            public decimal Sum(decimal a, decimal b)
                                            {
                                                return a+b;
                                            }
                                        }");
            
            var result = calc.Sum(a, b);

            return result;
        }
    }
}