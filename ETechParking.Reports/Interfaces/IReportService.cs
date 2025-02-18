namespace ETechParking.Reporting.Interfaces;

public interface IReportService
{
    (byte[] ReportData, string ContentType, string FileExtension) GetShiftsReport(string format);
    (byte[] ReportData, string ContentType, string FileExtension) GetTicketsReport(string format);
}
