using BugTrackerBackendAPI.Models.Projects;
using System.Collections.ObjectModel;

namespace BugTrackerBackendAPI.Models
{
    public partial class Project
    {
        /// <summary>
        /// Get all projects for specified user GUID
        /// </summary>
        /// <param name="userGuid">Guid of the user to get project list from</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ProjectInfoWithTicketCount> GetHighestTicketCount(string accesstoken, string connectionString)
        {
            ProjectInfoWithTicketCount projectInfoWithTicketCount = new ProjectInfoWithTicketCount();

            Data.DbHelper.Procedure.Executor reader = new Data.DbHelper.Procedure.Executor(connectionString);

            string query = "GetHighestCountingTicketProject";
            try
            
            {
                var result = await reader.Execute<Authentications.Authentication, ProjectInfoWithTicketCount>(query, new Authentications.Authentication { accesstoken = accesstoken });
                foreach (var item in result)
                {
                    projectInfoWithTicketCount = item;
                    break;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return projectInfoWithTicketCount;
        }
    }
}
