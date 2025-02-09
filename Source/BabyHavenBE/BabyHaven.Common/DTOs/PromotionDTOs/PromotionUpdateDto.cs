using BabyHaven.Common.Enum.PromotionEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.PromotionDTOs
{
    public class PromotionUpdateDto
    {
        [Required(ErrorMessage = "PromotionId is required.")]
        public Guid PromotionId { get; set; }

        [Required(ErrorMessage = "PromotionCode is required.")]
        [MaxLength(50, ErrorMessage = "PromotionCode cannot exceed 50 characters.")]
        public string PromotionCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(2000, ErrorMessage = "Description cannot exceed 2000 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "DiscountPercent is required.")]
        [Range(0, 100, ErrorMessage = "Discount percent must be between 0 and 100.")]
        public int DiscountPercent { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Minimum purchase amount must be a positive value.")]
        public decimal? MinPurchaseAmount { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Maximum discount amount must be a positive value.")]
        public decimal? MaxDiscountAmount { get; set; }

        [MaxLength(100, ErrorMessage = "ApplicablePackageIds cannot exceed 100 characters.")]
        public string ApplicablePackageIds { get; set; } = string.Empty;

        [MaxLength(100, ErrorMessage = "TargetAudience cannot exceed 100 characters.")]
        public string TargetAudience { get; set; } = string.Empty;

        [Required(ErrorMessage = "StartDate is required.")]
        public DateOnly StartDate { get; set; }

        [Required(ErrorMessage = "EndDate is required.")]
        public DateOnly EndDate { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PromotionStatus? Status { get; set; } = PromotionStatus.Inactive;

        [Range(0, int.MaxValue, ErrorMessage = "Redemption count must be a non-negative value.")]
        public int? RedemptionCount { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Usage limit must be a non-negative value.")]
        public int? UsageLimit { get; set; }

        [Required(ErrorMessage = "ModifiedBy (User ID) is required.")]
        public Guid ModifiedBy { get; set; }
    }
}
