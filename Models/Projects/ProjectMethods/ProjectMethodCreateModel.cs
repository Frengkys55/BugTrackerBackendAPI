namespace BugTrackerBackendAPI.Models
{
    public partial class Project
    {
        /// <summary>
        /// Create a new project
        /// </summary>
        /// <param name="project">Information about the rpoject to be created</param>
        /// <exception cref="NotImplementedException"></exception>
        public void CreateProject(Project project, string connectionString)
        {
            if (project == null) throw new ArgumentNullException(nameof(project));
            else project.accesstoken = accesstoken;
            if (connectionString == null) throw new ArgumentNullException(nameof(connectionString));


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