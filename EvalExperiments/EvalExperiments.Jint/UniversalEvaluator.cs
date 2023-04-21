using System.Collections.Generic;
using Jint;

namespace EvalExperiments.Jint
{
    public class UniversalEvaluator
    {
        public object Eval(string expression, Dictionary<string, object> context)
        {
            
            // Create a Jint Engine instance
            var engine = new Engine(cfg => cfg.AllowClr());

            // Reference the necessary assemblies and types
            engine.Execute(@"
                var DataTable = importNamespace('System.Data').DataTable;
                var DataColumn = importNamespace('System.Data').DataColumn;
                var DataRow = importNamespace('System.Data').DataRow;
            ");
            
            var value = engine  // Create the Jint engine
                .SetValue("context", context) // Define a "global" variable "obj"
                .Execute(expression) // Define the car() function, and call it with "obj".
                .GetCompletionValue();  // Get the last evaluated statement completion value 

            return value;
        }
    }
}