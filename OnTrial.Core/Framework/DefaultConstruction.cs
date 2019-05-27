using Microsoft.Extensions.Configuration;
using System;

namespace OnTrial
{
    public class DefaultConstruction : Construction
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public DefaultConstruction() =>
            this.AddDefaultConfiguration().AddDefaultLogger();
            
        /// <summary>
        /// Constructor with configuration options
        /// </summary>
        public DefaultConstruction(Action<IConfigurationBuilder> pConfigure) =>
            this.AddDefaultConfiguration(pConfigure).AddDefaultLogger();

        #endregion
    }
}