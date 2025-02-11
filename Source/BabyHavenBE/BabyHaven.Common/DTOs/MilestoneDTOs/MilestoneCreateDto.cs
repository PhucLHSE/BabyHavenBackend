using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.MilestoneDTOS
{
    public class MilestoneCreateDto
    {
        [Required(ErrorMessage = "Milestone name is required.")]
        [StringLength(255, ErrorMessage = "Milestone name cannot exceed 255 characters.")]
        public string MilestoneName { get; set; } = string.Empty;

        [StringLength(2000, ErrorMessage = "Description cannot exceed 2000 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Importance is required.")]
        [StringLength(50, ErrorMessage = "Importance cannot exceed 50 characters.")]
        public string Importance { get; set; } = "Medium";

        [StringLength(100, ErrorMessage = "Category cannot exceed 100 characters.")]
        public string Category { get; set; } = string.Empty;

        [Range(0, 216, ErrorMessage = "MinAge must be between 0 and 216 months.")]
        public int? MinAge { get; set; }

        [Range(0, 216, ErrorMessage = "MaxAge must be between 0 and 216 months.")]
        public int? MaxAge { get; set; }

        public bool IsPersonal { get; set; } = false;
    }
}
