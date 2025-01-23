using BabyHaven.Common.Enum.FeatureEnums;
using BabyHaven.Common.Enum.MembershipPackageEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.FeatureDTOs
{
    public class FeatureViewAllDto
    {
        public string FeatureName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FeatureStatus Status { get; set; } = FeatureStatus.Inactive;
    }
}
