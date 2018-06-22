using System.Collections.Generic;
using System.IO;
using System.Linq;
using Polygen.Core.DataType;
using Polygen.Core.DesignModel;
using Polygen.Core.Exceptions;
using Polygen.Core.Impl;
using Polygen.Core.Impl.File;
using Polygen.Core.Impl.Project;
using Polygen.Core.NamingConvention;
using Polygen.Core.OutputModel;
using Polygen.Core.Parser;
using Polygen.Core.Project;
using Polygen.Core.Schema;
using Polygen.Core.Stage;
using Polygen.Core.Utils;

namespace Polygen.Core
{
    public class Runner
    {
        private readonly ISchemaCollection _schemaCollection;
        private readonly IXmlElementParser _xmlElementParser;
        private readonly IDesignModelParser _designModelParser;
        private readonly Context _context;
        private RunnerConfiguration _configration;

        public Runner(
            ISchemaCollection schemaCollection,
            IXmlElementParser xmlElementParser,
            IDesignModelParser designModelParser,
            IDesignModelCollection designModelCollection,
            IOutputModelCollection outputModelCollection,
            IStageHandlerRegistry stageHandlers,
            IDataTypeRegistry dataTypeRegistry,
            IEnumerable<IClassNamingConvention> classNamingConventions
        )
        {
            this._schemaCollection = schemaCollection;
            this._xmlElementParser = xmlElementParser;
            this._designModelParser = designModelParser;

            this._context = new Context
            {
                Schemas = schemaCollection,
                DesignModels = designModelCollection,
                OutputModels = outputModelCollection,
                DataTypeRegistry = dataTypeRegistry,
                MainOutputConfiguration = designModelCollection.RootNamespace.OutputConfiguration,
                StageHandlers = stageHandlers
            };
        }

        public IContext Context => this._context;

        /// <summary>
        /// Runs the code generation process.
        /// </summary>
        /// <param name="config"></param>
        public virtual void Execute(RunnerConfiguration config)
        {
            this._configration = config;

            this.Initialize();
            this.RegisterSchemas();
            this.ParseProjectConfiguration(config.ProjectConfigurationFile);
            this.RegisterTemplates();
            this.ConfigureTargetPlatforms();
            this.InitializeOutputConfiguration();
            this.ParseInputXmlFilesFiles();
            this.ParseDesignModels();
            this.ApplyProjectLayout();
            this.GenerateOutputModels();
            this.WriteOutputFiles(config.TempFolder);
            this.CopyOutputFilesToProjectFolders();
            this.Cleanup();
        }

        /// <summary>
        /// Initializes the code generation framework.
        /// </summary>
        public virtual void Initialize()
        {
            // Indicate that the context has been initialize.
            this.FireStageEvent(StageType.Initialize);
        }

        /// <summary>
        /// Registers all needed schemas. Those are usually defined by plugins.
        /// </summary>
        public virtual void RegisterSchemas()
        {
            // Fire the generic event.
            this.FireStageEvent(StageType.RegisterSchemas);
        }

        /// <summary>
        /// Parses the ProjectConfiguration.xml file.
        /// </summary>
        /// <param name="projectConfigurationFile"></param>
        public virtual void ParseProjectConfiguration(string projectConfigurationFile)
        {
            this.FireStageEvent(StageType.BeforeParseProjectConfiguration);

            var schema = this._schemaCollection.GetSchemaByNamespace(CoreConstants.ProjectConfiguration_SchemaNamespace);

            using (var reader = System.IO.File.OpenText(projectConfigurationFile))
            {
                var dummyProject = new Impl.Project.Project("init", "init", Path.GetDirectoryName(projectConfigurationFile));
                var dummyProjectFile = new ProjectFile(dummyProject, Path.GetFileName(projectConfigurationFile));
                var projectConfigurationElement = this._xmlElementParser.Parse(reader, schema, dummyProjectFile);

                this._designModelParser.Parse(projectConfigurationElement, this._context.DesignModels);

                var parsedDesignModel = this._context.DesignModels.GetByType(CoreConstants.DesignModelType_ProjectConfiguration).FirstOrDefault();

                if (parsedDesignModel == null)
                {
                    throw new ConfigurationException("No project configuration design model found.");
                }

                var projectConfiguration = parsedDesignModel as IProjectConfiguration;

                this._context.Configuration = projectConfiguration ?? throw new ConfigurationException("Project configuration object must implement interface IProjectConfiguration");
                this._context.Projects = projectConfiguration.Projects;
            }

            this.FireStageEvent(StageType.AfterParseProjectConfiguration);
            this.FireStageEvent(StageType.ValidateProjectConfiguration);
        }

