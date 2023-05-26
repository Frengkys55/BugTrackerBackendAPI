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
        public Collection<ShortProjectInfo> GetProjectsList(string accesstoken, string connectionString)
        {
            Collection<ShortProjectInfo> shortProjectInfoList = new Collection<ShortProjectInfo>();
            Data.DbHelper.GenericRead<ShortProjectInfo> projects = new Data.DbHelper.GenericRead<ShortProjectInfo>();
            string query = "USE BugTracker_Demo; SELECT * FROM GetAllProjectListShort('" + accesstoken + "');";

            foreach (var item in projects.Read(query, connectionString))
            {
                shortProjectInfoList.Add(item);
            }
            return shortProjectInfoList;
        }
    }
}
