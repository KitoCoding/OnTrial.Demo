using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Reflection;
using System.Threading;

namespace OnTrial.Automation
{
    public static class IWebDriverExtensions
    {
        public static T WaitUntil<T>(this IWebDriver pDriver, Func<T> pCondition, TimeSpan? pTimeout = null)
        {
            // Validate that the condition we passed, is there. We can't wait on nothing eh?
            if (pCondition == null)
                throw new ArgumentNullException("condition", "condition cannot be null");

            // Validate that the type we have passed in is a valide type we can wait on
            if ((typeof(T).IsValueType && typeof(T) != typeof(bool)) || !typeof(object).IsAssignableFrom(typeof(T)))
                throw new ArgumentException("Can only wait on an object or boolean response, tried to use type: " + typeof(T).ToString(), "condition");

            // Set our interval variable
            var interval = TimeSpan.FromMilliseconds(Convert.ToInt64(OnTrial.Properties["driver.sleepInterval"]));

            // Set our timeout to be X seconds from now
            var endTime = DateTime.Now.Add(pTimeout ?? TimeSpan.FromMilliseconds(Convert.ToInt64(OnTrial.Properties["driver.timeout"])));

            // Loop until we return or timeout
            while (true)
            {
                try
                {
                    var result = pCondition();
                    if (typeof(T) == typeof(bool))
                    {
                        var boolResult = result as bool?;
                        if (boolResult.HasValue && boolResult.Value)
                            return result;
                    }
                    else
                    {
                        if (result != null)
                            return result;
                    }
                }
                // If we were not able to find the element, don't do anything. We are going to try again
                catch (NotFoundException) { }

                // Check the timeout after evaluating the function to ensure conditions with a zero timeout can succeed.
                if (DateTime.Now > endTime)
                {
                    //OnTrial.Log.Error($"Timed out after {pTimeout} seconds");
                    return default(T);
                }

                Thread.Sleep(interval);
            }
        }

        /// <summary>
        ///     Gets a <see cref="Screenshot"/> object representing the image of the page on the screen.
        /// </summary>
        /// <param name="pDriver">The driver instance to extend.</param>
        /// <returns>A <see cref="Screenshot"/> object containing the image.</returns>
        /// <exception cref="WebDriverException">Thrown if this <see cref="IWebDriver"/> instance does not implement <see cref="ITakesScreenshot"/>, 
        /// or the capabilities of the driver
        /// indicate that it cannot take screenshots.</exception>
        public static Screenshot TakeScreenshot(this IWebDriver pDriver)
        {
            ITakesScreenshot screenshotDriver = pDriver as ITakesScreenshot;
            IHasCapabilities capabilitiesDriver = pDriver as IHasCapabilities;

            // If our driver supports screenshot taking, lets just utilize it
            if (screenshotDriver != null)
                return screenshotDriver.GetScreenshot();

            // Make sure that our driver supports IHasCapabilities
            if (capabilitiesDriver == null)
                throw new WebDriverException("Driver does not implement ITakesScreenshot or IHasCapabilities");

            // Sometimes the driver could support the interace, but the interface could be outdated, lets validate that.
            if (!capabilitiesDriver.Capabilities.HasCapability(CapabilityType.TakesScreenshot) || !(bool)capabilitiesDriver.Capabilities.GetCapability(CapabilityType.TakesScreenshot))
                throw new WebDriverException("Driver capabilities do not support taking screenshots");

            // Now lets execute our method
            MethodInfo executeMethod = pDriver.GetType().GetMethod("Execute", BindingFlags.Instance | BindingFlags.NonPublic);

            // Now invoke our response, be sure to pass the screenshot driver command.
            Response screenshotResponse = executeMethod.Invoke(pDriver, new object[] { DriverCommand.Screenshot, null }) as Response;

            // Lets validate that we got a response, normally we should unless something funky happened.
            if (screenshotResponse == null)
                throw new WebDriverException("Unexpected failure getting screenshot; response was not in the proper format.");

            // Return the screenshot
            return new Screenshot(screenshotResponse.Value.ToString());
        }

        /// <summary>
        ///     Will execute javascript in the browser and return a generic type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pDriver"></param>
        /// <param name="pScript"></param>
        /// <returns>The generic type passed into the function</returns>
        public static T Execute<T>(this IWebDriver pDriver, string pScript, object[] pArgs = null)
        {
            IJavaScriptExecutor executor = pDriver as IJavaScriptExecutor;

            // If our driver does not support IJavaScriptExecutor, throw an error.
            if (executor == null)
                throw new WebDriverException("Driver does not implement IJavaScriptExecutor");

            // Execute the script in question
            var value = executor.ExecuteScript(pScript, pArgs);

            // Did we pass a type that is expected to be returned? If so, throw an error when value is null
            if (typeof(T).IsValueType && (Nullable.GetUnderlyingType(typeof(T)) == null) && value == null)
                throw new WebDriverException("Script returned null, but desired type is a value type");

            // If were not able to parse the value to expected type, return the value.
            if (!typeof(T).IsInstanceOfType(value))
                throw new WebDriverException("Script returned a value, but the result could not be cast to the desired type");

            // Return the value
            return (T)value;
        }
    }
}
