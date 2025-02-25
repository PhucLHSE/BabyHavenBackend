using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.GrowthRecordDTOs
{
    public class GrowthRecordUpdateDto
    {
        [Required(ErrorMessage = "ID is required.")]
        public int RecordId { get; set; }
        [Required(ErrorMessage = "Child ID is required.")]
        public Guid ChildId { get; set; }
        [Required(ErrorMessage = "Weight is required.")]
        [Range(5, 30, ErrorMessage = "Weight must be between 5kg and 30kg.")]
        public float? Weight { get; set; }

        [Required(ErrorMessage = "Height is required.")]
        [Range(50, 120, ErrorMessage = "Height must be between 50cm and 120cm.")]
        public float? Height { get; set; }

        [Range(0, 100, ErrorMessage = "Head Circumference must be between 0 and 100 cm.")]
        public float? HeadCircumference { get; set; }

        [Range(5, 50, ErrorMessage = "Muscle Mass must be between 5kg and 50kg.")]
        public float? MuscleMass { get; set; }

        [StringLength(50, ErrorMessage = "Nutritional Status cannot exceed 50 characters.")]
        public string NutritionalStatus { get; set; } = string.Empty;

        [Range(10, 300, ErrorMessage = "Ferritin Level must be between 10 and 300 μg/L.")]
        public float? FerritinLevel { get; set; }

        [Range(50, 150, ErrorMessage = "Triglycerides must be between 50 and 150 mg/dL.")]
        public float? Triglycerides { get; set; }

        [Range(70, 140, ErrorMessage = "Blood Sugar Level must be between 70 and 140 mg/dL.")]
        public float? BloodSugarLevel { get; set; }

        [StringLength(50, ErrorMessage = "Physical Activity Level cannot exceed 50 characters.")]
        public string PhysicalActivityLevel { get; set; } = string.Empty;

        [Range(50, 120, ErrorMessage = "Heart Rate must be between 50 and 120 bpm.")]
        public int? HeartRate { get; set; }

        [Range(80, 130, ErrorMessage = "Blood Pressure must be between 80 and 130 mmHg.")]
        public float? BloodPressure { get; set; }

        [Range(30, 45, ErrorMessage = "Body Temperature must be between 30 and 45 °C.")]
        public float? BodyTemperature { get; set; }

        [Range(0, 100, ErrorMessage = "Oxygen Saturation must be between 0 and 100%.")]
        public float? OxygenSaturation { get; set; }

        [Range(0, 24, ErrorMessage = "Sleep Duration must be between 0 and 24 hours.")]
        public float? SleepDuration { get; set; }

        [StringLength(50, ErrorMessage = "Vision cannot exceed 50 characters.")]
        public string Vision { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Hearing cannot exceed 50 characters.")]
        public string Hearing { get; set; } = string.Empty;

        [StringLength(2000, ErrorMessage = "Immunization Status cannot exceed 2000 characters.")]
        public string ImmunizationStatus { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Mental Health Status cannot exceed 50 characters.")]
        public string MentalHealthStatus { get; set; } = string.Empty;

        [Range(0, 100, ErrorMessage = "Growth Hormone Level must be between 0 and 100 ng/mL.")]
        public float? GrowthHormoneLevel { get; set; }

        [StringLength(50, ErrorMessage = "Attention Span cannot exceed 50 characters.")]
        public string AttentionSpan { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "Neurological Reflexes cannot exceed 255 characters.")]
        public string NeurologicalReflexes { get; set; } = string.Empty;
    }
}
