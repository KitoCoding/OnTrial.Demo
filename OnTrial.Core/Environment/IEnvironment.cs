namespace OnTrial
{
    /// <summary>
    /// Details about the current environment
    /// </summary>
    public interface IEnvironment
    {
        /// <summary>
        /// The configuration of the environment, typically Development or Production
        /// </summary>
        string Configuration { get; }

        /// <summary>
        /// True if we are in a development (specifically, debuggable) environment
        /// </summary>
        bool IsDevelopment { get; }

        /// <summary>
        /// Indicates if we are a mobile platform
        /// </summary>
        bool IsMobile { get; }

        /// <summary>
        /// The machine name of the current enviornment
        /// </summary>
        string MachineName { get; }

        /// <summary>
        /// The machines executable location
        /// </summary>
        string ExecutableLocation { get; }
    }
}