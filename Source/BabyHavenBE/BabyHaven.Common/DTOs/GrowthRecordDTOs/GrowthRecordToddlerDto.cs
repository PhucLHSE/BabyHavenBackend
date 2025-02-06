using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.GrowthRecordDTOs
{
    public class GrowthRecordToddlerDto
    {
        [Required(ErrorMessage = "ChildID is required.")]
        public Guid ChildID { get; set; }

        [Required(ErrorMessage = "RecordedBy is required.")]
        public Guid RecordedBy { get; set; }

        [Required(ErrorMessage = "Weight is required.")]
        [Range(5, 30, ErrorMessage = "Weight must be between 5kg and 30kg.")]
        public float Weight { get; set; }

        [Required(ErrorMessage = "Height is required.")]
        [Range(50, 120, ErrorMessage = "Height must be between 50cm and 120cm.")]
        public float Height { get; set; }

        [Range(40, 80, ErrorMessage = "Chest Circumference must be between 40cm and 80cm.")]
        public float? ChestCircumference { get; set; }

        [MaxLength(50, ErrorMessage = "NutritionalStatus cannot exceed 50 characters.")]
        public string? NutritionalStatus { get; set; }

        [MaxLength(2000, ErrorMessage = "ImmunizationStatus cannot exceed 2000 characters.")]
        public string? ImmunizationStatus { get; set; }

        [MaxLength(255, ErrorMessage = "DevelopmentalMilestones cannot exceed 255 characters.")]
        public string? DevelopmentalMilestones { get; set; }

        [MaxLength(2000, ErrorMessage = "Notes cannot exceed 2000 characters.")]
        public string? Notes { get; set; }
    }
}
