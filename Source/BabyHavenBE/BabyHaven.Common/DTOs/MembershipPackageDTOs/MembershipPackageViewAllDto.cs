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
    public class MembershipPackageViewAllDto
    {
        [Required(ErrorMessage = "PackageId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "PackageId must be greater than 0.")]
        public int PackageId { get; set; }

        [Required(ErrorMessage = "PackageName is required.")]
        [MaxLength(255, ErrorMessage = "PackageName cannot exceed 255 characters.")]
        public string PackageName { get; set; } = string.Empty;

        [MaxLength(2000, ErrorMessage = "Description cannot exceed 2000 characters.")]
        public string Description { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Currency is required.")]
        [MaxLength(10, ErrorMessage = "Currency cannot exceed 10 characters.")]
        public string Currency { get; set; } = string.Empty;

        [Range(1, 120, ErrorMessage = "DurationMonths must be between 1 and 120.")]
        public int DurationMonths { get; set; }

        [Required(ErrorMessage = "IsRecurring is required.")]
        public bool IsRecurring { get; set; }

        [Range(1, 100, ErrorMessage = "MaxChildrenAllowed must be between 1 and 100.")]
        public int MaxChildrenAllowed { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MembershipPackageStatus Status { get; set; } = MembershipPackageStatus.Inactive;

        // Add information from Promotions table
        [MaxLength(50, ErrorMessage = "PromotionCode cannot exceed 50 characters.")]
        public string PromotionCode { get; set; } = string.Empty;

        [Range(0, 100, ErrorMessage = "DiscountPercent must be between 0 and 100.")]
        public decimal? DiscountPercent { get; set; }
    }
}
