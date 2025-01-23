using BabyHaven.Common.Enum.FeatureEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.FeatureDTOs
{
    public class FeatureCreateDto
    {
        [Required(ErrorMessage = "FeatureName is required.")]
        [MaxLength(255, ErrorMessage = "FeatureName cannot exceed 255 characters.")]
        public string FeatureName { get; set; } = string.Empty;

        [MaxLength(2000, ErrorMessage = "Description cannot exceed 2000 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Status is required.")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FeatureStatus Status { get; set; } = FeatureStatus.Inactive;
    }
}
