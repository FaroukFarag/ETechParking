namespace ETechParking.Reporting.Dtos;

public class GenerateReportDto
{
    public string ReportName { get; set; } = "";
    public string DatasetName { get; set; } = "";
    public string Format { get; set; } = "pdf";
}
