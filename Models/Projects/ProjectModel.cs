using Newtonsoft.Json;
using NSwag.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;
using System.Runtime.Serialization;

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

        public string? IconUrl { set; get; }
        public string? BackgroundImageUrl { set; get; }

        [Required]
        public DateTime DateCreated { set; get; }

        [Required]
        public DateTime DateModified { set; get; }

        public string? ProjectStatus { set; get; }

        public string? accesstoken { set; get; }

        [Required]
        internal User? User { set; get; }

        ICollection<Ticket>? Tickets { set; get; }
    }
}


