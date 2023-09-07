using System.Data;
using Python.Runtime;

namespace EvalExperiments.PythonNet;

public class PythonNetEval
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

    public void Example()
    {
        // path to python3*.dll
        // Runtime.PythonDLL = "C:\\Users\\Oceanshiver\\AppData\\Local\\Programs\\Python\\Python311\\python311.dll";
        PythonEngine.Initialize();
        using (Py.GIL())
        {
            using (var scope = Py.CreateScope())
            {
                var pyDataTable = GetDataTable().ToPython();

                // scope.Import("numpy", "np");

                scope.Set("DataTable", pyDataTable);

                var code = "def get_data():" +
                           "return DataTable.Rows[0][\"id\"]";

                var exec = scope.Exec(code);
                var result = exec.InvokeMethod("get_data");
            }
        }
    }
}