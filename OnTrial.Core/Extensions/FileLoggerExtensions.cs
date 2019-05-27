using Microsoft.Extensions.DependencyInjection;

namespace OnTrial
{
    /// <summary>
    /// Extension methods for the <see cref="FileLogger"/>
    /// </summary>
    public static class FileLoggerExtensions
    {
        /// <summary>
        /// Injects a file logger into the framework construction
        /// </summary>
        /// <param name="pConstruction">The construction</param>
        /// <param name="pLogPath">The path of the file to log to</param>
        /// <returns></returns>
        public static Construction AddFileLogger(this Construction pConstruction, string pLogPath = "log.txt")
        {
            // Make use of AddLogging extension
            pConstruction.Services.AddLogging(options =>
            {
                options.AddFile(pLogPath);
            });

            // Chain the construction
            return pConstruction;
        }
    }
}