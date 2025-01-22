using BabyHaven.Common.Enum.FeatureEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.FeatureDTOs
{
    public class FeatureViewDetailsDto
    {
        public int FeatureId { get; set; }

        public string FeatureName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FeatureStatus Status { get; set; } = FeatureStatus.Inactive;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
