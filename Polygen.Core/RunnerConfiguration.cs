namespace Polygen.Core
{
    /// <summary>
    /// Contains configuration neede by the Runner class
    /// </summary>
    public class RunnerConfiguration
    {
        /// <summary>
        /// Location of the ProjectConfiguration.xml file
        /// </summary>
        public string ProjectConfigurationFile { get; set; }
        /// <summary>
        /// Generated files will be written under this folder.
        /// </summary>
        public string TempFolder { get; set; }
    }
}
