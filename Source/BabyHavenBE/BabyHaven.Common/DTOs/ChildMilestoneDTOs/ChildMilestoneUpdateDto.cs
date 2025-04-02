using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BabyHaven.Common.Enum.ChildMilestoneEnums;
using BabyHaven.Common.Enum.Converters;

namespace BabyHaven.Common.DTOs.ChildMilestoneDTOs
{
    public class ChildMilestoneUpdateDto
    {

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "DateOfBirth is required.")]
        public string DateOfBirth { get; set; }

        [Required(ErrorMessage = "MemberId ID is required.")]
        public Guid MemberId { get; set; }

        public string AchievedDate { get; set; }

        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
        public string Status { get; set; }

        [StringLength(2000, ErrorMessage = "Notes cannot exceed 2000 characters.")]
        public string Notes { get; set; } = string.Empty;

        [StringLength(2000, ErrorMessage = "Guidelines cannot exceed 2000 characters.")]
        public string Guidelines { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Importance cannot exceed 50 characters.")]
        public string Importance { get; set; } = "Medium";

        [StringLength(100, ErrorMessage = "Category cannot exceed 100 characters.")]
        public string Category { get; set; } = string.Empty;
    }
}
