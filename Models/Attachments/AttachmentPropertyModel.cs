using System.ComponentModel.DataAnnotations;

namespace BugTrackerBackendAPI.Models 
{ 
    public partial class Attachment
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Location { get; set; }

        [Required]
        public long Size { get; set; }

        [Required]
        public AttachmentType? AttachmentType { get; set; }

        [Required]
        public string? Icon { get; set; }

        [Required]
        public Comment? Comment { get; set; }
    }
}