using System.Drawing;
using QRCoder;

namespace testQrCode
{
    public interface IQrCode
    {
        Bitmap Get(string url, int pixel);
    }
    
    public class RaffQrCode : IQrCode
    {
        public Bitmap Get(string url, int pixel)
        {
            var qrCodeGenerator = new QRCodeGenerator();
            var qrCodeData = qrCodeGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.M, true);
            var qrCode = new QRCode(qrCodeData);
            return qrCode.GetGraphic(pixel, Color.Black, Color.White, true);
        }
    }
}