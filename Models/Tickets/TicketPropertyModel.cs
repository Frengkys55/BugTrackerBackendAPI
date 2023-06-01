﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BugTrackerBackendAPI.Models
{
    public partial class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid Guid { get; set; }

        [Required]
        public string Project { get; set; }

        [Required]
        internal User User { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime? DateCreated { get; set; }

        [Required]
        public DateTime? DateModified { get; set; }

        [Required]
        public string Severity { get; set; }

        [Required]
        public string Type { get; set; }
    }

}