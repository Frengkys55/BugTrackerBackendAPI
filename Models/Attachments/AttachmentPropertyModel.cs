using System.ComponentModel.DataAnnotations;

namespace BugTrackerBackendAPI.Models 
{ 
    public partial class Attachment
    {
        [Key]
        [Required]
        public Guid Guid { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Location { get; set; }

        [Required]
        public long Size { get; set; }

        [Required]
        public string? AttachmentType { get; set; }

        [Required]
        public string? Icon { get; set; }

        [Required]
        public Guid CommentGuid { get; set; }
    }
}