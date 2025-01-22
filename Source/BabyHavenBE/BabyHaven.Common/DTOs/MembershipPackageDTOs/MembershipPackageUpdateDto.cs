using BabyHaven.Common.Enum.MembershipPackageEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.MembershipPackageDTOs
{
    public class MembershipPackageUpdateDto
    {
        [Required(ErrorMessage = "PackageId is required.")]
        public int PackageId { get; set; }

        [Required(ErrorMessage = "PackageName is required.")]
        [MaxLength(255, ErrorMessage = "PackageName cannot exceed 255 characters.")]
        public string PackageName { get; set; } = string.Empty;

        [MaxLength(2000, ErrorMessage = "Description cannot exceed 2000 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Currency is required.")]
        [MaxLength(10, ErrorMessage = "Currency cannot exceed 10 characters.")]
        public string Currency { get; set; } = "VND";

        [Required(ErrorMessage = "DurationMonths is required.")]
        [Range(1, 120, ErrorMessage = "DurationMonths must be between 1 and 120.")]
        public int? DurationMonths { get; set; }

        [Range(0, 365, ErrorMessage = "TrialPeriodDays must be between 0 and 365.")]
        public int? TrialPeriodDays { get; set; }

        [Required(ErrorMessage = "MaxChildrenAllowed is required.")]
        [Range(1, 100, ErrorMessage = "MaxChildrenAllowed must be between 1 and 100.")]
        public int? MaxChildrenAllowed { get; set; }

        [MaxLength(255, ErrorMessage = "SupportLevel cannot exceed 255 characters.")]
        public string SupportLevel { get; set; } = string.Empty;

        [Required(ErrorMessage = "Status is required.")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MembershipPackageStatus? Status { get; set; }
    }
}
