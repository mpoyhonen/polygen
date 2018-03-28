using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Polygen.Core.Exceptions;
using Polygen.Core.Project;

namespace Polygen.Core.Impl.Project
{
    public class ProjectLayout : IProjectLayout
    {
        private List<ProjectLayoutEntry> _entries = new List<ProjectLayoutEntry>();
        
        public ReadOnlyCollection<ProjectLayoutEntry> Entries => this._entries.AsReadOnly();

        public void AddEntry(ProjectLayoutEntry entry)
        {
            if (entry.Outputs != null)
            {
                foreach (var outputEntry in entry.Outputs)
                {
                    this.ValidateOutputEntry(outputEntry);
                }
            }

            this._entries.Add(entry);
        }

        private void ValidateOutputEntry(ProjectLayoutOutput entry)
        {
            var folderPathSet = !string.IsNullOrWhiteSpace(entry.FolderPath);
            var projectNameSet = !string.IsNullOrWhiteSpace(entry.ProjectName);
            var projectTypeSet = !string.IsNullOrWhiteSpace(entry.ProjectType);

            if (folderPathSet)
            {
                if (projectNameSet && projectTypeSet)
                {
                    throw new ConfigurationException("Both project name and type cannot be set for a project layout entry.");
                }

                if (!projectNameSet && !projectTypeSet)
                {
                    throw new ConfigurationException("Either project name or type must be set for a project layout entry.");
                }
            }
            else if (projectNameSet || projectTypeSet)
            {
                throw new ConfigurationException("Project name or type cannot be set when folder path is not set.");
            }
        }
    }
}
