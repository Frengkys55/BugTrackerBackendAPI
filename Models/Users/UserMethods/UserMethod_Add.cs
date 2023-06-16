 namespace BugTrackerBackendAPI.Models
{
    public partial class User
    {
        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="user">Information about the user</param>
        /// <param name="connectionString">Your database connection string</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<bool> AddUserMinimal(User user, string connectionString)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (connectionString == null)
            {
                throw new ArgumentException(nameof(connectionString));
            }

            string query = "AddUserMinimal";

            List<string> ignoreProperties = new List<string>
            {
                nameof(user.Id),
                nameof(user.FirstName),
                nameof(user.LastName),
                nameof(user.PhoneNumber)
            };

            Data.DbHelper.DbWriter writer = new Data.DbHelper.DbWriter();
            var result = await writer.WriteUsingProcedureGeneric(connectionString, query, user, ignoreProperties);
            if(result > 0)
                return true;
            else
                return false;
        }

        public async Task<bool> UpdateUserInformation(User user, string connectionString)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Logout user from the system
        /// </summary>
        /// <param name="accesstoken">User's access token</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> LogoutUser(string accesstoken)
        {
            if (accesstoken == null)
            {
                throw new ArgumentException(nameof (accesstoken));
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Check for user logon credential
        /// </summary>
        /// <param name="user">User credential</param>
        /// <returns>User's access token</returns>
        public async Task<string> LoginUser(UserShort user)
        {
            throw new NotImplementedException();
        }
    }
}
