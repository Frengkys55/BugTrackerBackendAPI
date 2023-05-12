using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace BugTrackerBackendAPI.Models
{
    public partial class Comment
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public Guid Guid { get; set; }

        [Required]
        public Ticket? Ticket { get; set; }

        [Required]
        public string? CommentText { get; set; }

        public Attachment? Attachment { get; set; }

        [Required]
        public DateTime? DateCreated { get; set; } = DateTime.Now;

        [Required]
        public User? User { get; set; }
    }
}
