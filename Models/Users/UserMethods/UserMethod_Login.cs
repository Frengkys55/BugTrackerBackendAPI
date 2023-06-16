using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using BugTrackerBackendAPI.Models.Misc;

namespace BugTrackerBackendAPI.Models
{
    public partial class User
    {
        /// <summary>
        /// Check for user logon credential
        /// </summary>
        /// <param name="user">User credential</param>
        /// <returns>User's access token</returns>
        public async Task<string> LoginUser(UserShort user, string connectionString)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            
            if (connectionString == null)
            {
                throw new Exception(nameof(connectionString));
            }

            string query = "CheckUser";

            Data.DbHelper.GenericRead<DbResultModel> reader = new Data.DbHelper.GenericRead<DbResultModel>();

            Collection<KeyValuePair<string, string>> parameters = new Collection<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>(nameof(user.Username), user.Username));
            parameters.Add(new KeyValuePair<string, string>(nameof(user.Password), user.Password));

            var result = await reader.Read(query, connectionString, parameters);
            if (result.ToList()[0].Result == 1)
            {
                return Guid.NewGuid().ToString();
            }
            else
            {
                throw new Exception("Login failed. Check if username or password is valid. Also check if you have registered before");
            }
        }
    }
}
