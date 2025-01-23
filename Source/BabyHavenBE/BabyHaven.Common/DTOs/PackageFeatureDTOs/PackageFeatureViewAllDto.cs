using BabyHaven.Common.Enum.PackageFeatureEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.PackageFeatureDTOs
{
    public class PackageFeatureViewAllDto
    {
        // MembershipPackage
        public string PackageName { get; set; } = string.Empty;

        // Feature
        public string FeatureName { get; set; } = string.Empty;

        // PackageFeature Status
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PackageFeatureStatus Status { get; set; } = PackageFeatureStatus.Inactive;
    }
}
