using Microsoft.Identity.Client.Utils.Windows;
using System.Drawing;
using System.Text;
using SkiaSharp;

namespace BugTrackerBackendAPI.Models
{
    public partial class Project
    {
        /// <summary>
        /// Create a new project
        /// </summary>
        /// <param name="project">Information about the rpoject to be created</param>
        /// <exception cref="NotImplementedException"></exception>
        public async Task CreateProject(Project project, string connectionString)
        {
            if (project == null) throw new ArgumentNullException(nameof(project));
            else project.accesstoken = accesstoken;
            if (connectionString == null) throw new ArgumentNullException(nameof(connectionString));

            #region Path configuration

            string applicationSavePath = Path.Combine("UserData", "Projects");

            #endregion Path configuration

            // Check the icon
            try
            {
                #region Setting up save information

                string fileName = Guid.NewGuid().ToString() + ".png";

                string path = Path.Combine(env.WebRootPath, applicationSavePath, "Icons", fileName);

                #endregion Setting up save information

                byte[] data = Convert.FromBase64String(project.IconUrl!);
                
                SKBitmap icon = SKBitmap.Decode(data);
                var skData = icon.Encode(SKEncodedImageFormat.Png, 100);

                using var iconSaveStream = new MemoryStream();
                skData.SaveTo(iconSaveStream);

                await new Data.Misc.StreamWriter().Write(iconSaveStream, path, true);

                string iconUrl = (appAddress.EndsWith("/")) ? appAddress + "/UserData/Projects/Icons/" + fileName : appAddress + "/UserData/Projects/Icons/" + fileName;
                project.IconUrl = fileName;
            }
            catch (Exception)
            {
                // Skip (probably it's uses a link)
            }

            // Check the background
            try
            {
                byte[] data = Convert.FromBase64String(project.BackgroundImageUrl!);
                string fileName = Guid.NewGuid().ToString() + ".png";

                // Load image from data array
                SKBitmap background = SKBitmap.Decode(data);

                // Save image to memory stream
                using var bgStream = new MemoryStream();
                background.Encode(bgStream, SKEncodedImageFormat.Png, 100);

                string path = Path.Combine(env.WebRootPath, applicationSavePath, "Backgrounds", fileName);
                await new Data.Misc.StreamWriter().Write(bgStream, path, true);

                string backgroundImagePath = (appAddress.EndsWith("/")) ? appAddress + "UserData/Projects/Backgrounds/" + fileName : appAddress + "/UserData/Projects/Backgrounds/" + fileName;
                project.BackgroundImageUrl = fileName;
            }
            catch (Exception)
            {
                // Skip (probably it's uses a link)
            }

            Data.DbHelper.GenericWrite<Project> writeProject = new Data.DbHelper.GenericWrite<Project>();
            List<string> ignoredProperties = new List<string>
            {
                "Id",
                "DateCreated",
                "DateModified",
            };
            int resul = writeProject.WriteUsingProcedure(connectionString, "AddProject", project, ignoredProperties);
        }
    }
}