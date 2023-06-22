using BugTrackerBackendAPI.Models.Misc;
using Microsoft.AspNetCore.Identity;

namespace BugTrackerBackendAPI.Models
{
    public partial class User
    {
        /// <summary>
        /// Register new guest user
        /// </summary>
        /// <param name="user">Information about the user (assign T to DBNull or whatever while set the data properties to null</param>
        /// <param name="connectionString">Your database connection string</param>
        /// <returns>Will return the guest access token</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<string> AddUserGuestMinimal(string connectionString)
        {
            if (connectionString == null)
            {
                throw new ArgumentException(nameof(connectionString));
            }

            string query = "CreateGuestDemo";

            var executer = new Data.DbHelper.Procedure.Executor(connectionString);
            var result = await executer.Execute<DBNull, DbResultModel>(query);
            return result.ToList()[0].Result;
        }
    }
}
