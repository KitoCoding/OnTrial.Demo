using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace OnTrial
{
    /// <summary>
    /// Default implementation about the current framework environment
    /// </summary>
    public class DefaultEnvironment : IEnvironment
    {
        #region Public Properties

        /// <summary>
        /// True if we are in a development (specifically, debuggable) environment
        /// </summary>
        public bool IsDevelopment => Debugger.IsAttached == true;

        /// <summary>
        /// The configuration of the environment, either Development or Production
        /// </summary>
        public string Configuration => IsDevelopment ? "Development" : "Production";

        /// <summary>
        /// The machine name of the current environment
        /// </summary>
        public string MachineName => System.Environment.MachineName;

        /// <summary>
        ///     Getter for the executable path location
        /// </summary>
        public string ExecutableLocation => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        /// <summary>
        /// Determines (crudely) if we are a mobile (Xamarin) platform.
        /// This is a temporary, fragile check until it is officially supported 
        /// </summary>
        public bool IsMobile => RuntimeInformation.FrameworkDescription?.ToLower().Contains("mono") == true;

        /// <summary>
        /// 
        /// </summary>
        public readonly string NewLine = System.Environment.NewLine;

        /// <summary>
        /// 
        /// </summary>
        public static int TickCount { get { return System.Environment.TickCount; } }

        #endregion
    }
}