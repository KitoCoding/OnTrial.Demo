using OnTrial.Automation;
using OpenQA.Selenium;

namespace OnTrial.FOCI.Pages
{
    public class Header
    {
        private WebBrowser browser;

        public Button HomeButton => new Button(browser.Driver.FindElement(By.XPath("//li[@id='menu-item-25']/a[1]")));
        public Button ServicesButton => new Button(browser.Driver.FindElement(By.XPath("//li[@id='menu-item-32']/a[1]")));
        public Button AboutUsButton => new Button(browser.Driver.FindElement(By.XPath("//li[@id='menu-item-26']/a[1]")));
        public Button OutTeamButton => new Button(browser.Driver.FindElement(By.XPath("//li[@id='menu-item-451']/a[1]")));
        public Button ArticlesButton => new Button(browser.Driver.FindElement(By.XPath("//li[@id='menu-item-28']/a[1]")));
        public Button CareersButton => new Button(browser.Driver.FindElement(By.XPath("//li[@id='menu-item-119']/a[1]")));
        public Button ContactButton => new Button(browser.Driver.FindElement(By.XPath("//li[@id='menu-item-30']/a[1]")));

        public Header(WebBrowser pBrowser) => this.browser = pBrowser;
    }
}