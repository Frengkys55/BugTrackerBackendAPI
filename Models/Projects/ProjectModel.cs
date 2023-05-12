using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;

namespace BugTrackerBackendAPI.Models
{
    public partial class Project
    {
        [Key]
        [Required]
        public int Id { set; get; }

        [Required]
        public Guid Guid { set; get; }

        [Required]
        public string? Name { set; get; }

        public string? Description { set; get; }

        public string? ProjectIconUrl { set; get; }
        public string? ProjectBackgroundImageUrl { set; get; }

        [Required]
        public DateTime DateCreated { set; get; }

        [Required]
        public DateTime DateModified { set; get; }

        [Required]
        internal User? User { set; get; }

        ICollection<Ticket>? Tickets { set; get; }
    }
}


