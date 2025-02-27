using ETechParking.Reporting.Dtos;
using ETechParking.Reporting.Dtos.Tickets;

namespace ETechParking.Reporting.Interfaces;

public interface IReportService
{
    (byte[] ReportData, string ContentType, string FileExtension) GetShiftsReport(ShiftReportFilterDto shiftReportFilterDto);
    (byte[] ReportData, string ContentType, string FileExtension) GetTicketsReport(TicketReportFilterDto ticketReportFilterDto);
}
