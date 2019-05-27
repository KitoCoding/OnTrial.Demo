using OpenQA.Selenium;

namespace OnTrial.Automation
{
    public class Checkbox : Element
    {
        public Checkbox(IWebElement pWebElement) : base(pWebElement) { }

        public void Check()
        {
            if (!base.mWebElement.Selected)
                base.mWebElement.Click();
        }

        public void Uncheck()
        {
            if (base.mWebElement.Selected)
                base.mWebElement.Click();
        }
    }
}
