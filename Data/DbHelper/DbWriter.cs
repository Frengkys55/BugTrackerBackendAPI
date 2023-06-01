using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace BugTrackerBackendAPI.Data.DbHelper
{
    public class DbWriter
    {

        /// <summary>
        /// Database writer helper to help write to database with custom parameter input
        /// </summary>
        /// <param name="connectionString">Database connection string</param>
        /// <param name="query">Parameter name</param>
        /// <param name="additionalParameters">Parametert to use</param>
        public int WriteUsingProcedure(string connectionString, string query, IEnumerable<KeyValuePair<string, string>> parameters)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand com = new SqlCommand(query, con);

            com.CommandType = CommandType.StoredProcedure;
            try
            {
                foreach (var parameter in parameters)
                {
                    com.Parameters.Add(new SqlParameter("@" + parameter.Key, parameter.Value));
                }
            }
            catch (Exception)
            {
                throw;
            }

            try
            {
                int result = com.ExecuteNonQuery();
                if (result == -1)
                {
                    throw new Exception("Fail to delete ticket");
                }
                else
                {
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Database writer helper to help write to database with custom parameter input
        /// </summary>
        /// <typeparam name="T">The type of object you want to add</typeparam>
        /// <param name="connectionString">Database connection string</param>
        /// <param name="command">Stored procedure to execute</param>
        /// <param name="data">The data object you want to add</param>
        /// <param name="propertyToIgnore">List of string of property you want ignore</param>
        /// <param name="additionalParameters">Additional data you want to add that is not a part of the data you want to add</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public int WriteUsingProcedureGeneric<T>(string connectionString, string command, T data, IEnumerable<string>? propertyToIgnore = null, IEnumerable<KeyValuePair<string, string>>? additionalParameters = null)
        {
            if (connectionString == string.Empty)
            {
                throw new Exception("Connections string is empty");
            }
            if (data == null)
            {
                throw new ArgumentNullException("data is null");
            }
            if (command == string.Empty)
            {
                throw new Exception("command variable is empty");
            }

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            SqlCommand sqlCommand = new SqlCommand(command, con);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (propertyToIgnore != null && propertyToIgnore.Contains(property.Name))
                {
                    continue;
                }
                else
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@" + property.Name, property.GetValue(data)));
                }
            }

            if (additionalParameters != null)
            {
                foreach (var parameter in additionalParameters)
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@" + parameter.Key, parameter.Value));
                }
            }

            try
            {
                var result = sqlCommand.ExecuteNonQuery();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
                con.DisposeAsync();
                sqlCommand.DisposeAsync();
            }
        }
    }
}