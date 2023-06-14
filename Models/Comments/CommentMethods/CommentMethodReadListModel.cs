using System.Collections.ObjectModel;

namespace BugTrackerBackendAPI.Models
{
    public partial class Comment
    {
        public async Task<ICollection<Comment>> GetCommentListAsync(Guid ticketGuid, string accesstoken)
        {
            List<Comment> comments = new List<Comment>();

            for (int i = 0; i < new Random().Next(2, 100); i++)
            {
                comments.Add(new Comment()
                {
                    CommentText = "Replace this placeholder text (" + i + ")",
                    Guid = Guid.NewGuid(),
                    DateCreated = DateTime.Now,
                    TicketGuid = ticketGuid
                });
            }

            return comments;
        }
    }
}
