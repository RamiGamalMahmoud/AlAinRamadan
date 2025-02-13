using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Text;

namespace AlAinRamadan.Print
{
    internal class DirectPrint(LocalReport report, string printerName = "Default", bool isLabel = false) : IDisposable
    {
        private List<Stream> _streams;
        private int _currentPageIndex = 0;

        public DirectPrint Export()
        {
            string deviceInfo =
            @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>{pageSettings.PaperSize.Width * 100}in</PageWidth>
                <PageHeight>{pageSettings.PaperSize.Height * 100}in</PageHeight>
                <MarginTop>{pageSettings.Margins.Top * 100}in</MarginTop>
                <MarginLeft>{pageSettings.Margins.Left * 100}in</MarginLeft>
                <MarginRight>{pageSettings.Margins.Right * 100}in</MarginRight>
                <MarginBottom>{pageSettings.Margins.Bottom * 100}in</MarginBottom>
            </DeviceInfo>";

            _streams = [];
            report.Render("Image", deviceInfo, CreateStream, out Warning[] warnings);

            foreach (Stream stream in _streams)
            {
                stream.Position = 0;
            }

            return this;
        }

        public void Print()
        {
            if (_streams == null || _streams.Count == 0)
            {
                throw new Exception("Error: no stream to print.");
            }

            PrintDocument printDoc = new PrintDocument();

            if (isLabel)
            {
                PaperSize paperSize = report.GetDefaultPageSettings().PaperSize;
                printDoc.DefaultPageSettings.PaperSize = new PaperSize(paperSize.PaperName, paperSize.Height, paperSize.Width);
            }
            else
            {
                printDoc.DefaultPageSettings.Landscape = report.GetDefaultPageSettings().IsLandscape;
                printDoc.DefaultPageSettings.PaperSize = report.GetDefaultPageSettings().PaperSize;
            }

            if (printerName is not null && !string.IsNullOrEmpty(printerName) && printerName != "Default")
                printDoc.PrinterSettings.PrinterName = printerName;

            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                _currentPageIndex = 0;
                printDoc.Print();
            }
        }

        public Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            _streams.Add(stream);
            return stream;
        }

        public void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
               Metafile(_streams[_currentPageIndex]);

            // Adjust rectangular area with printer margins.
            Rectangle adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);

            // Draw a white background for the report
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // Prepare for the next page. Make sure we haven't hit the end.
            _currentPageIndex++;
            ev.HasMorePages = (_currentPageIndex < _streams.Count);
        }

        public void Dispose()
        {
            if (_streams != null)
            {
                foreach (Stream stream in _streams)
                    stream.Close();
                _streams = null;
            }
        }
    }
}
