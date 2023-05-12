using Microsoft.OpenApi.Writers;
using System.Collections.ObjectModel;

namespace BugTrackerBackendAPI.Models
{
    public partial class Project
    {
        /// <summary>
        /// Remove a project from database (cruD)
        /// </summary>
        /// <param name="guid">Guid of the project to delete</param>
        /// <exception cref="NotImplementedException"></exception>
        public void DeleteProject(Guid guid)
        {
            throw new NotImplementedException();
        }
    }
}
