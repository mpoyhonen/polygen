using System.Collections.Generic;
using System.Linq;
using Polygen.Core.Exceptions;
using Polygen.Core.Project;
using Polygen.Core.Utils;

namespace Polygen.Core.Impl.Project
{
    public class ProjectCollection : IProjectCollection
    {
        private readonly List<IProject> _projects = new List<IProject>();
        private readonly MultiKeyListDictionary<IProject> _projectMap = new MultiKeyListDictionary<IProject>();

        public IReadOnlyList<IProject> Projects => _projects;

        public void AddProject(IProject project)
        {
            if (_projectMap.ContainsKey(nameof(IProject.Name), project.Name))
            {
                throw new ConfigurationException($"Duplicate project '{project.Name}' defined.");
            }

            _projects.Add(project);
            _projectMap.Add(nameof(IProject.Name), project.Name, project);
            _projectMap.Add(nameof(IProject.Type), project.Type, project);
        }

        public IProject GetProjectByName(string name, bool throwIfMissing = true)
        {
            var project = _projectMap.GetOrEmpty(nameof(IProject.Name), name).FirstOrDefault();
                
            if (project == null && throwIfMissing)
            {
                throw new ConfigurationException($"Project '{name}' not found.");
            }

            return project;
        }

        public IProject GetFirstProjectByType(string type, bool throwIfMissing = true)
        {
            var project = GetProjectsByType(type).FirstOrDefault();

            if (project == null && throwIfMissing)
            {
                throw new ConfigurationException($"Project with type '{type}' not found.");
            }

            return project;
        }

        public IEnumerable<IProject> GetProjectsByType(string type)
        {
            return _projectMap.GetOrEmpty(nameof(IProject.Type), type);
        }
    }
}
