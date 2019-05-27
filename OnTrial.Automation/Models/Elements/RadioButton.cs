using OpenQA.Selenium;

namespace OnTrial.Automation
{
    public class RadioButton : Element
    {
        public RadioButton(IWebElement pWebElement) : base(pWebElement) { }

        public void Check()
        {
            if (!base.mWebElement.Selected)
                base.mWebElement.Click();
        }

        public void Click()
        {
            base.mWebElement.Click();
        }
    }
}
