using OpenQA.Selenium;
using System;

namespace OnTrial.Automation
{
    public class WebBrowser
    {
        public IWebDriver Driver { get; set; }

        public void Close()
        {
            if (Driver == null)
                throw new NullReferenceException();

            Driver.Close();
            Driver.Quit();
        }
    }
}