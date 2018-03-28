using System;
using System.Collections.Generic;
using System.Text;
using Polygen.Core.DesignModel;
using Polygen.Core.OutputModel;
using Polygen.Plugins.Base;

namespace Polygen.Plugins.Entity.Output.Backend
{
    public class EntityOutputModelGenerator : IOutputModelGenerator
    {
        private readonly EntityConverter _entityConverter;

        public EntityOutputModelGenerator(EntityConverter entityConverter)
        {
            this._entityConverter = entityConverter;
        }

        public IEnumerable<IOutputModel> GenerateOutputModels(IDesignModel designModel)
        {
            var entity = (DesignModel.Entity)designModel;

            return new[] {
                this._entityConverter.CreateEntityGeneratedClass(entity, BasePluginConstants.Language_CSharp),
                this._entityConverter.CreateEntityCustomClass(entity, BasePluginConstants.Language_CSharp)
            };
        }
    }
}
