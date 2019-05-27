using OnTrial.Automation;

namespace OnTrial.FOCI.Pages
{
    public class Services
    {
        private WebBrowser browser;
        public Header Header;
        public string Title = "Services - Foci Solutions | Ottawa";
        
        public Services(WebBrowser pBrowser)
        {
            this.browser = pBrowser;
            Header = new Header(browser);
        }
    }
}