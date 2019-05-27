using OnTrial.Automation;

namespace OnTrial.FOCI.Pages
{
    public class Home
    {
        private WebBrowser browser;
        public Header Header;
        public string Title = "Foci Solutions - IT Consulting | Ottawa";

        public Home(WebBrowser pBrowser)
        {
            this.browser = pBrowser;
            Header = new Header(browser);
        }
    }
}