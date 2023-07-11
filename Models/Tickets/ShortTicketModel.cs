using BugTrackerBackendAPI.Models.Tickets;

namespace BugTrackerBackendAPI.Models
{
    public class ShortTicket : TicketMinimal
    {
        public DateTime DateCreated { get; set; }
    }
}
