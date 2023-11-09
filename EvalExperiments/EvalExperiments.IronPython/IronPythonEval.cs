using System.Data;
using IronPython.Hosting;

namespace EvalExperiments.IronPython;

public class IronPythonEval
{
    private DataTable GetDataTable()
    {
        var dataTable = new DataTable();
        dataTable.Columns.Add("id", typeof(int));

        var row = dataTable.NewRow();
        row["id"] = 10;
        dataTable.Rows.Add(row);

        return dataTable;
    }
    
    public int? SimpleEval(int num)
    {
        var eng = Python.CreateEngine();
        var scope = eng.CreateScope();

        var dataTable = GetDataTable();
        scope.SetVariable("DataTable", dataTable);
        
        var code = "def get_data(num):" +
                   "return DataTable.Rows[0][\"id\"] + num";
        
        eng.Execute(code, scope);
        var function = scope.GetVariable("get_data");
        
        var result = function(num);

        return result is int ? (int) result : null;
    }
}