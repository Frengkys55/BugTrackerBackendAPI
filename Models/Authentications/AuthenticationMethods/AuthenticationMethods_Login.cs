using BugTrackerBackendAPI.Models.Misc;
using Microsoft.AspNetCore.Identity;

namespace BugTrackerBackendAPI.Models.Authentications
{
    public partial class Authentication
    {
        public async Task<string> Login(UserShort user, string connectionString)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (connectionString == null) throw new ArgumentNullException(nameof(connectionString));

            string procedureName = "SetUserCredential";

            try
            {
                var obj = await new Data.DbHelper.Procedure.Executor(connectionString).Execute<UserShort, DbResultModel>(procedureName, user);
                return obj.ToList()[0].Result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
