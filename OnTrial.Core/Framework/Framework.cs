using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace OnTrial
{
    public static class OnTrial
    {
        #region Public Properties

        /// <summary>
        /// The framework construction used in this application.
        /// NOTE: This should be set by the consuming application at the very start of the program
        /// </summary>
        public static Construction Construction { get; set; }

        /// <summary>
        /// Gets the configuration
        /// </summary>
        public static IConfiguration Configuration => OnTrial.Provider.GetService<IConfiguration>();

        /// <summary>
        /// Gets the custom context properties for the given project
        /// </summary>
        public static IDictionary<string, object> Properties => OnTrial.Provider.GetService<IDictionary<string, object>>();

        /// <summary>
        /// Gets the default logger
        /// </summary>
        public static ILogger Log => OnTrial.Provider.GetService<ILogger>();

        /// <summary>
        /// Gets the framework environment
        /// </summary>
        public static IEnvironment Environment => OnTrial.Provider.GetService<IEnvironment>();

        /// <summary>
        /// The dependency injection service provider
        /// </summary>
        public static IServiceProvider Provider => Construction.Provider;

        /// <summary>
        /// Shortcut to Framework.Provider.GetService to get an injected service of type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type of service to get</typeparam>
        /// <returns></returns>
        public static T Service<T>() => Provider.GetService<T>();

        #endregion

        #region Extension Methods


        /// <summary>
        /// The initial call to setting up and using the Framework
        /// </summary>
        /// <typeparam name="T">The type of construction to use</typeparam>
        /// <param name="pConstruction">The instance of the construction to use</param>
        /// <returns>Construction for chaining</returns>
        public static Construction Construct<T>(T pConstruction = null) where T : Construction, new()
        {
            Construction = pConstruction ?? new T();
            return Construction;
        }

        #endregion
    }
}