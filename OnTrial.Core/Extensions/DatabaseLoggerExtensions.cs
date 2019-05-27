using Microsoft.Extensions.DependencyInjection;

namespace OnTrial
{
    /// <summary>
    /// Extension methods for the <see cref="DatabaseLogger"/>
    /// </summary>
    public static class DatabaseLoggerExtensions
    {
        /// <summary>
        /// Injects a DatabaseLogger logger into the framework construction
        /// </summary>
        /// <param name="pConstruction">The construction</param>
        /// <param name="pDataSource">The datasource to log to</param>
        /// <returns></returns>
        public static Construction AddDatabaseLogger(this Construction pConstruction, string pDataSource)
        {
            // Make use of AddLogging extension
            pConstruction.Services.AddLogging(options =>
            {
                options.AddDatabaseLogger(pDataSource);
            });

            // Chain the construction
            return pConstruction;
        }
    }
}