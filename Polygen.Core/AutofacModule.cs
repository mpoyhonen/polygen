using Autofac;

namespace Polygen.Core
{
    public class AutofacModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<Impl.Stage.StageHandlerRegistry>()
                .As<Stage.IStageHandlerRegistry>()
                .SingleInstance();            
            builder
                .RegisterType<Impl.DataType.DataTypeRegistry>()
                .As<DataType.IDataTypeRegistry>()
                .SingleInstance();            
            builder
                .RegisterType<Impl.Schema.SchemaCollection>()
                .As<Schema.ISchemaCollection>()
                .SingleInstance();
            builder
               .RegisterType<Impl.Parser.DesignModelGeneratorFactory>()
               .As<Parser.IDesignModelGeneratorFactory>()
               .SingleInstance();
            builder
                .RegisterType<Impl.Parser.XmlElementParser>()
                .As<Parser.IXmlElementParser>()
                .SingleInstance();
            builder
                .RegisterType<Impl.Parser.DesignModelParser>()
                .As<Parser.IDesignModelParser>()
                .SingleInstance();
            builder
                .RegisterType<Impl.DesignModel.DesignModelCollection>()
                .As<DesignModel.IDesignModelCollection>()
                .SingleInstance();
            builder
                .RegisterType<Impl.Project.ProjectCollection>()
                .As<Project.IProjectCollection>()
                .SingleInstance();
            builder
                .RegisterType<Impl.Project.ProjectLayout>()
                .As<Project.IProjectLayout>()
                .SingleInstance();
            builder
                .RegisterType<Impl.OutputModel.OutputModelCollection>()
                .As<OutputModel.IOutputModelCollection>()
                .SingleInstance();
            builder
                .RegisterType<Impl.NamingConvention.NamingConventionCollection>()
                .As<NamingConvention.INamingConventionCollection>()
                .SingleInstance();
            builder
                .RegisterType<Impl.TargetPlatform.TargetPlatformCollection>()
                .As<TargetPlatform.ITargetPlatformCollection>()
                .SingleInstance();
            builder
                .RegisterType<Impl.Reference.ReferenceRegistry>()
                .As<Reference.IReferenceRegistry>()
                .SingleInstance();

            // Register all stage handler implementation in the core assembly.
            builder
                .RegisterAssemblyTypes(typeof(AutofacModule).Assembly)
                .Where(x => x.IsAssignableTo<Stage.IStageHandler>() && !x.IsAbstract)
                .As<Stage.IStageHandler>()
                .PropertiesAutowired()
                .SingleInstance();
        }
    }
}
