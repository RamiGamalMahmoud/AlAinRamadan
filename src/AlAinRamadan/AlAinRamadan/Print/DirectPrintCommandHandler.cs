using MediatR;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AlAinRamadan.Print
{
    internal class DirectPrintCommandHandler : IRequestHandler<Core.Commands.Common.DirectPrintCommand>
    {
        public async Task Handle(Core.Commands.Common.DirectPrintCommand request, CancellationToken cancellationToken)
        {
            string reportPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Resources", "Reports", request.ReportName);
            await Print(reportPath, request.IsLabel, request.Parameters, request.DataSources, request.printerName);
        }

        private static async Task Print(string reportPath, bool isLabel, Dictionary<string, string> parameters, Dictionary<string, object> dataSources, string printer = "Default")
        {
            LocalReport localReport = await LocalReportHelpers.CreateLocalReport(reportPath, parameters, dataSources);
            using (DirectPrint directPrint = new DirectPrint(localReport, printer, isLabel))
            {
                directPrint.Export().Print();
            }
        }

    }
}
