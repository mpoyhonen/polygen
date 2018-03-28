using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Polygen.Common.Xml;
using Polygen.Core.DesignModel;
using Polygen.Core.OutputModel;
using Polygen.Core.Parser;
using Polygen.Core.Project;
using Polygen.Core.Schema;
using Polygen.Core.Stage;
using Polygen.Plugins.Base;
using Polygen.Plugins.Base.Output.Xsd;
using Polygen.Plugins.Entity.Output.Backend;

namespace Polygen.Plugins.Entity.StageHandler
{
    /// <summary>
    /// Creates output mopdels from the desing models.
    /// </summary>
    public class CreateOutputModels: StageHandlerBase
    {
        public CreateOutputModels(): base(StageType.GenerateOutputModels, "Entity", "Base")
        {
        }

        public IDesignModelCollection DesignModels { get; set; }
        public IProjectCollection Projects { get; set; }
        public IOutputModelCollection OutputModels { get; set; }
        public EntityConverter EntityConverter { get; set; }

        public override void Execute()
        {
            foreach (var entityDesignModel in this.DesignModels.RootNamespace.FindDesignModelsByType("Entity"))
            {
                var entity = (DesignModel.Entity)entityDesignModel;

                //this.OutputModels.AddOutputModel(this.EntityConverter.CreateEntityGeneratedClass(entity, BasePluginConstants.Language_CSharp));
                //this.OutputModels.AddOutputModel(this.EntityConverter.CreateEntityCustomClass(entity, BasePluginConstants.Language_CSharp));
            }
        }
    }
}
