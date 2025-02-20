using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.GrowthRecordDTOs
{
    public class GrowthRecordTeenagerDto
    {
        [Required(ErrorMessage = "ChildId is required.")]
        public Guid ChildId { get; set; }

        [Required(ErrorMessage = "RecordedBy is required.")]
        public Guid RecordedBy { get; set; }

        [Required(ErrorMessage = "Weight is required.")]
        [Range(30, 120, ErrorMessage = "Weight must be between 30 and 120 kg.")]
        public float Weight { get; set; }

        [Required(ErrorMessage = "Height is required.")]
        [Range(50, 200, ErrorMessage = "Height must be between 50 and 200 cm.")]
        public float Height { get; set; }

        [Range(40, 100, ErrorMessage = "Chest Circumference must be between 40 and 100 cm.")]
        public float? ChestCircumference { get; set; }

        [Range(0, 80, ErrorMessage = "Muscle Mass must be between 0 and 80 kg.")]
        public float? MuscleMass { get; set; }

        [Range(50, 200, ErrorMessage = "Blood Sugar Level must be between 50 and 200 mg/dL.")]
        public float? BloodSugarLevel { get; set; }

        [Range(50, 200, ErrorMessage = "Triglycerides must be between 50 and 200 mg/dL.")]
        public float? Triglycerides { get; set; }

        [Range(0, 10, ErrorMessage = "Growth Hormone Level must be between 0 and 10 ng/mL.")]
        public float? GrowthHormoneLevel { get; set; }

        [MaxLength(50, ErrorMessage = "MentalHealthStatus cannot exceed 50 characters.")]
        public string? MentalHealthStatus { get; set; }

        [MaxLength(255, ErrorMessage = "NeurologicalReflexes cannot exceed 255 characters.")]
        public string? NeurologicalReflexes { get; set; }

        [MaxLength(255, ErrorMessage = "DevelopmentalMilestones cannot exceed 255 characters.")]
        public string? DevelopmentalMilestones { get; set; }

        [MaxLength(2000, ErrorMessage = "Notes cannot exceed 2000 characters.")]
        public string? Notes { get; set; }

        [MaxLength(2000, ErrorMessage = "Vision cannot exceed 2000 characters.")]
        public string? Vision { get; set; }
    }
}
