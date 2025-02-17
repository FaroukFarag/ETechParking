using ETechParking.Reporting.Interfaces;
using Microsoft.Reporting.NETCore;
using System.Data;
using System.Reflection;

namespace ETechParking.Reporting.Services;

public class ReportService : IReportService
{
    public byte[] GenerateReport<T>(List<T> data, string reportName, string datasetName, string format)
    {
        var localReport = new LocalReport();
        string rdlcPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Reports", $"{reportName}.rdlc");

        if (!File.Exists(rdlcPath))
        {
            throw new FileNotFoundException($"RDLC file '{reportName}.rdlc' not found.");
        }

        localReport.ReportPath = rdlcPath;

        var dataTable = ConvertToDataTable(data);

        localReport.DataSources.Add(new ReportDataSource(datasetName, dataTable));

        string outputFormat = format.ToUpper() switch
        {
            "EXCEL" => "EXCELOPENXML",
            "WORD" => "WORDOPENXML",
            _ => "PDF"
        };

        return localReport.Render(outputFormat);
    }

    private static DataTable ConvertToDataTable<T>(List<T> data)
    {
        var dataTable = new DataTable(typeof(T).Name);
        PropertyInfo[] properties = typeof(T).GetProperties();

        foreach (var prop in properties)
        {
            dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        }

        foreach (var item in data)
        {
            var values = new object[properties.Length];

            for (int i = 0; i < properties.Length; i++)
            {
                values[i] = properties[i].GetValue(item) ?? DBNull.Value;
            }

            dataTable.Rows.Add(values);
        }

        return dataTable;
    }

}
