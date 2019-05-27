using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace OnTrial.Automation
{
    public class DropDown : Element
    {
        private SelectElement sElement => new SelectElement(base.mWebElement);

        public DropDown(IWebElement pWebElement) : base(pWebElement) { }

        public IWebElement SelectedOption() =>
            sElement.SelectedOption;

        public void DeselectAll() =>
            sElement.DeselectAll();

        public void SelectByIndex(int pValue) =>
            sElement.SelectByIndex(pValue);
        public void DeselectByIndex(int pValue) =>
            sElement.DeselectByIndex(pValue);

        public void SelectByText(string pValue) =>
            sElement.SelectByText(pValue);
        public void DeselectByText(string pValue) =>
            sElement.DeselectByText(pValue);

        public void SelectByValue(string pValue) =>
            sElement.SelectByValue(pValue);
        public void DeselectByValue(string pValue) =>
            sElement.DeselectByValue(pValue);
    }
}
