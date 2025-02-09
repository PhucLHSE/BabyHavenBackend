using BabyHaven.Common.Enum.PromotionEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.PromotionDTOs
{
    public class PromotionDeleteDto
    {
        public string PromotionCode { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int DiscountPercent { get; set; }

        public decimal? MinPurchaseAmount { get; set; }

        public decimal? MaxDiscountAmount { get; set; }

        public string ApplicablePackageIds { get; set; } = string.Empty;

        public string TargetAudience { get; set; } = string.Empty;

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PromotionStatus Status { get; set; } = PromotionStatus.Inactive;

        public int? RedemptionCount { get; set; }

        public int? UsageLimit { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string CreatedBy { get; set; } = string.Empty;

        public string ModifiedBy { get; set; } = string.Empty;
    }
}
