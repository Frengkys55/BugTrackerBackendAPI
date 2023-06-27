using Microsoft.OpenApi.Writers;
using System.Collections.ObjectModel;
using System.Net.Security;
using System.Drawing;

namespace BugTrackerBackendAPI.Models
{
    public partial class Project
    {
        IWebHostEnvironment env;
        string appAddress = string.Empty;
        public Project()
        {

        }

        /// <summary>
        /// Use this class if you want this class to have access about the application environment.
        /// </summary>
        /// <param name="env">Application's environment</param>
        /// <param name="appAddress">Application's base address</param>
        public Project(IWebHostEnvironment env, string appAddress)
        {
            this.env = env;
            this.appAddress = appAddress;
        }

        // The other method is located inside the folder "ProjectMethods"
    }

}
