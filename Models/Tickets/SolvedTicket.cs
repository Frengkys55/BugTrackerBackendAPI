using System.Data;

namespace BugTrackerBackendAPI.Models.Tickets
{
    public class SolvedTicket : TicketMinimal
    {
        public DateTime DateSolved { get; set; }
    }
}
