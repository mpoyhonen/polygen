using Polygen.Common.Class.Renderer;
using Polygen.Core.DesignModel;
using Polygen.Core.Exceptions;
using Polygen.Core.NamingConvention;
using Polygen.Core.OutputConfiguration;
using Polygen.Core.Template;
using System;

namespace Polygen.Common.Class.OutputModel
{
    public class ClassOutputModelBuilder
    {
        private readonly IClassNamingConvention _namingConvention;
        private readonly IDesignModel _designModel;
        private readonly string _outputModelType;
        private ClassOutputModel _outputModel;

        public ClassOutputModelBuilder(string outputModelType, IDesignModel designModel, IClassNamingConvention namingConvention)
        {
            _namingConvention = namingConvention;
            _designModel = designModel;
            _outputModelType = outputModelType;
        }

        public ClassOutputModelBuilder CreateClass(string className, INamespace classNamespace = null)
        {
            CreateClassInternal(className, classNamespace, false);

            return this;
        }

        public ClassOutputModelBuilder CreatePartialClass(string className, INamespace classNamespace = null)
        {
            CreateClassInternal(className, classNamespace, false);
            _outputModel.Modifiers.Add(Modifiers.Partial);

            return this;
        }

        public ClassOutputModelBuilder CreateInterface(string className, INamespace classNamespace = null)
        {
            CreateClassInternal(className, classNamespace, true);

            return this;
        }

        private void CreateClassInternal(string className, INamespace classNamespace, bool isInterface)
        {
            classNamespace = classNamespace ?? _designModel?.Namespace;

            if (classNamespace == null)
            {
                throw new CodeGenerationException("Namespace and design model is not set.");
            }

            CheckOutputModel(false);
            _outputModel = new ClassOutputModel(_outputModelType, classNamespace, _designModel)
            {
                ClassName = _namingConvention.GetClassName(className, isInterface),
                ClassNamespace = _namingConvention.GetNamespaceName(classNamespace)
            };
            _outputModel.Modifiers.Add(Modifiers.Public);
            _outputModel.NamespaceImports.Add("System");
            _outputModel.NamespaceImports.Add("System.Linq");
        }

        public ClassOutputModelBuilder CreateProperty(string name, string type, Action<Property> configurator = null)
        {
            CheckOutputModel();

            var property = new Property(name, type);

            property.Modifiers.Add(Modifiers.Public);
            _outputModel.Properties.Add(property);

            configurator?.Invoke(property);

            return this;
        }

        public ClassOutputModelBuilder AddAttribute(string name, params ValueTuple<string, object>[] arguments)
        {
            CheckOutputModel();
            _outputModel.Attributes.Add(new Attribute(name, arguments));

            return this;
        }

        public ClassOutputModel Build()
        {
            var res = _outputModel;

            _outputModel = null;

            return res;
        }

        public void SetOutputFile(IOutputConfiguration outputConfiguration, IClassNamingConvention namingConvention, string fileExtension)
        {
            CheckOutputModel();

            var outputFolder = outputConfiguration.GetOutputFolder(_outputModel.Type);
            var outputFile = namingConvention.GetOutputFolderPath(_outputModel.Namespace) + "/" + _outputModel.ClassName + fileExtension;

            _outputModel.File = outputFolder.GetFile(outputFile);
        }

        public void SetOutputRenderer(ITemplate template)
        {
            CheckOutputModel();

            _outputModel.Renderer = new ClassOutputModelRenderer(template);
        }

        private void CheckOutputModel(bool expectSet = true)
        {
            if (expectSet && _outputModel == null)
            {
                throw new CodeGenerationException("Output model not set.");
            }
            else if (!expectSet && _outputModel != null)
            {
                throw new CodeGenerationException("Output model already set.");
            }
        }
    }
}
