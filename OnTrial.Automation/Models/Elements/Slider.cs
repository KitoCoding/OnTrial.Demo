using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace OnTrial.Automation
{
    public class Slider : Element
    {
        /// <summary>
        /// A constructor for our element
        /// </summary>
        /// <param name="pWebElement"></param>
        public Slider(IWebElement pWebElement) : base(pWebElement) { }

        /// <summary>
        /// A method allowing a user to change the slider via pixels, will start at the farthest left
        /// </summary>
        /// <param name="pPixels"></param>
        public void MoveByPixels(int pPixels)
        {
            Actions builder = new Actions(base.mWebElement.ToDriver());
            int height = base.mWebElement.Size.Height;
            int width = base.mWebElement.Size.Width;

            if (width > height)
            {
                builder = builder.ClickAndHold(base.mWebElement);
                builder = builder.MoveByOffset((-(int)base.mWebElement.Size.Width / 2), 0);
                builder = builder.MoveByOffset(pPixels, 0);
            }
            else if (width < height)
            {
                builder = builder.ClickAndHold(base.mWebElement);
                builder = builder.MoveByOffset(0, (-(int)base.mWebElement.Size.Height / 2));
                builder = builder.MoveByOffset(0, pPixels);
            }

            builder.Release().Build().Perform();
        }

        //base.mWebElement.ToDriver().Execute<string>($"document.getElementById('{}').value = '{pValue}';");
        public void SetValue(int pValue) { }

        //base.mWebElement.ToDriver().Execute<string>($"document.getElementById('{}').value = '{pValue}';");
        public void SetValue(string pValue) { }

        public void MoveByPercentage(int pPercentage)
        {
            Actions builder = new Actions(base.mWebElement.ToDriver());

            int x = base.mWebElement.Location.X;
            int y = base.mWebElement.Location.Y;

            int width = base.mWebElement.Size.Width + x;
            int height = base.mWebElement.Size.Height + y;

            int scrollWidth = 11;

            var a = ((width / 100.0f) * pPercentage) + ((scrollWidth / 100.0f) * pPercentage);
            
            if (width > height)
            {
                //It is highly likely we are working with a horizontal slider.
                //If someone built a horizontal slider that is taller than it is wide, then... 
                //Well you might need to have a talk with your developer
                builder = builder.ClickAndHold(base.mWebElement);
                builder = builder.MoveByOffset(-(width / 2), 0);
                builder = builder.MoveByOffset((int)a, 0);
            }
            else if (width < height)
            {
                //It is highly likely we are working with a vertical slider.
                //Now this makes sense if we error out. Because you can be fat and short, we call it a choad
                builder = builder.ClickAndHold(base.mWebElement);
                builder = builder.MoveByOffset(0, -(height / 2));
                builder = builder.MoveByOffset(0, (int)((height / 100) * pPercentage) + y);
            }

            builder.Release().Build().Perform();
        }

        public void MoveToPercentage(int pPercentage)
        {
            Actions builder = new Actions(base.mWebElement.ToDriver());

            int height = base.mWebElement.Size.Height;
            int width = base.mWebElement.Size.Width;

            if (width > height)
            {
                //It is highly likely we are working with a horizontal slider.
                //If someone built a horizontal slider that is taller than it is wide, then... 
                //Well you might need to have a talk with your developer
                builder = builder.ClickAndHold(base.mWebElement);
                builder = builder.MoveByOffset(-width, 0);
                builder = builder.MoveByOffset(width, 0);
            }
            else if (width < height)
            {
                //It is highly likely we are working with a vertical slider.
                //Now this makes sense if we error out. Because you can be fat and short, we call it a choad
                builder = builder.ClickAndHold(base.mWebElement);
                builder = builder.MoveByOffset(0, 0);
                builder = builder.MoveByOffset(0, (int)((height / 100) * pPercentage));
            }

            builder.Release().Build().Perform();
        }
    }
}
