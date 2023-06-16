using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BugTrackerBackendAPI.Models
{
    public class UserShort
    {
        [NotNull]
        [Required]
        public string? Username {  get; set; }

        [NotNull]
        [Required]
        public string? Password { get; set; }
    }
}
