using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace OnTrial.Automation
{
    public class Element
    {
        protected readonly IWebElement mWebElement;
        public Element(IWebElement pWebElement) => this.mWebElement = pWebElement;

        public string TagName => mWebElement.TagName;
        public string Text => mWebElement.Text;

        public bool Enabled => mWebElement.Enabled;
        public bool Displayed => mWebElement.Displayed;
        public bool Selected => mWebElement.Selected;

        public Point Location => mWebElement.Location;
        public Size Size => mWebElement.Size;

        public string GetAttribute(string pAttributeName)
        {
            throw new NotImplementedException();
        }

        public string GetCssValue(string pPropertyName)
        {
            throw new NotImplementedException();
        }

        public string GetProperty(string pPropertyName)
        {
            throw new NotImplementedException();
        }

        public Screenshot TakeScreenshot()
        {
            var screenshotDriver = (ITakesScreenshot)mWebElement.ToDriver();
            var rect = new Rectangle(mWebElement.Location.X, mWebElement.Location.Y, mWebElement.Size.Width, mWebElement.Size.Height);

            Bitmap bmp, target;
            using (var ms = new MemoryStream(screenshotDriver.GetScreenshot().AsByteArray))
                bmp = new Bitmap(ms);

            target = new Bitmap(rect.Width, rect.Height);
            using (Graphics g = Graphics.FromImage(target))
                g.DrawImage(bmp, new Rectangle(0, 0, target.Width, target.Height), rect, GraphicsUnit.Pixel);

            return new Screenshot(Convert.ToBase64String(target.ToByteArray(ImageFormat.Bmp)));
        }

        public void DragDrop(Element pElement)
        {
            Actions builder = new Actions(mWebElement.ToDriver());

            builder = builder.ClickAndHold(mWebElement);
            builder.MoveToElement(pElement.mWebElement);
            builder.Release().Build().Perform();
        }
    }
}
