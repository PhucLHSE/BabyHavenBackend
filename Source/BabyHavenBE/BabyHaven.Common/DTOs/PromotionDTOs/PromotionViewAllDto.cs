using BabyHaven.Common.Enum.PromotionEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.PromotionDTOs
{
    public class PromotionViewAllDto
    {
        public string PromotionCode { get; set; } = string.Empty;

        public int DiscountPercent { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PromotionStatus Status { get; set; } = PromotionStatus.Inactive;
    }
}
