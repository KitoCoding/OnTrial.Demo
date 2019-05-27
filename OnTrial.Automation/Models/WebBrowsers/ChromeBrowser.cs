using OpenQA.Selenium.Chrome;
using System;

namespace OnTrial.Automation
{
    public class ChromeBrowser : WebBrowser
    {
        public ChromeBrowser(string pUrl)
        {            
            ChromeOptions cOptions = new ChromeOptions();
            cOptions.AddArguments("test-type");
            cOptions.AddArguments("--disable-popup-blocking");
            cOptions.AddArguments("--disable-default-apps");
            cOptions.AddArguments("disable-infobars");
            cOptions.AddArgument("no-sandbox");
            cOptions.AddArguments("--js-flags=--expose-gc");
            cOptions.AddArguments("--enable-precise-memory-info");
            cOptions.AddArguments("--start-maximized");

            Driver = new ChromeDriver(Environment.GetEnvironmentVariable("ChromeWebDriver"), cOptions);

            Driver.Navigate().GoToUrl(pUrl);
        }
    }
}