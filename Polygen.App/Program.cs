using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Autofac;
using Microsoft.Extensions.CommandLineUtils;

namespace Polygen.App
{
    public class Program : CommandLineApplication
    {
        private CommandOption _projectConfigurationFileOption;
        private CommandOption _tempDirOption;

        public Program()
        {
            Name = "Polygen";
            Description = "Code generator application";

            HelpOption("-?|-h|--help");
            _projectConfigurationFileOption = Option("-c|--config <file>",
                "Path to the ProjectConfiguration.xml file.",
                CommandOptionType.SingleValue);
            _tempDirOption = Option("--tempdir <dir>",
                "Path to the temporary directory used during code generation.",
                CommandOptionType.SingleValue);

            OnExecute(() => ExecuteApp());
        }

        private List<Assembly> LoadPluginAssemblies()
        {
            var pluginAssemblies = new List<Assembly>
            {
                Assembly.Load(new AssemblyName("Polygen.Plugins.Base")),
                Assembly.Load(new AssemblyName("Polygen.Plugins.NHibernate"))
            };

            return pluginAssemblies;
        }

        private Core.Runner CreateRunner(List<Assembly> pluginAssemblies)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new Core.AutofacModule());
            builder.RegisterModule(new Templates.HandlebarsNet.AutofacModule());
            builder.RegisterAssemblyModules(pluginAssemblies.ToArray());

            builder
                .RegisterType<Core.Runner>()
                .As<Core.Runner>()
                .SingleInstance();

            var container = builder.Build();

            return container.Resolve<Core.Runner>();
        }

        private void ExecuteRunner(Core.Runner runner)
        {
            var currentDir = Directory.GetCurrentDirectory();
            var projectConfigurationFilePath = Path.Combine(currentDir, "ProjectConfiguration.xml");

            if (_projectConfigurationFileOption.HasValue())
            {
                projectConfigurationFilePath = Path.GetFullPath(_projectConfigurationFileOption.Value());
            }

            if (!File.Exists(projectConfigurationFilePath))
            {
                throw new Exception($"Project configuration file '{projectConfigurationFilePath}' does not exist.");
            }

            var tempDirPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

            if (_tempDirOption.HasValue())
            {
                tempDirPath = Path.GetFullPath(_tempDirOption.Value());
            }

            if (!Directory.Exists(tempDirPath))
            {
                Directory.CreateDirectory(tempDirPath);
            }

            runner.Execute(new Core.RunnerConfiguration
            {
                ProjectConfigurationFile = projectConfigurationFilePath,
                TempFolder = tempDirPath
            });
        }

        private int ExecuteApp()
        {
            var pluginAssemblies = default(List<Assembly>);

            try
            {
                pluginAssemblies = LoadPluginAssemblies();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Error while loading plugin assemblies: " + e.Message);
                Console.Error.WriteLine();
                Console.Error.WriteLine(e);
                return 1;
            }

            var runner = default(Core.Runner);

            try
            {
                runner = CreateRunner(pluginAssemblies);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Error while during startup: " + e.Message);
                Console.Error.WriteLine();
                Console.Error.WriteLine(e);
                return 2;
            }

            try
            {
                ExecuteRunner(runner);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                Console.Error.WriteLine();
                Console.Error.WriteLine(e);
                return 3;
            }

            return 0;
        }

        static void Main(string[] args)
        {
            var app = new Program();

            app.Execute(args);
        }
    }
}