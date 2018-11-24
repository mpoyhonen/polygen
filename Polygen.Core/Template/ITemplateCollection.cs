using System.Reflection;

namespace Polygen.Core.Template
{
    /// <summary>
    /// Contains all registered templates.
    /// </summary>
    public interface ITemplateCollection
    {
        /// <summary>
        /// Registers the given template.
        /// </summary>
        /// <param name="template"></param>
        /// <param name="overrideExisting"></param>
        void RegisterTemplate(ITemplate template, bool overrideExisting = false);
        /// <summary>
        /// Adds a template to the collectin.
        /// </summary>
        void AddTemplate(string name, string templateText, bool overrideExisting = false);
        /// <summary>
        /// Loads templates from the given folders. Later folders can override earlier folders.
        /// </summary>
        /// <param name="folders"></param>
        void LoadTemplates(params string[] folders);

        /// <summary>
        /// Loads templates from embedded resources inside the given assemblies.
        /// </summary>
        /// <param name="assembly">Assembly to scan</param>
        /// <param name="templatePath">Root path inside the assembly to include</param>
        /// <param name="templateNamePrefix">Prefix to add to the names of loaded templates. Used to fill in the plugin name so the assembly doesn't have to include it as a folder.</param>
        void LoadTemplates(Assembly assembly, string templatePath, string templateNamePrefix);

        /// <summary>
        /// Returns a template with the given name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="throwIfMissing"></param>
        /// <returns></returns>
        ITemplate GetTemplate(string name, bool throwIfMissing = true);
    }
}
