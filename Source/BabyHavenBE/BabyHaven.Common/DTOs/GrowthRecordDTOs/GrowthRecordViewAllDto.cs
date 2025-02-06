using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BabyHaven.Common.Enum.GrowthRecordEnum;

namespace BabyHaven.Common.DTOs.GrowthRecordDTOs
{
    public class GrowthRecordViewAllDto
    {
        public double Weight { get; set; }

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

        public string Vision { get; set; } = string.Empty;

        public string Hearing { get; set; } = string.Empty;

        public string ImmunizationStatus { get; set; } = string.Empty;

        public string MentalHealthStatus { get; set; } = string.Empty;

        public double? GrowthHormoneLevel { get; set; }

        public string AttentionSpan { get; set; } = string.Empty;

        public string NeurologicalReflexes { get; set; } = string.Empty;

        public string DevelopmentalMilestones { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;
    }
}
