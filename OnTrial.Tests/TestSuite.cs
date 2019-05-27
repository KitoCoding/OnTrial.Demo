using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnTrial.Automation;
using OpenQA.Selenium;
using System;

namespace OnTrial.FOCI
{
    [TestClass]
    public class BaseTest
    {
        [AssemblyInitialize]
        public static void Assembly(TestContext pContext)
        {
            OnTrial.Construct<DefaultConstruction>()
               .AddFileLogger("log.txt")
               .AddProperties(pContext.Properties)
               .Build();
        }

        [TestClass]
        public class TestSuite
        {
            private static WebBrowser browser { get; set; }

            [ClassInitialize]
            public static void ClassInitialize(TestContext pContext)
            {
                browser = new ChromeBrowser(Convert.ToString(OnTrial.Properties["browser.url"]));
            }

            [ClassCleanup]
            public static void ClassCleanup()
            {
                browser.Close();
            }

            /// <summary>
            /// This test case will test the website if we can get by the anti spam check. 
            /// </summary>
            [TestMethod]
            public void AntiSpam_Test()
            {
                Pages.Home homePage = new Pages.Home(browser);
                Assert.IsTrue(homePage.Title == browser.Driver.Title);
                homePage.Header.ContactButton.Click();

                Pages.Contact contactPage = new Pages.Contact(browser);
                Assert.IsTrue(contactPage.Title == browser.Driver.Title);

                contactPage.NameTextBox.SendText("BOT");
                contactPage.EmailTextBox.SendText("ro@bot.ca");
                contactPage.MessageTextBox.SendText("Anti-Spam is failing");

                // Orignally i was looking at building an evaluation expression engine which will parse out values into a readable equation, 
                // but then i realized, the question isn't changing...
                //thirteen minus 6 = 7...
                contactPage.QuestionTextbox.SendText("7");
                contactPage.SendButton.Click();
                
                Assert.IsTrue(browser.Driver.FindElement(By.XPath("//div[1]/p[1]")).Text == "Your form has been successfully submitted.");
            }
        }
    }
}