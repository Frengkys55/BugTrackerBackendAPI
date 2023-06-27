using BugTrackerBackendAPI.Data.DbHelper;
using Microsoft.OpenApi.Writers;
using System.Collections.ObjectModel;
using System.Drawing;

namespace BugTrackerBackendAPI.Models
{
    public partial class Project
    {

        /// <summary>
        /// Update project from database with the current given information
        /// </summary>
        /// <param name="project">Infomration about the project to be updated</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public void UpdateProject(Project project, string connectionString)
        {
            string iconAddress = string.Empty;
            string folderRelativePath = Path.Combine("UserData", "Projects");
            // Check project's icon. Is it a blob or url by trying to convert from base64string to byte array
            byte[] iconData = null;

            try
            {
                iconData = Convert.FromBase64String(project.IconUrl);

                

            }
            catch
            {
                // Skip (content might be just an image url)
            }

            if (iconData != null || iconData.Length > 300)
            {
                try
                {
                    using var stream = new MemoryStream(iconData);
                    #region Setup save location
                    string fileName = Guid.NewGuid() + ".png";
                    string iconPath = Path.Combine(env.WebRootPath, folderRelativePath, "Icons" + fileName);
                    
                    using var iconStream = new MemoryStream();

                    #endregion Setup save location


                    Bitmap icon = new Bitmap(stream);
                    icon.Save(iconStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                catch (Exception)
                {
                    throw;
                }
                
            }
            

            GenericWrite<Project> genericWrite = new GenericWrite<Project>();
            List<string> ignoreList = new List<string>
            {
                "Id",
                "DateModified",
                "DateCreated",
            };
            if (genericWrite.WriteUsingProcedure(connectionString, "EditProject", project, ignoreList) != 1)
            {
                throw new Exception("Fail to update project");
            }
        }
    }

}
