using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace BugTrackerBackendAPI.Models
{
    public partial class Comment
    {
        bool _hasAttachment = false;

        [Key]
        [Required]
        public Guid Guid { get; set; }

        [Required]
        public Guid TicketGuid { get; set; }

        [Required]
        public string? CommentText { get; set; }

        public bool HasAttachment
        {
            get
            {
                return _hasAttachment;
            }
            private set
            {
                _hasAttachment = value;
            }
        }

        [Required]
        public DateTime? DateCreated { get; set; } = DateTime.Now;
    }
}
