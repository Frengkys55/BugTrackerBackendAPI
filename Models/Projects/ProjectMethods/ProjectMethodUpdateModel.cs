using BugTrackerBackendAPI.Data.DbHelper;
using BugTrackerBackendAPI.Models.Misc;
using Microsoft.OpenApi.Writers;
using SkiaSharp;
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
        public async Task UpdateProject(Project project, string connectionString)
        {
            string folderRelativePath = Path.Combine("UserData", "Projects");
            int iconThreadId = -1;
            int backgroundThreadId = -1;

            #region Icon saving

            // Check project's icon. Is it a blob or url by trying to convert from base64string to byte array
            byte[] iconData = Array.Empty<byte>()!;
            try
            {
                iconData = Convert.FromBase64String(project.IconUrl!);

                // Only execute when icon data length is not equal to the size required for image with resolution of 32x32 pixels
                // Current limitation: I'm using threads to save the image to disk and leave it immediately. That means, if an excepion was thrown, main thread would not able to capture it.
                if (iconData.Length > (32 * 32))
                {
                    try
                    {
                        #region Setup save location
                        string fileName = Guid.NewGuid() + ".png";
                        string iconPath = Path.Combine(env.WebRootPath, folderRelativePath, "Icons", fileName);
                        project.IconUrl = fileName;
                        #endregion Setup save location

                        // Method to save the image to disk (executed using Threading.Thread() method)
                        var saveIcon = async (byte[] data, string path) =>
                        {
                            try
                            {
                                using var iconStream = new MemoryStream();

                                using var icon = SKBitmap.Decode(iconData);
                                icon.Encode(iconStream, SKEncodedImageFormat.Png, 100);

                                // Save the image
                                await new Data.Misc.StreamWriter().Write(iconStream, iconPath);
                            }
                            catch (Exception)
                            {
                                // For now, just catch the exception
                            }
                        };

                        // Assign Action to thread
                        Thread iconThread = new Thread(() =>
                        {
                            saveIcon(iconData, iconPath);
                        });
                        iconThreadId = iconThread.ManagedThreadId;
                        iconThread.IsBackground = false;
                        iconThread.Start();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                } 
            }
            catch
            {
                // Skip (content might be just an image url)
            }

            #endregion Background saving
            // Check project's icon. Is it a blob or url by trying to convert from base64string to byte array
            byte[] backgroundData = Array.Empty<byte>()!;
            try
            {
                backgroundData = Convert.FromBase64String(project.BackgroundImageUrl!);

                // Only execute when icon data is not null and the length is not equal to the size required for image with resolution of 32x32 pixels
                // Current limitation: I'm using threads to save the image to disk and leave it immediately. That means, if an excepion was thrown, main thread might not able to relay it to the user on time (I'm focusing on performance right now).
                if (backgroundData.Length > (32 * 32))
                {
                    try
                    {
                        #region Setup save location
                        string fileName = Guid.NewGuid() + ".png";
                        string backgroundPath = Path.Combine(env.WebRootPath, folderRelativePath, "Backgrounds", fileName);
                        project.BackgroundImageUrl = fileName;
                        #endregion Setup save location

                        // Method to save the image to disk (executed using Threading.Thread() method)
                        var saveBackground = async (byte[] data, string path) =>
                        {
                            try
                            {
                                using var backgroundStream = new MemoryStream();

                                using var background = SKBitmap.Decode(backgroundData);
                                background.Encode(backgroundStream, SKEncodedImageFormat.Png, 100);

                                backgroundStream.Position = 0;

                                // Save the image
                                await new Data.Misc.StreamWriter().Write(backgroundStream, backgroundPath, true);
                            }
                            catch (Exception)
                            {
                                // For now, just catch the exception
                                throw;
                            }
                        };

                        // Assign Action to thread
                        Thread backgroundThread = new Thread(() =>
                        {
                            saveBackground(backgroundData, backgroundPath);
                        });
                        backgroundThreadId = backgroundThread.ManagedThreadId;
                        backgroundThread.IsBackground = false;
                        backgroundThread.Start();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            catch
            {
                // Skip (content might be just an image url)
            }

            
            #region Background saving

            #endregion Background saving
            GenericWrite<Project> genericWrite = new GenericWrite<Project>();
            List<string> ignoreList = new List<string>
            {
                "Id",
                nameof(project.DateModified),
                nameof(project.DateCreated),
                //nameof(project.IconUrl),
                //nameof(project.BackgroundImageUrl)
            };

            List<KeyValuePair<string, dynamic>> additionalData = new List<KeyValuePair<string, dynamic>>()
            {
                new KeyValuePair<string, dynamic>("BackGroundImageUrl", project.BackgroundImageUrl!)
            };

            try
            {
                var result = await new Data.DbHelper.Procedure.Executor(connectionString).Execute<Project, DbResultModel>("EditProject", project, ignoreList);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
