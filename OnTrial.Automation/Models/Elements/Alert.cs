using OpenQA.Selenium;

namespace OnTrial.Automation
{
    public class Alert
    {
        private readonly IWebDriver webDriver;

        public Alert(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        public string Text
        {
            get { return this.webDriver.SwitchTo().Alert().Text; }
        }

        public void SetAuthenticationCredentials(string pUsername, string pPassword)
        {
            this.webDriver.SwitchTo().Alert().SetAuthenticationCredentials(pUsername, pPassword);
        }

        public void Accept()
        {
            this.webDriver.SwitchTo().Alert().Accept();
            this.webDriver.SwitchTo().DefaultContent();
        }

        public void Dismiss()
        {
            this.webDriver.SwitchTo().Alert().Dismiss();
            this.webDriver.SwitchTo().DefaultContent();
        }

        public void SendText(string pText)
        {
            this.webDriver.SwitchTo().Alert().SendKeys(pText);
        }
    }
}
