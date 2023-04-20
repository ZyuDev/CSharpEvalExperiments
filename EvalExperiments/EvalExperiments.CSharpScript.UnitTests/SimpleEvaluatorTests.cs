using CSScriptLib;
using NUnit.Framework;

namespace EvalExperiments.CSharpScript.UnitTests;

public interface ICalc
{
    int Sum(int a, int b);
}

[TestFixture]
public class SimpleEvaluatorTests
{
    [Test]
    public void Eval_ReturnValue()
    {
        var evaluator = new SimpleEvaluator();
        var result = evaluator.Eval(1.5m, 2.5m);
        
        Assert.AreEqual(4m, result);
    }

    [Test]
    public void FirstTest()
    {
        ICalc calc = CSScript.Evaluator
            .LoadCode<ICalc>(@"using System;
                                        public class Script
                                        {
                                            public int Sum(int a, int b)
                                            {
                                                return a+b;
                                            }
                                        }");
        int result = calc.Sum(1, 2);
        
        Assert.Pass();
    }
}