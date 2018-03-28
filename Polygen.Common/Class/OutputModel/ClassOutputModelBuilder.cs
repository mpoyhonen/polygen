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
            this._namingConvention = namingConvention;
            this._designModel = designModel;
            this._outputModelType = outputModelType;
        }

        public ClassOutputModelBuilder CreateClass(string className, INamespace classNamespace = null)
        {
            this.CreateClassInternal(className, classNamespace, false);

            return this;
        }

        public ClassOutputModelBuilder CreatePartialClass(string className, INamespace classNamespace = null)
        {
            this.CreateClassInternal(className, classNamespace, false);
            this._outputModel.Modifiers.Add(Modifiers.Partial);

            return this;
        }

        public ClassOutputModelBuilder CreateInterface(string className, INamespace classNamespace = null)
        {
            this.CreateClassInternal(className, classNamespace, true);

            return this;
        }

        private void CreateClassInternal(string className, INamespace classNamespace, bool isInterface)
        {
            classNamespace = classNamespace ?? this._designModel?.Namespace;

            if (classNamespace == null)
            {
                throw new CodeGenerationException("Namespace and design model is not set.");
            }

            this.CheckOutputModel(false);
            this._outputModel = new ClassOutputModel(this._outputModelType, classNamespace, this._designModel)
            {
                ClassName = this._namingConvention.GetClassName(className, isInterface),
                ClassNamespace = this._namingConvention.GetNamespaceName(classNamespace)
            };
            this._outputModel.Modifiers.Add(Modifiers.Public);
            this._outputModel.NamespaceImports.Add("System");
            this._outputModel.NamespaceImports.Add("System.Linq");
        }

        public ClassOutputModelBuilder CreateProperty(string name, string type, Action<Property> configurator = null)
        {
            this.CheckOutputModel();

            var property = new Property(name, type);

            property.Modifiers.Add(Modifiers.Public);
            this._outputModel.Properties.Add(property);

            configurator?.Invoke(property);

            return this;
        }

        public ClassOutputModelBuilder AddAttribute(string name, params ValueTuple<string, object>[] arguments)
        {
            this.CheckOutputModel();
            this._outputModel.Attributes.Add(new Attribute(name, arguments));

            return this;
        }

        public ClassOutputModel Build()
        {
            var res = this._outputModel;

            this._outputModel = null;

            return res;
        }

        public void SetOutputFile(IOutputConfiguration outputConfiguration, IClassNamingConvention namingConvention, string fileExtension)
        {
            this.CheckOutputModel();

            var outputFolder = outputConfiguration.GetOutputFolder(this._outputModel.Type);
            var outputFile = namingConvention.GetOutputFolderPath(this._outputModel.Namespace) + "/" + this._outputModel.ClassName + fileExtension;

            this._outputModel.File = outputFolder.GetFile(outputFile);
        }

        public void SetOutputRenderer(ITemplate template)
        {
            this.CheckOutputModel();

            this._outputModel.Renderer = new ClassOutputModelRenderer(template);
        }

        private void CheckOutputModel(bool expectSet = true)
        {
            if (expectSet && this._outputModel == null)
            {
                throw new CodeGenerationException("Output model not set.");
            }
            else if (!expectSet && this._outputModel != null)
            {
                throw new CodeGenerationException("Output model already set.");
            }
        }
    }
}
