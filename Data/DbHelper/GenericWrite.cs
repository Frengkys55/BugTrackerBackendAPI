using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace BugTrackerBackendAPI.Data.DbHelper
{
    public class GenericWrite<T>
    {
        [Obsolete("Use DbWriter instead. I just moved this method to a new class so that you this method can be overloaded with a normal one.")]
        public int WriteUsingProcedure(string connectionString, string command, T data, IEnumerable<string>? propertyToIgnore = null, IEnumerable<KeyValuePair<string, string>>? additionalParameters = null)
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

            if(additionalParameters != null)
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
