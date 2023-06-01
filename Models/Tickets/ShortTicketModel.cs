namespace BugTrackerBackendAPI.Models
{
    public class ShortTicket
    {
        public Guid Guid { get; set; }
        public string? Name { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
