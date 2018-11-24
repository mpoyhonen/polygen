using Polygen.Core.OutputConfiguration;
using Polygen.Core.OutputModel;
using Polygen.Core.Parser;
using System.Collections.Generic;

namespace Polygen.Core.DesignModel
{
    /// <summary>
    /// Interface for a design model parsed for a design model element data.
    /// </summary>
    public interface IDesignModel
    {
        /// <summary>
        /// Determines the design model type.
        /// </summary>
        string DesignModelType { get; }
        /// <summary>
        /// Design model name. Can be null if the model type isn't named.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Design model name prefixed with the namespace name. Can be null if the model type isn't named.
        /// </summary>
        string FullyQualifiedName { get; }
        /// <summary>
        /// Globally unique ID of this design model.
        /// </summary>
        string ObjectId { get; set; }
        /// <summary>
        /// Design model element this model was parsed from.
        /// </summary>
        IXmlElement Element { get; }
        /// <summary>
        /// Localtion where this element was parsed from.
        /// </summary>
        IParseLocationInfo ParseLocation { get; }
        /// <summary>
        /// Namespace this design model belongs to.
        /// </summary>
        INamespace Namespace { get; }
        /// <summary>
        /// Extra data for this design model.
        /// </summary>
        Dictionary<string, object> CustomData { get; }
        /// <summary>
        /// Returns all defined properties for this design model.
        /// </summary>
        IEnumerable<IDesignModelProperty> Properties { get;  }
        /// <summary>
        /// Adds a new property to this design model.
        /// </summary>
        /// <param name="property"></param>
        void AddProperty(IDesignModelProperty property);
        /// <summary>
        /// Returns a property with the given name. If the parseLocation info it set and the property is not found, an exception is thrown.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parseLocation"></param>
        /// <returns></returns>
        IDesignModelProperty GetProperty(string name, IParseLocationInfo parseLocation = null);
        /// <summary>
        /// Output configuration to be used by output models created from this design model.
        /// </summary>
        IOutputConfiguration OutputConfiguration { get; }
        /// <summary>
        /// Returns all output models registered for this design model.
        /// </summary>
        IEnumerable<IOutputModel> OutputModels { get; }
        /// <summary>
        /// Copies property values from the source design model. This copies only the properties which are
        /// present in the in schema of this design model and which do not exist in this design model.
        /// </summary>
        /// <param name="source"></param>
        void CopyPropertiesFrom(IDesignModel source, IParseLocationInfo parseLocation = null);
        /// <summary>
        /// Registers an output model for this design model.
        /// </summary>
        /// <param name="outputModel"></param>
        void AddOutputModel(IOutputModel outputModel);
    }
}