        /// <summary>
        /// Plugins register all their templates.
        /// </summary>
        public virtual void RegisterTemplates()
        {
            this.FireStageEvent(StageType.RegisterTemplates);
        }

        /// <summary>
        /// Plugins register all supported target platforms.
        /// </summary>
        public virtual void ConfigureTargetPlatforms()
        {
            this.FireStageEvent(StageType.ConfigureTargetPlatforms);
        }

        /// <summary>
        /// Registers all needed output templates and output folders. Those are usually defined by plugins.
        /// </summary>
        public virtual void InitializeOutputConfiguration()
        {
            this.FireStageEvent(StageType.InitializeOutputConfiguration);
        }

        /// <summary>
        /// Parses all XML files into IXmlElement objects.
        /// </summary>
        public virtual void ParseInputXmlFilesFiles()
        {
            this.FireStageEvent(StageType.BeforeParseInputXmlFiles);
            this.FireStageEvent(StageType.ParseInputXmlFiles);
            this.FireStageEvent(StageType.AfterParseInputXmlFiles);
        }

        /// <summary>
        /// Parses all design model elements into design models.
        /// </summary>
        /// <param name="designModelFiles"></param>
        public virtual void ParseDesignModels()
        {
            this.FireStageEvent(StageType.BeforeParseDesignModels);
            this.FireStageEvent(StageType.ParseDesignModels);
            this.FireStageEvent(StageType.AfterParseDesignModels);
        }

        /// <summary>
        /// Applies the project layout configuration to all parsed design models.
        /// </summary>
        public virtual void ApplyProjectLayout()
        {
            this.FireStageEvent(StageType.ApplyProjectLayout);
        }

        /// <summary>
        /// Notifies plugins that they should generate IOutputModel objects from the design models.
        /// </summary>
        public void GenerateOutputModels()
        {
            this.FireStageEvent(StageType.BeforeGenerateOutputModels);
            this.FireStageEvent(StageType.GenerateOutputModels);
            this.FireStageEvent(StageType.AfterOutputModelsGenerated);
        }

        /// <summary>
        /// Writes the output models into file system.
        /// </summary>
        /// <param name="outputFolder"></param>
        public void WriteOutputFiles(string outputFolder)
        {
            foreach (var project in this._context.Projects.Projects)
            {
                ((Impl.Project.Project)project).SetTempFolder(Path.Combine(outputFolder, project.Name));
            }

            this.FireStageEvent(StageType.BeforeWriteOutputFiles);

            foreach (var outputModel in this._context.OutputModels.Models)
            {
                if (outputModel.File == null)
                {
                    throw new OutputModelException(outputModel, "Output file is not set.");
                }

                if (outputModel.Renderer == null)
                {
                    throw new OutputModelException(outputModel, "Output model render not set.");
                }

                using (var writer = outputModel.File.OpenAsTextForWriting())
                {
                    outputModel.Renderer.Render(outputModel, writer);
                }
            }

            this.FireStageEvent(StageType.AfterWriteOutputFiles);
        }

        /// <summary>
        /// Copies output model files from the temporary folder to the project folders.
        /// Existing files are skipped, overwritten or merged depending on the output model type.
        /// </summary>
        public void CopyOutputFilesToProjectFolders()
        {
            this.FireStageEvent(StageType.BeforeCopyOutputFiles);

            foreach (var outputModel in this._context.OutputModels.Models)
            {
                if (outputModel.File == null)
                {
                    throw new OutputModelException(outputModel, "Output file is not set.");
                }

                var projectFile = outputModel.File.GetSourcePath(true);
                var projectFileDir = Path.GetDirectoryName(projectFile);

                if (!Directory.Exists(projectFileDir))
                {
                    Directory.CreateDirectory(projectFileDir);
                }

                var sourceFile = new FileInfo(outputModel.File.GetTempPath(true));
                var destinationFile = new FileInfo(projectFile);
                var fileMerger = FileMergerFactory.GetForMode(outputModel.MergeMode);
                fileMerger.Merge(sourceFile, destinationFile);
            }

            this.FireStageEvent(StageType.AfterCopyOutputFiles);
        }

        /// <summary>
        /// Cleans up the code generation data.
        /// </summary>
        public void Cleanup()
        {
            this.FireStageEvent(StageType.Cleanup);
            this.FireStageEvent(StageType.Finished);
        }

        /// <summary>
        /// Calls all registered handlers for the given stage.
        /// </summary>
        /// <param name="stage"></param>
        private void FireStageEvent(StageType stage)
        {
            foreach (var handler in this._context.StageHandlers.GetHandlers(stage))
            {
                handler.Execute();
            }
        }
    }
}
