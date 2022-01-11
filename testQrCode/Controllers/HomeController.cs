using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using SkiaSharp;
using SkiaSharp.QrCode;

namespace testQrCode.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("ok");
        }

        /// <summary>
        /// http://localhost:40144/qrcode?url=http://blog.cashwu.com&pixel=15
        /// </summary>
        /// <param name="url"></param>
        /// <param name="pixel"></param>
        /// <returns></returns>
        [HttpGet("/qrcode")]
        public FileContentResult GetQrCode(string url, int pixel = 15)
        {
            // var bitmap = _qrCode.Get(url, pixel);
            var qrCodeGenerator = new QRCoder.QRCodeGenerator();
            var qrCodeData = qrCodeGenerator.CreateQrCode(url, QRCoder.QRCodeGenerator.ECCLevel.M, true);
            var qrCode = new QRCode(qrCodeData);
            var bitmap = qrCode.GetGraphic(pixel, Color.Black, Color.White, true);

            var ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);

            return File(ms.GetBuffer(), "image/png");
        }

        /// <summary>
        /// ref https://khalidabuhakmeh.com/generate-qr-codes-csharp
        /// 
        /// http://localhost:40144/qrcode2?url=http://blog.cashwu.com
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpGet("/qrcode2")]
        public FileContentResult SaveQrCode(string url)
        {
            var info = new SKImageInfo(512, 512);
            using var surface = SKSurface.Create(info);

            var qr = new SkiaSharp.QrCode.QRCodeGenerator().CreateQrCode(url, ECCLevel.H);
            surface.Canvas.Render(qr, SKRect.Create(512, 512), 512, SKColors.Black);

            using var data = surface.Snapshot().Encode(SKEncodedImageFormat.Png, 100);

            return File(data.ToArray(), "image/png");
            
            // save to file
            // using var stream = System.IO.File.OpenWrite(@$"qr-{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}.png");
            // data.SaveTo(stream);
        }
    }
}