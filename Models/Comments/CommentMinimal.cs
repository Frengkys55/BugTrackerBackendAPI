namespace BugTrackerBackendAPI.Models.Comments
{
    public class CommentMinimal
    {
        public Guid Guid { get; set; }
        public string? TicketName { get; set; }
        public string? CommentText { get; set; }
        public DateTime? DateCreated { get; set; } = null;
    }
}
