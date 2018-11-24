using Autofac;
using Polygen.Core.DataType;
using Polygen.Core.DesignModel;
using Polygen.Core.NamingConvention;
using Polygen.Core.OutputModel;
using Polygen.Core.Parser;
using Polygen.Core.Schema;
using Polygen.Core.Stage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Polygen.Core.Tests
{
    public class TestRunner : Runner
    {
        public TestRunner(
            ISchemaCollection schemaCollection,
            IXmlElementParser designModelElementParser,
            IDesignModelParser designModelParser,
            IDesignModelCollection designModelCollection,
            IOutputModelCollection outputModelCollection,
            IStageHandlerRegistry stageHandlers,
            IDataTypeRegistry dataTypeRegistry,
            IEnumerable<IClassNamingConvention> classNamingConventions
         ) : base(
             schemaCollection, 
             designModelElementParser, 
             designModelParser, 
             designModelCollection, 
             outputModelCollection, 
             stageHandlers,
             dataTypeRegistry,
             classNamingConventions
        )
        {
        }

        public static TestRunner Create(params Module[] modules)
        {
            return Create(builder =>
            {
                foreach (var module in modules)
                {
                    builder.RegisterModule(module);
                }
            });
        }

        public static TestRunner Create(IEnumerable<Type> stageHandlerTypes)
        {
            return Create(builder =>
            {
                builder.RegisterModule(new AutofacModule());
                builder.RegisterTypes(stageHandlerTypes.ToArray())
                    .As<IStageHandler>()
                    .PropertiesAutowired();
            });
        }

        public static TestRunner Create(IEnumerable<IStageHandler> stageHandlers)
        {
            return Create(builder =>
            {
                builder.RegisterModule(new AutofacModule());

                foreach (var stageHandler in stageHandlers)
                {
                    builder
                        .RegisterInstance(stageHandler)
                        .As<IStageHandler>();
                }
            });
        }

        public static TestRunner Create(Action<ContainerBuilder> configurator)
        {
            var builder = new ContainerBuilder();

            configurator(builder);

            builder
                .RegisterType<TestRunner>()
                .As<TestRunner>()
                .SingleInstance();

            var container = builder.Build();

            return container.Resolve<TestRunner>();
        }
    }
}
