using BugTrackerBackendAPI.Models.Misc;
using Microsoft.AspNetCore.Identity;

namespace BugTrackerBackendAPI.Models.Authentications
{
    public partial class Authentication
    {
        public async Task<int> Logout(string accesstoken, string connectionString)
        {
            if (string.IsNullOrEmpty(accesstoken)) throw new ArgumentNullException(nameof(accesstoken));
            if (connectionString == null) throw new ArgumentNullException(nameof(connectionString));

            string procedureName = "RemoveUserToken";

            try
            {
                var logoutProperty = new Authentication { accesstoken = accesstoken };

                var obj = await new Data.DbHelper.Procedure.Executor(connectionString).Execute<Authentication, DbResultModel> (procedureName, logoutProperty);
                return obj.ToList()[0].Result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
