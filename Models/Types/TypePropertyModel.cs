using System.ComponentModel.DataAnnotations;

namespace BugTrackerBackendAPI.Models
{
    public partial class Type
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid Guid { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }
    }
}
