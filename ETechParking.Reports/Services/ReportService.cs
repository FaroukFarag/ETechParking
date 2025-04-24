using ETechParking.Application.Interfaces.Locations.Users;
using ETechParking.Reporting.Dtos;
using ETechParking.Reporting.Dtos.Tickets;
using ETechParking.Reporting.ETechParkingDataSetTableAdapters;
using ETechParking.Reporting.Interfaces;
using Microsoft.Reporting.NETCore;
using System.Data;
using System.Reflection;

namespace ETechParking.Reporting.Services;

public class ReportService(IUserService userService) : IReportService
{
    private readonly IUserService _userService = userService;

    public async Task<(byte[] ReportData, string ContentType, string FileExtension)> GetShiftsReport(ShiftReportFilterDto shiftReportFilterDto, int userId)
    {
        string reportName = "Shifts";
        var adapter = new ShiftsDataTableTableAdapter();
        DataTable dataTable = adapter.GetData(
            shiftReportFilterDto.FromDateTime,
            shiftReportFilterDto.ToDateTime,
            shiftReportFilterDto.LocationId,
            shiftReportFilterDto.CashierUserId,
            shiftReportFilterDto.AccountantUserId);

        return await GenerateReport(reportName, "ShiftDataSet", dataTable, shiftReportFilterDto.Format, userId);
    }

    public async Task<(byte[] ReportData, string ContentType, string FileExtension)> GetTicketsReport(TicketReportFilterDto ticketReportFilterDto, int userId)
    {
        string reportName = "Tickets";
        var adapter = new TicketsDataTableTableAdapter();
        DataTable dataTable = adapter.GetData(
            ticketReportFilterDto.FromDateTime,
            ticketReportFilterDto.ToDateTime,
            ticketReportFilterDto.LocationId,
            ticketReportFilterDto.CreateUserId,
            ticketReportFilterDto.CloseUserId);

        return await GenerateReport(reportName, "TicketDataSet", dataTable, ticketReportFilterDto.Format, userId);
    }

    private async Task<(byte[] ReportData, string ContentType, string FileExtension)> GenerateReport(
        string reportName,
        string dataSet,
        DataTable dataTable,
        string format,
        int userId)
    {
        var localReport = new LocalReport();
        string rdlcPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Reports", $"{reportName}.rdlc");
        var user = await _userService.GetAsync(userId);

        if (!File.Exists(rdlcPath))
        {
            throw new FileNotFoundException($"RDLC file '{reportName}.rdlc' not found.");
        }

        ReportParameter createdByParam = new("CreatedBy", user.UserName);
        ReportParameter createdAtParam = new("CreatedAt", DateTime.Now.ToString());

        localReport.ReportPath = rdlcPath;
        localReport.DataSources.Add(new ReportDataSource(dataSet, dataTable));
        localReport.SetParameters(new[] { createdByParam, createdAtParam });

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
