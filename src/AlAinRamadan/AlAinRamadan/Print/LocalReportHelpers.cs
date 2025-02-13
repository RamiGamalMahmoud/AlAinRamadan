using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AlAinRamadan.Print
{
    internal static class LocalReportHelpers
    {
        public static async Task<LocalReport> CreateLocalReport(string reportPath, Dictionary<string, string> parameters, Dictionary<string, object> dataSources)
        {
            return await Task.Run(() =>
            {
                List<Microsoft.Reporting.WinForms.ReportParameter> reportParameters = [];
                if (parameters is not null && parameters.Any())
                {
                    foreach (KeyValuePair<string, string> keyValue in parameters)
                    {
                        Microsoft.Reporting.WinForms.ReportParameter reportParameter = new Microsoft.Reporting.WinForms.ReportParameter(keyValue.Key, keyValue.Value);

                        reportParameters.Add(reportParameter);
                    }
                }

                LocalReport localReport = new LocalReport()
                {
                    ReportPath = reportPath
                };

                localReport.DataSources.Clear();

                if (dataSources is not null && dataSources.Count > 0)
                {
                    foreach (KeyValuePair<string, object> keyValuePair in dataSources)
                    {
                        localReport.DataSources.Add(new ReportDataSource(keyValuePair.Key, keyValuePair.Value));
                    }
                }

                localReport.EnableExternalImages = true;

                if (parameters is not null) localReport.SetParameters(reportParameters);

                localReport.Refresh();
                return localReport;
            });

        }
        public static async Task<string> Render(LocalReport localReport, string outputFileName, string format, string extension)
        {
            byte[] bytes = await Task.Run(() => localReport.Render(format, $"<DeviceInfo><OutputFormat>{extension.ToUpper()}</OutputFormat></DeviceInfo>"));

            string renderDate = DateTime.Now.ToString("yyyy_MM_dd__hh_mm_ss");
            string outputFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "AlAinRamadan", $"{format}");
            string filePath = $"{outputFolder}\\{outputFileName}-{renderDate}.{extension}";
            Directory.CreateDirectory(outputFolder);

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                await fs.WriteAsync(bytes, 0, bytes.Length);
            }

            return filePath;
        }
    }
}
