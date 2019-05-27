using OpenQA.Selenium;

namespace OnTrial.Automation
{
    public class Textbox : Element
    {
        public Textbox(IWebElement pWebElement) : base(pWebElement) { }

        public void ClearText()
        {
            base.mWebElement.Clear();
        }

        public void SendText(string pText)
        {
            base.mWebElement.SendKeys(pText);
        }
    }
}