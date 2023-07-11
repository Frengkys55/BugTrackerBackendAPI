using System.Diagnostics.CodeAnalysis;

namespace BugTrackerBackendAPI.Models.Tickets
{
    public class TicketMinimal
    {
        public Guid Guid { get; set; }
        [NotNull]
        public string? Name { get; set; }
    }
}
