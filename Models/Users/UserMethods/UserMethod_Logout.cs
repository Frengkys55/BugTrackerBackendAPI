using BugTrackerBackendAPI.Models.Misc;
using Microsoft.Identity.Client;
using System.Collections.ObjectModel;

namespace BugTrackerBackendAPI.Models
{
    public partial class User
    {
        /// <summary>
        /// Logout user from the system
        /// </summary>
        /// <param name="accesstoken">User's access token</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> LogoutUser(string accesstoken, string connectionString)
        {
            if (accesstoken == null)
            {
                throw new ArgumentException(nameof (accesstoken));
            }

            string query = "LogoutUser('" + accesstoken + "')";

            Data.DbHelper.GenericRead<DbResultModel> reader = new Data.DbHelper.GenericRead<DbResultModel>();
            try
            {
                var result = await reader.Read(query, connectionString);
                if (result.ToList()[0].Result == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
