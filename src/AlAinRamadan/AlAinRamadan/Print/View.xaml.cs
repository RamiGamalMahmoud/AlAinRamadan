using Microsoft.Reporting.WinForms;
using System.Collections.Generic;
using System.Windows;

namespace AlAinRamadan.Print
{
    public partial class View : Window
    {
        public View()
        {
            InitializeComponent();
        }

        public View(string reportPath, List<ReportParameter> reportParameters, Dictionary<string, object> dataSources) : this()
        {
            reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer.ZoomMode = ZoomMode.Percent;
            reportViewer.ZoomPercent = 100;
            reportViewer.LocalReport.ReportPath = reportPath;
            reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.SetParameters(reportParameters);
            reportViewer.LocalReport.DataSources.Clear();

            if (dataSources is not null)
            {
                foreach (KeyValuePair<string, object> keyValuePair in dataSources)
                {
                    reportViewer.LocalReport.DataSources.Add(new ReportDataSource(keyValuePair.Key, keyValuePair.Value));
                }
            }

            reportViewer.RefreshReport();
        }
    }
}
