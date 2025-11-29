using MVCAllProjects.Models;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static QRCoder.PayloadGenerator;

namespace MVCAllProjects.Controllers
{
    public class QRScannerController : Controller
    {
        // GET: QRScanner
        public ActionResult CreateQRCode()
        {
            return View(new QRCodeModel());
        }
        [HttpPost]
        public ActionResult CreateQRCode(QRCodeModel qrCodeModel)
        {
            Payload payload = new Url(qrCodeModel.QRCodeText);
            QRCodeGenerator codeGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = codeGenerator.CreateQrCode(payload);
            QRCoder.PngByteQRCode pngByte = new PngByteQRCode(qRCodeData);
            var QrByte = pngByte.GetGraphic(20);
            string base64Url = Convert.ToBase64String(QrByte);
            qrCodeModel.QRImageUrl = "data:image/png;base64," + base64Url;
            return View("CreateQRCode", qrCodeModel);
        }
    }
}