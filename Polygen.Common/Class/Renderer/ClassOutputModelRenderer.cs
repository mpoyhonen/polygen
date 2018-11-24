using System;
using System.Collections.Generic;
using System.IO;
using Polygen.Common.Class.OutputModel;
using Polygen.Core.Exceptions;
using Polygen.Core.OutputModel;
using Polygen.Core.Renderer;
using Polygen.Core.Template;

namespace Polygen.Common.Class.Renderer
{
    public class ClassOutputModelRenderer : IOutputModelRenderer
    {
        private ITemplate _template;

        public ClassOutputModelRenderer(ITemplate template)
        {
            _template = template;
        }

        public void Render(IOutputModel outputModel, TextWriter writer)
        {
            var classOutputModel = outputModel as ClassOutputModel;

            if (classOutputModel == null)
            {
                throw new RenderException("Output model must be an ClassOutputModel.");
            }

            try
            {
                var data = new Dictionary<string, object>()
                {
                    { "Model", classOutputModel }
                };

                _template.Render(data, writer);
            }
            catch (Exception e)
            {
                throw new RenderException($"Error while rendering template '{_template.Name}': {e.Message}", e);
            }
        }
    }
}
