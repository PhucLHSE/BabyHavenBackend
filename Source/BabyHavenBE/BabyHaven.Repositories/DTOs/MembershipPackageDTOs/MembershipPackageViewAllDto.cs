using BabyHaven.Common.Enum.MembershipPackageEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Repositories.DTOs.MembershipPackageDTOs
{
    public class MembershipPackageViewAllDto
    {
        public int PackageId { get; set; }
        public string PackageName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public int DurationMonths { get; set; }
        public bool IsRecurring { get; set; }
        public int MaxChildrenAllowed { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MembershipPackageStatus Status { get; set; }

        // Add information from Promotions table
        public string PromotionCode { get; set; }
        public decimal? DiscountPercent { get; set; }
    }
}
