using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace OnTrial
{
    /// <summary>
    /// Extension methods for the OnTrial
    /// Framework
    /// </summary>
    public static class ConstructionExtensions
    {
        #region Configuration

        /// <summary>
        /// Configures a framework construction in the default way
        /// </summary>
        /// <param name="pConstruction">The construction to configure</param>
        /// <param name="cConfigure">The custom configuration action</param>
        /// <returns></returns>
        public static Construction AddDefaultConfiguration(this Construction pConstruction, Action<IConfigurationBuilder> pConfigure = null)
        {
            // Create our configuration sources
            var configurationBuilder = new ConfigurationBuilder().AddEnvironmentVariables();

            // If we are not on a mobile platform...
            if (!pConstruction.Environment.IsMobile)
            {
                configurationBuilder.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                configurationBuilder.AddXmlFile("vsts.runsettings", optional: true, reloadOnChange: true);
            }

            // Let custom configuration happen
            pConfigure?.Invoke(configurationBuilder);

            // Inject configuration into services
            var configuration = configurationBuilder.Build();
            pConstruction.Services.AddSingleton<IConfiguration>(configuration);

            // Set the construction Configuration
            pConstruction.UseConfiguration(configuration);

            // Chain the construction
            return pConstruction;
        }

        /// <summary>
        /// Configures a framework construction using the provided configuration
        /// </summary>
        /// <param name="construction">The construction to configure</param>
        /// <param name="configure">The configuration</param>
        /// <returns></returns>
        public static Construction AddConfiguration(this Construction construction, IConfiguration configuration)
        {
            // Add specific configuration
            construction.UseConfiguration(configuration);

            // Add configuration to services
            construction.Services.AddSingleton(configuration);

            // Chain the construction
            return construction;
        }

        #endregion


        /// <summary>
        /// Injects all of the default services used by the Framework for a quicker and cleaner setup
        /// </summary>
        /// <param name="pConstruction">The construction</param>
        /// <returns></returns>
        public static Construction AddDefaultServices(this Construction pConstruction)
        {
            pConstruction.AddDefaultExceptionHandler();
            pConstruction.AddDefaultLogger();
            return pConstruction;
        }


        /// <summary>
        /// Injects the default exception handler into the framework construction
        /// </summary>
        /// <param name="pConstruction">The construction</param>
        /// <returns></returns>
        public static Construction AddDefaultExceptionHandler(this Construction pConstruction)
        {
            //pConstruction.Services.AddSingleton<IExceptionHandler>(new BaseExceptionHandler());
            return pConstruction;
        }


        /// <summary>
        /// Injects the passed in default properties
        /// </summary>
        /// <param name="pConstruction"></param>
        /// <param name="pProperties"></param>
        /// <returns></returns>
        public static Construction AddProperties(this Construction pConstruction, IDictionary<string, object> pProperties)
        {
            pConstruction.Services.AddSingleton<IDictionary<string, object>>(pProperties);
            return pConstruction;
        }

        /// <summary>
        /// Injects the default logger into the framework construction
        /// </summary>
        /// <param name="pConstruction">The construction</param>
        /// <returns></returns>
        public static Construction AddDefaultLogger(this Construction pConstruction)
        {
            // Add logging as default
            pConstruction.Services.AddLogging(options =>
            {
                // Setup loggers from configuration
                options.AddConfiguration(pConstruction.Configuration.GetSection("Logging"));

                options.AddConsole();
                options.AddDebug();
            });

            // Adds a default logger so that we can get a non-generic ILogger
            // that will have the category name of "NQAP"
            pConstruction.Services.AddTransient(provider => provider.GetService<ILoggerFactory>().CreateLogger("NQAP"));

            // Chain the construction
            return pConstruction;
        }
    }
}