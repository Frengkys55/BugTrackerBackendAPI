using BugTrackerBackendAPI.Data.DbHelper.Procedure;
using BugTrackerBackendAPI.Models.Comments;
using System.Collections.ObjectModel;

namespace BugTrackerBackendAPI.Models
{
    public partial class Comment
    {
        public async Task<ICollection<CommentMinimal>> GetCommentListAsync(Guid ticketGuid, string accesstoken, string connectionString)
        {
            List<CommentMinimal> comments = new List<CommentMinimal>();

            Executor executor = new Executor(connectionString);
            string query = "ListComments";

            List<KeyValuePair<string, dynamic>> additionalData = new List<KeyValuePair<string, dynamic>>
            {
                new KeyValuePair<string, dynamic>("Guid", ticketGuid.ToString()),
                new KeyValuePair<string, dynamic>("accesstoken", accesstoken)
            };

            try
            {
                return await executor.Execute<DBNull, CommentMinimal>(query, null, null, additionalData);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
