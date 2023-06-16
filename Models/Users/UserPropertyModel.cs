using System.ComponentModel.DataAnnotations;
using BugTrackerBackendAPI.Models;

namespace BugTrackerBackendAPI.Models
{
    public partial class User
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public string? FirstName{ get; set; }

        public string? LastName { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        public string? PhoneNumber { get; set; }



    }
}
