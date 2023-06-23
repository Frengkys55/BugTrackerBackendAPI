using BugTrackerBackendAPI.Models.Misc;

namespace BugTrackerBackendAPI.Models
{
    public partial class Comment
    {
        public async Task<int> AddAsync(Guid ticketGuid, Comment comment, string accesstoken, string connectionString)
        {
            Data.DbHelper.Procedure.Executor executor = new Data.DbHelper.Procedure.Executor(connectionString);
            string query = "AddComment";

            List<string> ignoreProperty = new List<string>();
            ignoreProperty.Add(nameof(comment.Guid));
            ignoreProperty.Add(nameof(comment.DateCreated));
            ignoreProperty.Add(nameof(comment.HasAttachment));
            ignoreProperty.Add(nameof(comment._hasAttachment));

            List<KeyValuePair<string, dynamic>> additionalProperties = new List<KeyValuePair<string, dynamic>>();
            additionalProperties.Add(new KeyValuePair<string, dynamic>("accesstoken", accesstoken));

            try
            {
                var result = await executor.Execute<Comment, DbResultModel>(query, comment, ignoreProperty, additionalProperties);
                return result.ToList()[0].Result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
