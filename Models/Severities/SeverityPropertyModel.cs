using System.ComponentModel.DataAnnotations;

namespace BugTrackerBackendAPI.Models
{
    public class Severity
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public Guid Guid { get; set; }

        [Required]
        public string? Title { get; set; }

        public string? Description { get; set; }
    }
}
