using NUnit.Framework;

namespace EvalExperiments.IronPython.UnitTests;

[TestFixture]
public class SimpleTests
{
    [Test]
    public async Task SimpleEval_Test()
    {
        var instance = new IronPythonEval();
        var result = instance.SimpleEval(4);
        
        Assert.AreEqual(14, result);
    }
}