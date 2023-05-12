using System.ComponentModel.DataAnnotations;

namespace BugTrackerBackendAPI.Models
{
    public class AttachmentType
    {

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public Guid Guid { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Extension { get; set; }

        public string? Description { get; set; }

        public string? MIMEType { get; set; }

    }
}
