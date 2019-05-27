using System;
using System.IO;

namespace OnTrial
{
    /// <summary>
    /// Formats a message when the callers source information is provided first in the arguments
    /// </summary>
    public static class LoggerSourceFormatter
    {
        /// <summary>
        /// Formats the message including the source information pulled out of the state
        /// </summary>
        /// <param name="state">The state information about the log</param>
        /// <param name="exception">The exception</param>
        /// <returns></returns>
        public static string Format(object[] pState, Exception pException)
        {
            // Get the values from the state
            var origin = (string)pState[0];
            var filePath = (string)pState[1];
            var lineNumber = (int)pState[2];
            var message = (string)pState[3];

            // Get any exception message
            var exceptionMessage = pException?.ToString();

            // If we have an exception ...
            if (pException != null)
                exceptionMessage = Environment.NewLine + pException;

            // Format the message string
            return $"{message} [{Path.GetFileName(filePath)} > {origin}() > Line {lineNumber}]{exceptionMessage}";
        }
    }
}