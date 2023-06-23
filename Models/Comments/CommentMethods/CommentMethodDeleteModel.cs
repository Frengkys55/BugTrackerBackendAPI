using BugTrackerBackendAPI.Models.Misc;
using BugTrackerBackendAPI.Models;

namespace BugTrackerBackendAPI.Models { 
    public partial class Comment
    {
        public async Task<int> DeleteAsync(Guid commentGuid, string accesstoken, string connectionString)
        {
            Data.DbHelper.Procedure.Executor executor = new Data.DbHelper.Procedure.Executor(connectionString);

            string query = "DeleteComment";

            List<KeyValuePair<string, dynamic>> additionalData = new List<KeyValuePair<string, dynamic>>()
            {
                new KeyValuePair<string, dynamic>("Guid", commentGuid.ToString()),
                new KeyValuePair<string, dynamic>("accesstoken", accesstoken)
            };

            try
            {
                var result = await executor.Execute<DBNull, DbResultModel>(query, null, null, additionalData);
                Console.WriteLine(result.ToList()[0].Result);
                return result.ToList()[0].Result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
