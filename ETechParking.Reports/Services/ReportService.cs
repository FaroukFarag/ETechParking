using ETechParking.Reporting.ETechParkingDataSetTableAdapters;
using ETechParking.Reporting.Interfaces;
using Microsoft.Reporting.NETCore;
using System.Data;
using System.Reflection;

namespace ETechParking.Reporting.Services;

public class ReportService : IReportService
{
    public (byte[] ReportData, string ContentType, string FileExtension) GetShiftsReport(string format)
    {
        string reportName = "Shifts";
        var adapter = new ShiftsDataTableTableAdapter();
        DataTable dataTable = adapter.GetData();

        return GenerateReport(reportName, "ShiftDataSet", dataTable, format);
    }

    public (byte[] ReportData, string ContentType, string FileExtension) GetTicketsReport(string format)
    {
        string reportName = "Tickets";
        var adapter = new TicketsDataTableTableAdapter();
        DataTable dataTable = adapter.GetData();

        return GenerateReport(reportName, "TicketDataSet", dataTable, format);
    }

    private static (byte[] ReportData, string ContentType, string FileExtension) GenerateReport(string reportName, string dataSet, DataTable dataTable, string format)
    {
        var localReport = new LocalReport();
        string rdlcPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Reports", $"{reportName}.rdlc");

        if (!File.Exists(rdlcPath))
        {
            throw new FileNotFoundException($"RDLC file '{reportName}.rdlc' not found.");
        }

        localReport.ReportPath = rdlcPath;
        localReport.DataSources.Add(new ReportDataSource(dataSet, dataTable));

        string outputFormat = format.ToUpper() switch
        {
            "EXCEL" => "EXCELOPENXML",
            "WORD" => "WORDOPENXML",
            _ => "PDF"
        };

        string contentType;
        string fileExtension;

        switch (format.ToLower())
        {
            case "excel":
                contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                fileExtension = "xlsx";
                break;
            case "word":
                contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                fileExtension = "docx";
                break;
            default:
                contentType = "application/pdf";
                fileExtension = "pdf";
                break;
        }

        var reportBytes = localReport.Render(outputFormat);

        return (reportBytes, contentType, fileExtension);
    }
}
