using Microsoft.Extensions.Logging;

namespace OnTrial
{
    public static class ILoggingBuilderExtensions
    {
        /// <summary>
        /// Adds a new file logger to the specific path
        /// </summary>
        /// <param name="pBuilder">The log builder to add to</param>
        /// <param name="pPath">The path where to write to</param>
        /// <returns></returns>
        public static ILoggingBuilder AddFile(this ILoggingBuilder pBuilder, string pPath, FileLoggerConfiguration pConfiguration = null)
        {
            // Add file log provider to builder
            pBuilder.AddProvider(new FileLoggerProvider(pPath, pConfiguration ?? new FileLoggerConfiguration()));
            return pBuilder;
        }

        /// <summary>
        /// Adds a new database logger to the specified datasource
        /// </summary>
        /// <param name="pBuilder"></param>
        /// <param name="pDataSource"></param>
        /// <param name="pConfiguration"></param>
        /// <returns></returns>
        public static ILoggingBuilder AddDatabaseLogger(this ILoggingBuilder pBuilder, string pDataSource, DatabaseLoggerConfiguration pConfiguration = null)
        {
            // Add database log provider to builder
            pBuilder.AddProvider(new DatabaseLoggerProvider(pDataSource, pConfiguration ?? new DatabaseLoggerConfiguration()));
            return pBuilder;
        }
    }
}
