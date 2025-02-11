using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BabyHaven.Common.Enum.AlertEnums;

namespace BabyHaven.Common.DTOs.DiseaseDTOs
{
    public class DiseaseViewAllDto
    {
        public string DiseaseName { get; set; } = string.Empty;

        public double LowerBoundMale { get; set; }

        public double UpperBoundMale { get; set; }

        public double LowerBoundFemale { get; set; }

        public double UpperBoundFemale { get; set; }

        public int MinAge { get; set; }

        public int MaxAge { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))] // Chuyển Enum thành string khi trả về API
        public SeverityLevelEnum Severity { get; set; }

        public string DiseaseType { get; set; } = string.Empty; 

        public string Symptoms { get; set; } = string.Empty;

        public string? Treatment { get; set; }

        public string? Prevention { get; set; }

        public string? Description { get; set; }

        public string? Notes { get; set; }
    }
}
