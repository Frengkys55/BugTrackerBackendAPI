using Microsoft.Data.SqlClient;
using Microsoft.OpenApi.Writers;
using System.Collections.ObjectModel;
using System.Runtime.ExceptionServices;

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
        public void DeleteProject(Guid guid, string accesstoken, string connectionString)
        {
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
                if(result == -1)
                {
                    throw new Exception("Fail to delete the project. Did you mess up with something?");
                }
            }
            catch (SqlException err)
            {
                ExceptionDispatchInfo.Capture(err).Throw();
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
