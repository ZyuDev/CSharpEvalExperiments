using System;
using System.Collections.Generic;
using System.Data;
using NUnit.Framework;

namespace EvalExperiments.Jint.UnitTests;

[TestFixture]
public class UniversalEvaluatorTests
{
    [Test]
    public void Eval_AddScript_ReturnValue()
    {
        var context = new Dictionary<string, object>();
        context.Add("a", 1);
        context.Add("b", 2);

        var script = "var result = context.a + context.b; result;";

        var engine = new UniversalEvaluator();
        var result = engine.Eval(script, context);
        
        Console.WriteLine($"result: {result}");
        
        Assert.Pass();
    }

    [Test]
    public void Eval_DataTableTotal_ReturnValue()
    {
        var productsTable = GenerateDataTable(5);
        var context = new Dictionary<string, object>();
        context.Add("products", productsTable);

        var script = @"
function calcTotal(table){
    var total = 0;
    for (var i = 0; i < table.rows.count; i++) {
          var row = table.rows[i];                    
          var quantity = row['Quantity'];
          row['Price'] = row['Amount'] / row['Quantity'];
          total += quantity;   
     }

    return total;
}

calcTotal(context.products);
";
        
        var engine = new UniversalEvaluator();
        var result = engine.Eval(script, context);
        
        Console.WriteLine($"result: {result}");
        foreach (DataRow row in productsTable.Rows)
        {
            Console.WriteLine($"amount: {row["Amount"]}, quantity: {row["Quantity"]} price: {row["Price"]}");
        }
        
        Assert.Pass();
    }
    
    private DataTable GenerateDataTable(int n)
    {
        DataTable table = new DataTable();
        table.Columns.Add("Product", typeof(string));
        table.Columns.Add("Quantity", typeof(decimal));
        table.Columns.Add("Amount", typeof(decimal));
        table.Columns.Add("Price", typeof(decimal));
        
        Random random = new Random();
        
        for (int i = 0; i < n; i++)
        {
            string product = "Product " + (i + 1);
            decimal quantity = random.Next(1, 100);
            decimal amount = (decimal)random.NextDouble() * 100;
            
            table.Rows.Add(product, quantity, amount);
        }
        
        return table;
    }
}