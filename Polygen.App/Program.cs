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
            this.Name = "Polygen";
            this.Description = "Code generator application";

            this.HelpOption("-?|-h|--help");
            this._projectConfigurationFileOption = this.Option("-c|--config <file>",
                "Path to the ProjectConfiguration.xml file.",
                CommandOptionType.SingleValue);
            this._tempDirOption = this.Option("--tempdir <dir>",
                "Path to the temporary directory used during code generation.",
                CommandOptionType.SingleValue);

            this.OnExecute(() => this.ExecuteApp());
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

            if (this._projectConfigurationFileOption.HasValue())
            {
                projectConfigurationFilePath = Path.GetFullPath(this._projectConfigurationFileOption.Value());
            }

            if (!File.Exists(projectConfigurationFilePath))
            {
                throw new Exception($"Project configuration file '{projectConfigurationFilePath}' does not exist.");
            }

            var tempDirPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

            if (this._tempDirOption.HasValue())
            {
                tempDirPath = Path.GetFullPath(this._tempDirOption.Value());
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
                pluginAssemblies = this.LoadPluginAssemblies();
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
                this.ExecuteRunner(runner);
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