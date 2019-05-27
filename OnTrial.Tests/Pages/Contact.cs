using OnTrial.Automation;
using OpenQA.Selenium;

namespace OnTrial.FOCI.Pages
{
    public class Contact
    {
        private WebBrowser browser;
        public Contact(WebBrowser pBrowser) => this.browser = pBrowser;
        public string Title = "Contact Us - Foci Solutions | Ottawa";

        public Textbox NameTextBox => new Textbox(browser.Driver.FindElement(By.XPath("//input[@id='ninja_forms_field_1']")));
        public Textbox EmailTextBox => new Textbox(browser.Driver.FindElement(By.XPath("//input[@id='ninja_forms_field_2']")));
        public Textbox MessageTextBox => new Textbox(browser.Driver.FindElement(By.XPath("//textarea[@id='ninja_forms_field_3']")));

        public string Question => new Element(browser.Driver.FindElement(By.XPath("//label[@id='ninja_forms_field_4_label']"))).Text;
        public Textbox QuestionTextbox => new Textbox(browser.Driver.FindElement(By.XPath("//input[@id='ninja_forms_field_4']")));

        public Button SendButton => new Button(browser.Driver.FindElement(By.XPath("//div[@id='nf_submit_1']/input[@id='ninja_forms_field_5' and @class='ninja-forms-field ' and 1]")));
    }
}