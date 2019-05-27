using OpenQA.Selenium;

namespace OnTrial.Automation
{
    public class Button : Element
    {
        public Button(IWebElement pWebElement) : base(pWebElement) { }

        public void Click()
        {
            base.mWebElement.Click();
        }
    }
}
