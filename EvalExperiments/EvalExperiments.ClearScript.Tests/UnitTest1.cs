namespace EvalExperiments.ClearScript.Tests;

public class Tests
{
    [Test]
    public async Task Test1()
    {
        var e = new SimpleEvaluator();
        await e.Eval();
    }
}