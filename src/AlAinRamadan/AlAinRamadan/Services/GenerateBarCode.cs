using BarcodeStandard;
using SkiaSharp;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace AlAinRamadan.Services
{
    public static class GenerateBarCode
    {
        public static string ToBarCodeString(int code)
        {
            Image img = ToImage(code);
            img.Save("barcode.gif", ImageFormat.Gif);
            string barcodeImageString = ImageToBase64(img, ImageFormat.Gif);
            return barcodeImageString;
        }

        public static Image ToImage(int code)
        {
            Barcode b = new Barcode
            {
                IncludeLabel = true
            };
            return Image
                .FromStream(b.Encode(BarcodeStandard.Type.Code128, code.ToString(), SKColors.Black, SKColors.White)
                .Encode()
                .AsStream());
        }

        public static string ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }
    }
}
