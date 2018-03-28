using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Polygen.Core.Exceptions;
using Polygen.Core.Project;

namespace Polygen.Core.Impl.Project
{
    public class ProjectCollection : IProjectCollection
    {
        private readonly List<IProject> _projects = new List<IProject>();
        private readonly Dictionary<string, IProject> _projectByNameMap = new Dictionary<string, IProject>();
        private readonly Dictionary<string, List<IProject>> _projectsByTypeMap = new Dictionary<string, List<IProject>>();

        public IReadOnlyList<IProject> Projects => this._projects;

        public void AddProject(IProject project)
        {
            if (this._projectByNameMap.ContainsKey(project.Name))
            {
                throw new ConfigurationException($"Duplicate project '{project.Name}' defined.");
            }

            this._projects.Add(project);
            this._projectByNameMap.Add(project.Name, project);

            if (!this._projectsByTypeMap.TryGetValue(project.Type, out var projectList))
            {
                projectList = new List<IProject>();
                this._projectsByTypeMap.Add(project.Type, projectList);
            }

            projectList.Add(project);
        }

        public IProject GetProjectByName(string name, bool throwIfMissing = true)
        {
            if (!this._projectByNameMap.TryGetValue(name, out var project) && throwIfMissing)
            {
                throw new ConfigurationException($"Project '{name}' not found.");
            }

            return project;
        }

        public IProject GetFirstProjectByType(string type, bool throwIfMissing = true)
        {
            var project = this.GetProjectsByType(type).FirstOrDefault();

            if (project == null && throwIfMissing)
            {
                throw new ConfigurationException($"Project with type '{type}' not found.");
            }

            return project;
        }

        public IEnumerable<IProject> GetProjectsByType(string type)
        {
            this._projectsByTypeMap.TryGetValue(type, out var projectList);

            return projectList ?? Enumerable.Empty<IProject>();
        }
    }
}
