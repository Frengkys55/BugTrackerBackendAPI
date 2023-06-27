using Newtonsoft.Json;
using NSwag.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Drawing;

namespace BugTrackerBackendAPI.Models
{
    public partial class Project
    {
        /// <summary>
        /// Project's Id (not used)
        /// </summary>
        [Key]
        [Required]
        public int Id { set; get; }

        /// <summary>
        /// Project's GUID
        /// </summary>
        [Required]
        public Guid Guid { set; get; }

        /// <summary>
        /// Name of the project
        /// </summary>
        [Required]
        public string? Name { set; get; }

        /// <summary>
        /// Project's description
        /// </summary>
        public string? Description { set; get; }

        /// <summary>
        /// Project's icon image (Use it to store image url when sending to the user and use it to read image data when receiving from user)
        /// </summary>
        public string? IconUrl { set; get; }

        /// <summary>
        /// Project's background image
        /// </summary>
        public string? BackgroundImageUrl { set; get; }

        /// <summary>
        /// When is this project is created (automatically created by SQLServer)
        /// </summary>
        [Required]
        public DateTime DateCreated { set; get; }

        /// <summary>
        /// When is the last time the user modified a project?
        /// </summary>
        [Required]
        public DateTime DateModified { set; get; }


        /// <summary>
        /// Status of a project (not used right now)
        /// </summary>
        public string? ProjectStatus { set; get; }

        /// <summary>
        /// User's access token (used only when modifying a projec)
        /// </summary>
        public string? accesstoken { set; get; }

        [Required]
        internal User? User { set; get; }


        ICollection<Ticket>? Tickets { set; get; }
    }
}


