using System.Diagnostics.CodeAnalysis;

namespace BugTrackerBackendAPI.Models.Tickets
{
    public class TypeModel
    {
        public Guid Guid { get; set; }

        [NotNull]
        public string? Title { get; set; }
        public string Description { get; set; }
    }
}
