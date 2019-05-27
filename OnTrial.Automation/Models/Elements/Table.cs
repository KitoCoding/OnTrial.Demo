using OpenQA.Selenium;

namespace OnTrial.Automation
{
    public class Table : Element
    {
        public Table(IWebElement pWebElement) : base(pWebElement) { }

        public string[][]  GetTable(By pRowLocator, By pColumnLocator)
        {
            var table = base.mWebElement;
            var rows = base.mWebElement.ToDriver().FindElements(pRowLocator);

            var result = new string[rows.Count][];
            var i = 0;

            foreach (var row in rows)
            {
                var cells = row.ToDriver().FindElements(pColumnLocator);
                result[i] = new string[cells.Count];

                var j = 0;
                foreach (var cell in cells)
                {
                    result[i][j++] = cell.Text;
                    //Debug("Table cell Row {0}, column {1}, Value: {2}", i, j, cell.Text);
                }

                i++;
            }

            return result;
        }
    }
}