using System;
using NUnit.Framework;

namespace EvalExperiments.Jint.UnitTests;

[TestFixture]
public class SimpleEvaluatorTests
{
    [Test]
    public void Eval_ReturnValue()
    {
        var evaluator = new SimpleEvaluator();
        var result = evaluator.Eval(1.5M, 2.5M);
        
        Console.Write($"result: {result}");
        
        Assert.Pass();
    }
}