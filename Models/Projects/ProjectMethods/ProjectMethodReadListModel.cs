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
        public Collection<ShortProjectInfo> GetProjectsList(Guid userGuid)
        {
            Collection<ShortProjectInfo> shortProjectInfoList = new Collection<ShortProjectInfo>();
            for (int i = 0; i < new Random().Next(3, 10); i++)
            {
                shortProjectInfoList.Add(new ShortProjectInfo
                {
                    Guid = Guid.NewGuid(),
                    Name = "Replace this placeholder text (" + i.ToString() + ")"
                });
            }

            return shortProjectInfoList;
        }
    }
}
