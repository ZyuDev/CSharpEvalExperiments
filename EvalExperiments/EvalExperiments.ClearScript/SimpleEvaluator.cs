using Microsoft.ClearScript.JavaScript;
using Microsoft.ClearScript.V8;

namespace EvalExperiments.ClearScript;

public class SimpleEvaluator
{
    public async Task<object> Eval()
    {
        using var engine = new V8ScriptEngine();
        
        Console.WriteLine("Foo bar");
        engine.AddHostType(typeof(Task));
        engine.AddHostType(typeof(Console));
        engine.AddHostType(typeof(JavaScriptExtensions));
        engine.Script.Bar = new Func<Task<string>>(Bar);
        engine.Script.Bar2 = new Func<string, string>(Bar2);
        engine.Evaluate(@"async function mainFunction() 
                        {
                            let result = await Bar().ToPromise();
                            let result2 = Bar2(result);
                            return result2;
                        }");
        var r = await engine.Evaluate("mainFunction();").ToTask();
        return r;
    }

    public async Task<string> Bar()
    {
        await Task.Delay(2000);
        return "foobar";
    }

    public string Bar2(string input)
    {
        return "bar2: " + input;
    }
}