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
        public Project GetProject(Guid guid)
        {
            Project project = new Project
            {
                Name = "Implement this method this method is currently hard coded",
                Guid = Guid.NewGuid(),
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                Description = "Replace this placeholder text with the real one",
                Id = 1,
                ProjectIconUrl = "https://www.frengkysinaga.com/Sources/Images/collei_honkaipose.png",
                ProjectBackgroundImageUrl = "https://www.frengkysinaga.com/Sources/Background/Collei.jpg"
            };
            return project;
        }
    }

}
