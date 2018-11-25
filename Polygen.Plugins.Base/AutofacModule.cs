using Autofac;
using Polygen.Core;
using Polygen.Core.NamingConvention;
using Polygen.Core.Stage;
using Polygen.Plugins.Base.NamingConvention;
using Polygen.Plugins.Base.StageHandler;

namespace Polygen.Plugins.Base
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
            
            // Register all naming conventions.
            builder
                .RegisterAssemblyTypes(typeof(AutofacModule).Assembly)
                .Where(x => x.IsAssignableTo<INamingConvention>())
                .As<INamingConvention>()
                .SingleInstance();

            // Register temporary state object needed during file parsed.
            builder
                .RegisterType<DesignModelParseState>()
                .AsSelf()
                .SingleInstance();
        }
    }
}
