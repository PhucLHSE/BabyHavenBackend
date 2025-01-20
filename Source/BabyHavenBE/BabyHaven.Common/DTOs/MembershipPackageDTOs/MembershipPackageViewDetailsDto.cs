using BabyHaven.Common.Enum.MembershipPackageEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.MembershipPackageDTOs
{
    public class MembershipPackageViewDetailsDto
    {
        public int PackageId { get; set; }
        public string PackageName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Currency { get; set; } = string.Empty;
        public int DurationMonths { get; set; }
        public bool IsRecurring { get; set; }
        public int? TrialPeriodDays { get; set; }
        public int MaxChildrenAllowed { get; set; }
        public string SupportLevel { get; set; } = string.Empty;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MembershipPackageStatus Status { get; set; } = MembershipPackageStatus.Inactive;

        // Promotion details
        public string PromotionCode { get; set; } = string.Empty;
        public decimal? DiscountPercent { get; set; }
        public DateOnly? PromotionStartDate { get; set; }
        public DateOnly? PromotionEndDate { get; set; }
        public decimal? MinPurchaseAmount { get; set; }
        public decimal? MaxDiscountAmount { get; set; }

        // Created and updated timestamps
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Additional details
        public List<string> Features { get; set; } = new List<string>();
    }
}
