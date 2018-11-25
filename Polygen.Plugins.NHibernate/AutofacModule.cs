using Autofac;
using Polygen.Core.NamingConvention;
using Polygen.Core.Stage;
using Polygen.Plugins.NHibernate.NamingConvention;
using Polygen.Plugins.NHibernate.Output.Entity;

namespace Polygen.Plugins.NHibernate
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Register all stage handlers.
            builder
                .RegisterAssemblyTypes(typeof(AutofacModule).Assembly)
                .Where(x => x.IsAssignableTo<IStageHandler>())
                .As<IStageHandler>()
                .PropertiesAutowired()
                .SingleInstance();
            
            // Register the default naming conventions.
            builder
                .RegisterType<NHibernateNamingConvention>()
                .As<INamingConvention>()
                .SingleInstance();            

            // Register the entity output model generator.
            builder
                .RegisterType<EntityConverter>()
                .AsSelf()
                .SingleInstance();
            builder
                .RegisterType<EntityOutputModelGenerator>()
                .AsSelf()
                .SingleInstance();

        }
    }
}
