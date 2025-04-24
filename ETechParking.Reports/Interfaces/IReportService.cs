using ETechParking.Reporting.Dtos;
using ETechParking.Reporting.Dtos.Tickets;

namespace ETechParking.Reporting.Interfaces;

public interface IReportService
{
    Task<(byte[] ReportData, string ContentType, string FileExtension)> GetShiftsReport(ShiftReportFilterDto shiftReportFilterDto, int userId);
    Task<(byte[] ReportData, string ContentType, string FileExtension)> GetTicketsReport(TicketReportFilterDto ticketReportFilterDto, int userId);
}
