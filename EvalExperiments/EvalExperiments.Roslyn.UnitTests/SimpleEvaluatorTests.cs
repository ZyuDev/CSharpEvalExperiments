using System.Threading.Tasks;
using NUnit.Framework;

namespace EvalExperiments.Roslyn.UnitTests;

[TestFixture]
public class SimpleEvaluatorTests
{
    [Test]
    public async Task SimpleEval_ReturnValue()
    {
        var evaluator = new SimpleEvaluator();
        var result = await evaluator.Eval(1.5m, 2.5m);
        
        Assert.AreEqual(4, result);
    }

    [Test]
    public async Task DelegateEval_ReturnValue()
    {
        var evaluator = new SimpleEvaluator();
        var result = await evaluator.EvalDelegate(1.5m, 2.5m);

        Assert.AreEqual(4, result);
    }
}