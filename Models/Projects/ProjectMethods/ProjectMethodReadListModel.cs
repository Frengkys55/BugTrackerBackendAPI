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
        public async Task<Collection<ShortProjectInfo>> GetProjectsList(string accesstoken, string connectionString)
        {
            Collection<ShortProjectInfo> shortProjectInfoList = new Collection<ShortProjectInfo>();
            Data.DbHelper.GenericRead<ShortProjectInfo> projects = new Data.DbHelper.GenericRead<ShortProjectInfo>();
            string query = "USE BugTracker_Demo; SELECT * FROM GetAllProjectListShort('" + accesstoken + "');";

            foreach (var item in await projects.Read(query, connectionString))
            {
                ShortProjectInfo shortProjectInfo = item;

                // Try loading files from disk to memory
                try
                {
                        string iconPath = Path.Combine(env.WebRootPath, "UserData", "Projects", "Icons", shortProjectInfo.IconUrl!);
                        using var iconStream = new Data.File.Reader().Read(iconPath);

                        shortProjectInfo.IconUrl = Convert.ToBase64String(iconStream.ToArray());
                        _ = iconStream.DisposeAsync();
                        string backgroundPath = Path.Combine(env.WebRootPath, "UserData", "Projects", "Backgrounds", shortProjectInfo.BackgroundImageUrl!);
                        using var backgroundStream = new Data.File.Reader().Read(backgroundPath);

                        shortProjectInfo.BackgroundImageUrl = Convert.ToBase64String(backgroundStream.ToArray());
                }
                catch (Exception)
                {
                    // Skip
                }
                shortProjectInfoList.Add(shortProjectInfo);

            }
            return shortProjectInfoList;
        }
    }
}
