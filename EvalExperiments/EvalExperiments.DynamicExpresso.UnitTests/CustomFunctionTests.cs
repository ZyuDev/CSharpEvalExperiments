using System.Collections.Generic;
using System.Text;
using DynamicExpresso;
using NUnit.Framework;
using System;
using System.Data;

namespace EvalExperiments.DynamicExpresso.UnitTests
{
    public class CustomFunctionTests
    {

        public static decimal Sum(decimal a, decimal b)
        {
            return a + b;
        }

        public static decimal Total(DataTable table, string columnName)
        {
            var result = 0M;

            foreach(DataRow row in table.Rows)
            {
                var val = (decimal)row[columnName];
                result += val;
            }

            return result;
        }

        [Test]
        public void SumFunction_ReturnResult()
        {
            Func<decimal, decimal, decimal> SumFunction;
            SumFunction = Sum;

            var a = 1M;
            var b = 2.1M;
            var expression = "Sum(a, b)";

            var interpreter = new Interpreter(InterpreterOptions.DefaultCaseInsensitive)
                .SetFunction("sum", SumFunction)
                .SetVariable("a", a)
                .SetVariable("b", b);

            var result = interpreter.Eval(expression);

            Assert.AreEqual(3.1, result);

        }

        [Test]
        public void SumFunctionNested_ReturnResult()
        {
            Func<decimal, decimal, decimal> SumFunction;
            SumFunction = Sum;

            var a = 1M;
            var b = 2.1M;
            var c = 3M;
            var expression = "Sum(sum(a,c)+1, b)";

            var interpreter = new Interpreter(InterpreterOptions.DefaultCaseInsensitive)
                .SetFunction("sum", SumFunction)
                .SetVariable("a", a)
                .SetVariable("b", b)
                .SetVariable("c", c);

            var result = interpreter.Eval(expression);

            Assert.AreEqual(7.1, result);

        }

        [Test]
        public void DataTableArgument_ReturnResult()
        {
            Func<DataTable, string, decimal> TotalFunction;
            TotalFunction = Total;

            var dataTable = new DataTable();
            dataTable.Columns.Add("Product", typeof(string));
            dataTable.Columns.Add("Amount", typeof(decimal));

            var row1 = dataTable.NewRow();
            row1["Product"] = "Product 1";
            row1["Amount"] = 100M;
            dataTable.Rows.Add(row1);

            var row2 = dataTable.NewRow();
            row2["Product"] = "Product 2";
            row2["Amount"] = 200.5M;
            dataTable.Rows.Add(row2);

            var expression = "Total(table,\"Amount\")";

            var interpreter = new Interpreter(InterpreterOptions.DefaultCaseInsensitive)
                .SetFunction("total", TotalFunction)
                .SetVariable("table", dataTable);

            var result = interpreter.Eval(expression);

            Assert.AreEqual(300.5, result);

        }
    }
}
