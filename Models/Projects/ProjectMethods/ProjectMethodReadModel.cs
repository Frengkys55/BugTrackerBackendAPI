using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Writers;
using System.Collections.ObjectModel;

namespace BugTrackerBackendAPI.Models
{
    public partial class Project
    {

        /// <summary>
        /// Get detailed project information (cRud) 
        /// </summary>
        /// <param name="guid">Guid of the specified project</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Project GetProject(Guid guid, string accesstoken, string connectionString)
        {
            Data.DbHelper.GenericRead<Project> readProject = new Data.DbHelper.GenericRead<Project>();
            return readProject.Read("SELECT * FROM GetProjectDetail('" + guid + "', '" + accesstoken + "')", connectionString).ToList()[0];
        }
    }

}
