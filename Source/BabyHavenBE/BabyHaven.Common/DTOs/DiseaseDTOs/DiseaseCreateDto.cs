using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BabyHaven.Common.Enum.AlertEnums;

namespace BabyHaven.Common.DTOs.DiseaseDTOs
{
    public class DiseaseCreateDto
    {
        [Required(ErrorMessage = "Disease is required.")]
        [MaxLength(255, ErrorMessage = "Disease cannot exceed 255 characters.")]
        public string DiseaseName { get; set; } = string.Empty;

        [Required(ErrorMessage = "LowerBoundMale is required.")]
        [Range(1, 1000, ErrorMessage = "LowerBoundMale must be between 1 and 1000.")]
        public double LowerBoundMale { get; set; }

        [Required(ErrorMessage = "UpperBoundMale is required.")]
        [Range(1, 1000, ErrorMessage = "UpperBoundMale must be between 1 and 1000.")]
        public double UpperBoundMale { get; set; }

        [Required(ErrorMessage = "LowerBoundFemale is required.")]
        [Range(1, 1000, ErrorMessage = "LowerBoundFemale must be between 1 and 1000.")]
        public double LowerBoundFemale { get; set; }

        [Required(ErrorMessage = "UpperBoundFemale is required.")]
        [Range(1, 1000, ErrorMessage = "UpperBoundFemale must be between 1 and 1000.")]
        public double UpperBoundFemale { get; set; }

        [Required(ErrorMessage = "MinAge is required.")]
        [Range(1, 18, ErrorMessage = "MinAge must be between 1 and 18.")]
        public int MinAge { get; set; }

        [Required(ErrorMessage = "MaxAge is required.")]
        [Range(1, 18, ErrorMessage = "MaxAge must be between 1 and 18.")]
        public int MaxAge { get; set; }

        [Required(ErrorMessage = "Severity is required.")]
        [JsonConverter(typeof(JsonStringEnumConverter))] // Serialize Enum thành string khi trả về API
        public SeverityLevelEnum Severity { get; set; }

        [Required(ErrorMessage = "DiseaseType is required.")]
        public string DiseaseType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Symptoms are required.")]
        [MaxLength(2000, ErrorMessage = "Symptoms cannot exceed 2000 characters.")]
        public string Symptoms { get; set; } = string.Empty;

        [MaxLength(2000, ErrorMessage = "Treatment cannot exceed 2000 characters.")]
        public string? Treatment { get; set; }

        [MaxLength(2000, ErrorMessage = "Prevention cannot exceed 2000 characters.")]
        public string? Prevention { get; set; }

        [MaxLength(2000, ErrorMessage = "Description cannot exceed 2000 characters.")]
        public string? Description { get; set; }

        [MaxLength(2000, ErrorMessage = "Notes cannot exceed 2000 characters.")]
        public string? Notes { get; set; }
    }
}
