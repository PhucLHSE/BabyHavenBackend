using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.GrowthRecordDTOs
{
    public class GrowthRecordChildDto
    {
        [Range(5, 50, ErrorMessage = "Muscle Mass must be between 5kg and 50kg.")]
        public float? MuscleMass { get; set; }

        [Range(10, 300, ErrorMessage = "Ferritin Level must be between 10 and 300 μg/L.")]
        public float? FerritinLevel { get; set; }

        [Range(50, 150, ErrorMessage = "Triglycerides must be between 50 and 150 mg/dL.")]
        public float? Triglycerides { get; set; }

        [Range(70, 140, ErrorMessage = "Blood Sugar Level must be between 70 and 140 mg/dL.")]
        public float? BloodSugarLevel { get; set; }

        [MaxLength(50, ErrorMessage = "PhysicalActivityLevel cannot exceed 50 characters.")]
        public string? PhysicalActivityLevel { get; set; }

        [Range(50, 120, ErrorMessage = "Heart Rate must be between 50 and 120 bpm.")]
        public int? HeartRate { get; set; }

        [Range(80, 130, ErrorMessage = "Blood Pressure must be between 80 and 130 mmHg.")]
        public float? BloodPressure { get; set; }

        [Range(0, 24, ErrorMessage = "Sleep Duration must be between 0 and 24 hours.")]
        public float? SleepDuration { get; set; }

        [MaxLength(50, ErrorMessage = "Vision cannot exceed 50 characters.")]
        public string? Vision { get; set; }

        [MaxLength(50, ErrorMessage = "Hearing cannot exceed 50 characters.")]
        public string? Hearing { get; set; }

        [Range(40, 90, ErrorMessage = "Chest Circumference must be between 40 and 90 cm.")]
        public double? ChestCircumference { get; set; }

        [MaxLength(2000, ErrorMessage = "ImmunizationStatus cannot exceed 2000 characters.")]
        public string? ImmunizationStatus { get; set; }

        [MaxLength(50, ErrorMessage = "Mental Health Status cannot exceed 50 characters.")]
        public string? MentalHealthStatus { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Growth Hormone Level must be a positive number.")]
        public double? GrowthHormoneLevel { get; set; }

        [MaxLength(50, ErrorMessage = "Attention Span cannot exceed 50 characters.")]
        public string? AttentionSpan { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Body Temperature must be a positive number.")]
        public double? BodyTemperature { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Oxygen Saturation must be a positive number.")]
        public double? OxygenSaturation { get; set; }
    }
}
