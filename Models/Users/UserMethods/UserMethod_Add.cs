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
            try
            {
                var result = await writer.WriteUsingProcedureGeneric(connectionString, query, user, ignoreProperties);
                if (result > 0)
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
