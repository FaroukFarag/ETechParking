using ETechParking.Reporting.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ETechParking.WebApi.Controllers.Reports;

[Route("api/[controller]")]
[ApiController]
public class ReportsController(IReportService reportService) : ControllerBase
{
    private readonly IReportService _reportService = reportService;

    [HttpGet("GetShiftsReport")]
    public IActionResult GetShiftsReport(string format = "PDF")
    {
        var (reportBytes, contentType, fileExtension) = _reportService.GetShiftsReport(format!);
        return File(reportBytes, contentType, $"ShiftsReport.{fileExtension}");
    }

    [HttpGet("GetTicketsReport")]
    public IActionResult GetTicketsReport(string format = "PDF")
    {
        var (reportBytes, contentType, fileExtension) = _reportService.GetTicketsReport(format!);
        return File(reportBytes, contentType, $"TicketsReport.{fileExtension}");
    }
}
