using BabyHaven.Common.Enum.PackageFeatureEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.PackageFeatureDTOs
{
    public class PackageFeatureUpdateDto
    {
        // MembershipPackage
        [Required(ErrorMessage = "PackageName is required.")]
        [MaxLength(255, ErrorMessage = "PackageName cannot exceed 255 characters.")]
        public string PackageName { get; set; } = string.Empty;

        // Feature
        [Required(ErrorMessage = "FeatureName is required.")]
        [MaxLength(255, ErrorMessage = "FeatureName cannot exceed 255 characters.")]
        public string FeatureName { get; set; } = string.Empty;

        // PackageFeature Status
        [Required(ErrorMessage = "Status is required.")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PackageFeatureStatus? Status { get; set; }
    }
}
