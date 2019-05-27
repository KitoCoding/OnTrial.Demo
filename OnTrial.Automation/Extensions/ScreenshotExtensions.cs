using OpenQA.Selenium;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace OnTrial.Automation
{
    public static class ScreenshotExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pScreenshot"></param>
        /// <param name="pWebElement"></param>
        /// <returns></returns>
        public static Screenshot CropElement(this Screenshot pScreenshot, Element pWebElement)
        {
            var croptangle = new Rectangle(pWebElement.Location.X, pWebElement.Location.Y, pWebElement.Size.Width, pWebElement.Size.Height);

            Bitmap bmp; 
            using (var ms = new MemoryStream(pScreenshot.AsByteArray))
                bmp = new Bitmap(ms);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SetClip(croptangle);
                g.Clear(Color.Transparent);
            }

            return new Screenshot(Convert.ToBase64String(bmp.ToByteArray(ImageFormat.Bmp)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pScreenshot"></param>
        /// <param name="pWebElement"></param>
        /// <returns></returns>
        public static Screenshot HighlightElement(this Screenshot pScreenshot, Element pWebElement)
        {
            var croptangle = new Rectangle(pWebElement.Location.X, pWebElement.Location.Y, pWebElement.Size.Width, pWebElement.Size.Height);

            Bitmap bmp;
            using (var ms = new MemoryStream(pScreenshot.AsByteArray))
                bmp = new Bitmap(ms);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                Pen pen = new Pen(Color.Red, 2);
                pen.Alignment = PenAlignment.Inset;
                g.DrawRectangle(pen, croptangle);
            }

            return new Screenshot(Convert.ToBase64String(bmp.ToByteArray(ImageFormat.Bmp)));
        }

        /// <summary>
        /// Will compare screenshot byte for byte with a byte array
        /// </summary>
        /// <param name="pScreenshotA"></param>
        /// <param name="pBytes"></param>
        /// <returns></returns>
        public static bool Compare(this Screenshot pScreenshotA, byte[] pBytes) => pScreenshotA.AsByteArray.SequenceEqual(pBytes);
    }
}
