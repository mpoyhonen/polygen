using Polygen.Core.DataType;
using Polygen.Core.DesignModel;
using Polygen.Core.OutputConfiguration;
using Polygen.Core.OutputModel;
using Polygen.Core.Project;
using Polygen.Core.Schema;
using Polygen.Core.Stage;
using System.Collections.Generic;

namespace Polygen.Core.Impl
{
    public class Context : IContext
    {
        public ISchemaCollection Schemas { get; internal set; }
        public IStageHandlerRegistry StageHandlers { get; internal set; }
        public IProjectConfiguration Configuration { get; internal set; }
        public IProjectCollection Projects { get; internal set; }
        public IDesignModelCollection DesignModels { get; internal set; }
        public IDataTypeRegistry DataTypeRegistry { get; internal set; }
        public IOutputModelCollection OutputModels { get; internal set; }
        public IOutputConfiguration MainOutputConfiguration { get; internal set; }
    }
}
