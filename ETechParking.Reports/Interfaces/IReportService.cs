namespace ETechParking.Reporting.Interfaces;

public interface IReportService
{
    byte[] GenerateReport<T>(List<T> data, string reportName, string datasetName, string format);
}
