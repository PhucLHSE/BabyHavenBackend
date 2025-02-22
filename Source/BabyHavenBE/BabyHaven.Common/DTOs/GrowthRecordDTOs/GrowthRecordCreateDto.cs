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
        [Range(0, double.MaxValue, ErrorMessage = "Weight must be a positive number.")]
        public double Weight { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Height must be a positive number.")]
        public double Height { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Head Circumference must be a positive number.")]
        public double? HeadCircumference { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Muscle Mass must be a positive number.")]
        public double? MuscleMass { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Chest Circumference must be a positive number.")]
        public double? ChestCircumference { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Nutritional Status cannot be longer than 50 characters.")]
        public string NutritionalStatus { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Ferritin Level must be a positive number.")]
        public double? FerritinLevel { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Triglycerides must be a positive number.")]
        public double? Triglycerides { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Blood Sugar Level must be a positive number.")]
        public double? BloodSugarLevel { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Physical Activity Level cannot be longer than 50 characters.")]
        public string PhysicalActivityLevel { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Heart Rate must be a positive number.")]
        public int? HeartRate { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Blood Pressure must be a positive number.")]
        public double? BloodPressure { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Body Temperature must be a positive number.")]
        public double? BodyTemperature { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Oxygen Saturation must be a positive number.")]
        public double? OxygenSaturation { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Sleep Duration must be a positive number.")]
        public double? SleepDuration { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Vision cannot be longer than 50 characters.")]
        public string Vision { get; set; } = string.Empty;

        [Required]
        [StringLength(50, ErrorMessage = "Hearing cannot be longer than 50 characters.")]
        public string Hearing { get; set; } = string.Empty;

        [Required]
        [StringLength(2000, ErrorMessage = "Immunization Status cannot be longer than 2000 characters.")]
        public string ImmunizationStatus { get; set; } = string.Empty;

        [Required]
        [StringLength(50, ErrorMessage = "Mental Health Status cannot be longer than 50 characters.")]
        public string MentalHealthStatus { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Growth Hormone Level must be a positive number.")]
        public double? GrowthHormoneLevel { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Attention Span cannot be longer than 50 characters.")]
        public string AttentionSpan { get; set; } = string.Empty;

        [Required]
        [StringLength(255, ErrorMessage = "Neurological Reflexes cannot be longer than 255 characters.")]
        public string NeurologicalReflexes { get; set; } = string.Empty;

        [Required]
        [StringLength(255, ErrorMessage = "Developmental Milestones cannot be longer than 255 characters.")]
        public string DevelopmentalMilestones { get; set; } = string.Empty;

        [StringLength(2000, ErrorMessage = "Notes cannot be longer than 2000 characters.")]
        public string Notes { get; set; } = string.Empty;
    }
}
