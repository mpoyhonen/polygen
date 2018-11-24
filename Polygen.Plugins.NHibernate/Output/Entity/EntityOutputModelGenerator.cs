using System.Collections.Generic;
using Polygen.Core.DesignModel;
using Polygen.Core.OutputModel;
using Polygen.Plugins.Base;

namespace Polygen.Plugins.NHibernate.Output.Entity
{
    public class EntityOutputModelGenerator : IOutputModelGenerator
    {
        private readonly EntityConverter _entityConverter;

        public EntityOutputModelGenerator(EntityConverter entityConverter)
        {
            _entityConverter = entityConverter;
        }

        public IEnumerable<IOutputModel> GenerateOutputModels(IDesignModel designModel)
        {
            var entity = (Base.Models.Entity.Entity)designModel;

            return new[] {
                _entityConverter.CreateEntityGeneratedClass(entity, BasePluginConstants.Language_CSharp),
                _entityConverter.CreateEntityCustomClass(entity, BasePluginConstants.Language_CSharp)
            };
        }
    }
}
