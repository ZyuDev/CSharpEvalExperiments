using Jint;

namespace EvalExperiments.Jint
{
    public class SimpleEvaluator
    {
        public object Eval(decimal a, decimal b)
        {
            var engine = new Engine().Execute("function add(a, b) { return a + b; }");

            var result = engine.Invoke("add", a, b).ToObject();

            return result;
        }
    }
}
