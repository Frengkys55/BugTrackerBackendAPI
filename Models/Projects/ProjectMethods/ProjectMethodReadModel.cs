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
        public async Task<Project> GetProject(Guid guid, string accesstoken, string connectionString)
        {
            Data.DbHelper.GenericRead<Project> readProject = new Data.DbHelper.GenericRead<Project>();
            try
            {
                var result = await readProject.Read("SELECT * FROM GetProjectDetail('" + guid + "', '" + accesstoken + "')", connectionString);
                Project project = new Project();
                project = result.ToList()[0];

                if (!project.IconUrl.ToLower().Contains("n/a") || !project.IconUrl.ToLower().StartsWith("http"))
                {
                    string path = Path.Combine(env.WebRootPath, "UserData", "Projects", "Icons", project.IconUrl);
                    try
                    {
                        project.IconUrl = Convert.ToBase64String(new Data.File.Reader().Read(path).ToArray());
                    }
                    catch (Exception)
                    {
                        project.IconUrl = "N/A";
                    }
                }

                if (!project.BackgroundImageUrl.ToLower().Contains("n/a") || !project.BackgroundImageUrl.ToLower().StartsWith("http"))
                {
                    string path = Path.Combine(env.WebRootPath, "UserData", "Projects", "Backgrounds", project.BackgroundImageUrl);
                    try
                    {
                        project.BackgroundImageUrl = Convert.ToBase64String(new Data.File.Reader().Read(path).ToArray());
                    }
                    catch (Exception)
                    {
                        project.BackgroundImageUrl = "N/A";
                    }
                }
                return project;
            }
            catch (Exception)
            {
                throw; 
            }
        }
    }

}
