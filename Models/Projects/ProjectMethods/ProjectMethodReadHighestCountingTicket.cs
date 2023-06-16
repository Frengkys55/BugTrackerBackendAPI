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
        public async Task<ShortProjectInfoWithTicketCount> GetHighestTicketCount(string accesstoken, string connectionString)
        {
            ShortProjectInfoWithTicketCount shortProjectInfoWithTicketCount = new ShortProjectInfoWithTicketCount();

            Data.DbHelper.GenericRead<ShortProjectInfoWithTicketCount> reader = new Data.DbHelper.GenericRead<ShortProjectInfoWithTicketCount>();

            string query = "USE BugTracker_Demo; SELECT * FROM GetHighestTicketCountProject('" + accesstoken + "');";
            try
            {
                var result = await reader.Read(query, connectionString);
                foreach (var item in result)
                {
                    shortProjectInfoWithTicketCount = item;
                    break;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return shortProjectInfoWithTicketCount;
        }
    }
}
