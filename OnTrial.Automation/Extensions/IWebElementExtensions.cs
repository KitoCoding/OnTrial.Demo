using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using System;
using System.Globalization;

namespace OnTrial.Automation
{
    public static class IWebElementExtensions
    {
        /// <summary>
        /// Gets the web driver from the web element.
        /// </summary>
        /// <param name="pWebElement">The Web Element</param>
        /// <returns>Driver from Element</returns>
        public static IWebDriver ToDriver(this IWebElement pWebElement)
        {
            if (!(pWebElement is IWrapsDriver wrappedElement))
            {
                if (Configuration.EnableEventFiringWebDriver)
                    return ((IWrapsElement)pWebElement).WrappedElement.ToDriver();

                return (IWebDriver)pWebElement;
            }

            return wrappedElement.WrappedDriver;
        }

        /// <summary>
        /// Converts generic IWebElement into specified web element (Checkbox, Table, etc.).
        /// </summary>
        /// <typeparam name="T">Specified web element</typeparam>
        /// <param name="pWebElement"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">When constructor is null.</exception>
        private static T As<T>(this IWebElement pWebElement) where T : class, IWebElement
        {
            var constructor = typeof(T).GetConstructor(new[] { typeof(IWebElementExtensions) });

            return constructor?.Invoke(new object[] { pWebElement }) as T;
            throw new ArgumentNullException(string.Format(CultureInfo.CurrentCulture, "Consuctor for type {0} is null", typeof(T)));
        }
    }
}
