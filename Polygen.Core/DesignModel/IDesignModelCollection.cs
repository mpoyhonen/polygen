using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polygen.Core.Impl.DesignModel;
using Polygen.Core.Parser;

namespace Polygen.Core.DesignModel
{
    /// <summary>
    /// Contains all design models which are parsed from the XML.
    /// </summary>
    public interface IDesignModelCollection
    {
        /// <summary>
        /// Returns the root namespace object. This contains all namespaces.
        /// </summary>
        INamespace RootNamespace { get; }
        /// <summary>
        /// Returns namespace by name or null if none was found.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parseLocation">Optional parse location for the error message when the namespace was not found.</param>
        /// <returns></returns>
        INamespace GetNamespace(string name, IParseLocationInfo parseLocation = null);
        /// <summary>
        /// Creates a namespace with the given name or creates a new instance.
        /// </summary>
        /// <param name="ns"></param>
        /// <returns></returns>
        INamespace DefineNamespace(string ns);
        /// <summary>
        /// Returns all defined namespaces.
        /// </summary>
        /// <returns></returns>
        IEnumerable<INamespace> GetAllNamespaces();
        /// <summary>
        /// Adds a design model to this collection.
        /// </summary>
        /// <param name="designModel"></param>
        void AddDesignModel(IDesignModel designModel);
        /// <summary>
        /// Returns all design models which have the given type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IEnumerable<IDesignModel> GetByType(string type);
        /// <summary>
        /// Returns a list of all configured design models.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IDesignModel> GetAllDesignModels();
    }
}
