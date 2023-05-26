using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BugTrackerBackendAPI.Data.DbHelper
{

    /// <summary>
    /// Generic function to read from database
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericRead<T>
    {
        public ICollection<T> Read(string query, string connectionString, Collection<KeyValuePair<string, string>> parameters = null)
        {
            Collection<T> data = new Collection<T>();
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand sqlCommand = new SqlCommand(query, con);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            try
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        // Get field count
                        int fieldCount = reader.FieldCount;

                        object obj = Activator.CreateInstance(typeof(T));   // Create new instance of T
                        Type type = obj.GetType();                          // Get the type of T
                        PropertyInfo[] info = type.GetProperties();         // Get all public properties of T

                        // Set value to all properties
                        for (int i = 0; i < info.Length; i++)
                        {
                            string fieldName = info[i].Name;
                            object? value = null;
                            try
                            {
                                value = reader[fieldName];
                            }
                            catch
                            {
                                value = null;
                            }
                            info[i].SetValue(obj, value);
                        }
                        data.Add((T)obj);
                    }
                }
            }
            catch (Exception err)
            {
                throw;
            }
            finally
            {
                con.Close();
                con.DisposeAsync();
                sqlCommand.DisposeAsync();
                reader.DisposeAsync();
            }

            return data;
        }
    }
}
