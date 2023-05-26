using Microsoft.Data.SqlClient;
using Microsoft.OpenApi.Writers;
using System.Collections.ObjectModel;

namespace BugTrackerBackendAPI.Models
{
    public partial class Project
    {
        /// <summary>
        /// Remove a project from database (cruD)
        /// </summary>
        /// <param name="guid">Guid of the project to delete</param>
        /// <param name="accesstoken">Access token to use</param>
        /// <exception cref="NotImplementedException"></exception>
        public void DeleteProject(Guid guid, string accesstoken)
        {
            string connectionString = "Data Source=ASUS\\MYDB;Initial Catalog=BugTracker_Demo;Integrated Security=True; TrustServerCertificate=True";

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string command = "DeleteProject";
            SqlCommand com = new SqlCommand(command, con);

            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@Guid", guid));
            com.Parameters.Add(new SqlParameter("@accesstoken", accesstoken));

            try
            {
                int result = com.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                throw;
            }
            finally 
            {
                com.Dispose();
                con.Close();
                con.Dispose();
            }
        }
    }
}
