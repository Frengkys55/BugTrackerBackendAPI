using System.ComponentModel.DataAnnotations;

namespace BugTrackerBackendAPI.Models
{

    /// <summary>
    /// Shortened project information model that only contains the project name and the project GUID
    /// </summary>
    public class ShortProjectInfo
    {
        [Required]
        public Guid Guid { get; set; }

        [Required]
        public string? Name { get; set; }
    }
}
