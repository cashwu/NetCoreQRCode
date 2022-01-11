using System.Drawing.Imaging;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace testQrCode.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQrCode _qrCode;

        public HomeController(IQrCode qrCode)
        {
            _qrCode = qrCode;
        }

        public IActionResult Index()
        {
            return Content("ok");
        }

        /// <summary>
        /// http://localhost:40144/api/qrcode?url=http://blog.cashwu.com&pixel=15
        /// </summary>
        /// <param name="url"></param>
        /// <param name="pixel"></param>
        /// <returns></returns>
        [HttpGet("/api/qrcode")]
        public FileContentResult GetQrCode(string url, int pixel)
        {
            var bitmap = _qrCode.Get(url, pixel);

            var ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);

            return File(ms.GetBuffer(), "image/png");
        }
    }
}