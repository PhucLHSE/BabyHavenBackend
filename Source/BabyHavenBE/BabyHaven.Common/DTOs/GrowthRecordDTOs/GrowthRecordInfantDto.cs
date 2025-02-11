using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.GrowthRecordDTOs
{
    public class GrowthRecordInfantDto
    {
        [Required(ErrorMessage = "ChildID is required.")]
        public Guid ChildID { get; set; }

        [Required(ErrorMessage = "RecordedBy is required.")]
        public Guid RecordedBy { get; set; }

        [Required(ErrorMessage = "Weight is required.")]
        [Range(0.5, 20, ErrorMessage = "Weight must be between 0.5kg and 20kg.")]
        public float Weight { get; set; }

        [Required(ErrorMessage = "Height is required.")]
        [Range(20, 100, ErrorMessage = "Height must be between 20cm and 100cm.")]
        public float Height { get; set; }

        [Range(30, 60, ErrorMessage = "Head Circumference must be between 30cm and 60cm.")]
        public float? HeadCircumference { get; set; }

        [MaxLength(2000, ErrorMessage = "ImmunizationStatus cannot exceed 2000 characters.")]
        public string? ImmunizationStatus { get; set; }

        [MaxLength(255, ErrorMessage = "DevelopmentalMilestones cannot exceed 255 characters.")]
        public string? DevelopmentalMilestones { get; set; }

        [MaxLength(2000, ErrorMessage = "Notes cannot exceed 2000 characters.")]
        public string? Notes { get; set; }
    }
}
