using ETechParking.Reporting.Dtos;
using ETechParking.Reporting.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ETechParking.WebApi.Controllers.Reports;

[Route("api/[controller]")]
[ApiController]
public class ReportsController(IReportService reportService) : ControllerBase
{
    private readonly IReportService _reportService = reportService;

    [HttpPost("generate")]
    public IActionResult GenerateReport([FromBody] GenerateReportDto request)
    {
        List<object> data = request.ReportName.ToLower() switch
        {
            "salesreport" => new List<object>
            {
                new { Id = 1, ProductName = "Laptop", Quantity = 5, Price = 1200 },
                new { Id = 2, ProductName = "Mouse", Quantity = 10, Price = 25 }
            },
            "customerreport" => new List<object>
            {
                new { CustomerId = 101, Name = "John Doe", Email = "john@example.com" },
                new { CustomerId = 102, Name = "Jane Doe", Email = "jane@example.com" }
            },
            _ => throw new ArgumentException("Invalid report name")
        };

        byte[] reportBytes = _reportService.GenerateReport(
            data,
            request.ReportName,
            request.DatasetName,
            request.Format
        );

        string contentType;
        string fileExtension;

        switch (request.Format.ToLower())
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

        return File(reportBytes, contentType, $"{request.ReportName}.{fileExtension}");
    }
}
