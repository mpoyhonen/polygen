using Autofac;
using Polygen.Plugins.Entity.Output.Backend;
using Polygen.Core.Stage;

namespace Polygen.Plugins.Entity
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
