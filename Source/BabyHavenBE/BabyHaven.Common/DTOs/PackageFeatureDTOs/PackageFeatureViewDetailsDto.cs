using BabyHaven.Common.Enum.PackageFeatureEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.PackageFeatureDTOs
{
    public class PackageFeatureViewDetailsDto
    {
        // MembershipPackage details
        public string PackageName { get; set; } = string.Empty;

        public string PackageDescription { get; set; } = string.Empty;

        public decimal PackagePrice { get; set; }

        public string PackageCurrency { get; set; } = string.Empty;

        public int DurationMonths { get; set; }

        public int? TrialPeriodDays { get; set; }

        public int MaxChildrenAllowed { get; set; }

        public string SupportLevel { get; set; } = string.Empty;

        // Feature details
        public string FeatureName { get; set; } = string.Empty;

        public string FeatureDescription { get; set; } = string.Empty;

        // PackageFeature details
        public DateTime CreatedAt { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PackageFeatureStatus Status { get; set; } = PackageFeatureStatus.Inactive;
    }
}
