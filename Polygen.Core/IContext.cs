using Polygen.Core.DataType;
using Polygen.Core.DesignModel;
using Polygen.Core.OutputConfiguration;
using Polygen.Core.OutputModel;
using Polygen.Core.Project;
using Polygen.Core.Schema;
using Polygen.Core.Stage;
using System.Collections.Generic;

namespace Polygen.Core
{
    /// <summary>
    /// Interface for code generation context which contains all the necessary data
    /// needed during model parsing and code generation.
    /// </summary>
    public interface IContext
    {
        /// <summary>
        /// All defines schemas.
        /// </summary>
        ISchemaCollection Schemas { get; }
        /// <summary>
        /// Contains all registred stage handlers.
        /// </summary>
        IStageHandlerRegistry StageHandlers { get; }
        /// <summary>
        /// Parsed project configuration.
        /// </summary>
        IProjectConfiguration Configuration { get; }
        /// <summary>
        /// All defined projects.
        /// </summary>
        IProjectCollection Projects { get; }
        /// <summary>
        /// All parsed design models.
        /// </summary>
        IDesignModelCollection DesignModels { get; }
        /// <summary>
        /// Contains all defined data types.
        /// </summary>
        IDataTypeRegistry DataTypeRegistry { get; }
        /// <summary>
        /// All generated output models.
        /// </summary>
        IOutputModelCollection OutputModels { get; }
        /// <summary>
        /// Root output configuration which defines the default configuration for all models.
        /// </summary>
        IOutputConfiguration MainOutputConfiguration { get; }
    }
}
