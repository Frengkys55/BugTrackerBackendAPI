using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BugTrackerBackendAPI.Models
{
    public partial class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid Guid { get; set; }

        [Required]
        public Project Project { get; set; }

        [Required]
        internal User User { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime? DateCreated { get; set; }

        [Required]
        public DateTime? DateModified { get; set; }

        [Required]
        public Severity? Severity { get; set; }

        [Required]
        public Type Type { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }

}