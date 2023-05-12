using System.Collections.ObjectModel;

namespace BugTrackerBackendAPI.Models
{
    public partial class Comment
    {

        /// <summary>
        /// Get comment information based on given GUID
        /// </summary>
        /// <param name="commentGuid">GUID of the comments</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Comment GetComment(Guid commentGuid)
        {
            throw new NotImplementedException();
        }
    }
}
