using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.DiseaseDTOs
{
    public class DiseaseViewDetailsDto
    {
        public int DiseaseId { get; set; }

        public string DiseaseName { get; set; } = string.Empty;

        public double LowerBoundMale { get; set; }

        public double UpperBoundMale { get; set; }

        public double LowerBoundFemale { get; set; }

        public double UpperBoundFemale { get; set; }

        public int MinAge { get; set; }

        public int MaxAge { get; set; }

        public string Severity { get; set; } = string.Empty;

        public string DiseaseType { get; set; } = string.Empty;

        public string Symptoms { get; set; } = string.Empty;

        public string? Treatment { get; set; }

        public string? Prevention { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastModified { get; set; }

        public string? Description { get; set; }

        public string? Notes { get; set; }

        public bool IsActive { get; set; }

    }
}
