using ETechParking.Reporting.Dtos;
using ETechParking.Reporting.Dtos.Tickets;
using ETechParking.Reporting.Interfaces;
using ETechParking.WebApi.Controllers.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETechParking.WebApi.Controllers.Reports;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class ReportsController(IReportService reportService) : BaseController
{
    private readonly IReportService _reportService = reportService;

    [HttpPost("DownloadShiftsReport")]
    public async Task<IActionResult> DownloadShiftsReport(ShiftReportFilterDto shiftReportFilterDto)
    {
        var (reportBytes, contentType, fileExtension) = await _reportService.GetShiftsReport(shiftReportFilterDto, GetCurrentUserId());
        return File(reportBytes, contentType, $"ShiftsReport.{fileExtension}");
    }

    [HttpPost("DownloadTicketsReport")]
    public async Task<IActionResult> DownloadTicketsReport(TicketReportFilterDto ticketReportFilterDto)
    {
        var (reportBytes, contentType, fileExtension) = await _reportService.GetTicketsReport(ticketReportFilterDto, GetCurrentUserId());
        return File(reportBytes, contentType, $"TicketsReport.{fileExtension}");
    }
}
