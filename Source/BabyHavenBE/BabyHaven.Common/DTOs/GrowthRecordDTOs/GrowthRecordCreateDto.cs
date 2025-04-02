using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.GrowthRecordDTOs
{
    public class GrowthRecordCreateDto
    {
        [Required]
        public string name { get; set; }

        [Required]
        public string DateOfBirth { get; set; }

        [Required]
        public Guid RecordedBy { get; set; }

        [Required]
        [Range(0, 300, ErrorMessage = "Weight must be a positive number.")]
        public double Weight { get; set; }

        [Required]
        [Range(0, 200, ErrorMessage = "Height must be a positive number.")]
        public double Height { get; set; }

        public double? HeadCircumference { get; set; }

        public double? MuscleMass { get; set; }

        public double? ChestCircumference { get; set; }

        public string NutritionalStatus { get; set; } = string.Empty;

        public double? FerritinLevel { get; set; }

        public double? Triglycerides { get; set; }

        public double? BloodSugarLevel { get; set; }

        public string PhysicalActivityLevel { get; set; } = string.Empty;

        public int? HeartRate { get; set; }

        public double? BloodPressure { get; set; }

        public double? BodyTemperature { get; set; }

        public double? OxygenSaturation { get; set; }

        public double? SleepDuration { get; set; }

        [StringLength(50, ErrorMessage = "Vision cannot be longer than 50 characters.")]
        public string Vision { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Hearing cannot be longer than 50 characters.")]
        public string Hearing { get; set; } = string.Empty;

        [StringLength(2000, ErrorMessage = "Immunization Status cannot be longer than 2000 characters.")]
        public string ImmunizationStatus { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Mental Health Status cannot be longer than 50 characters.")]
        public string MentalHealthStatus { get; set; } = string.Empty;

        public double? GrowthHormoneLevel { get; set; }

        [StringLength(50, ErrorMessage = "Attention Span cannot be longer than 50 characters.")]
        public string AttentionSpan { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "Neurological Reflexes cannot be longer than 255 characters.")]
        public string NeurologicalReflexes { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "Developmental Milestones cannot be longer than 255 characters.")]
        public string DevelopmentalMilestones { get; set; } = string.Empty;

        [StringLength(2000, ErrorMessage = "Notes cannot be longer than 2000 characters.")]
        public string Notes { get; set; } = string.Empty;

        [Required]
        public string CreatedAt { get; set; }
    }
}
