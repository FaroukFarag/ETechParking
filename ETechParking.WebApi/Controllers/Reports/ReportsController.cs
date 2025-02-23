using ETechParking.Reporting.Dtos;
using ETechParking.Reporting.Dtos.Tickets;
using ETechParking.Reporting.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETechParking.WebApi.Controllers.Reports;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class ReportsController(IReportService reportService) : ControllerBase
{
    private readonly IReportService _reportService = reportService;

    [HttpPost("DownloadShiftsReport")]
    public IActionResult DownloadShiftsReport(ShiftReportFilterDto shiftReportFilterDto)
    {
        var (reportBytes, contentType, fileExtension) = _reportService.GetShiftsReport(shiftReportFilterDto);
        return File(reportBytes, contentType, $"ShiftsReport.{fileExtension}");
    }

    [HttpPost("DownloadTicketsReport")]
    public IActionResult DownloadTicketsReport(TicketReportFilterDto ticketReportFilterDto)
    {
        var (reportBytes, contentType, fileExtension) = _reportService.GetTicketsReport(ticketReportFilterDto);
        return File(reportBytes, contentType, $"TicketsReport.{fileExtension}");
    }
}
