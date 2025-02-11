using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.PackagePromotionDTOs
{
    public class PackagePromotionViewAllDto
    {
        // Package Information
        public string PackageName { get; set; } = string.Empty;

        public int DurationMonths { get; set; }

        public int MaxChildrenAllowed { get; set; }

        public string SupportLevel { get; set; } = string.Empty;


        // Promotion Information
        public string PromotionCode { get; set; } = string.Empty;

        public int DiscountPercent { get; set; }

        public decimal? MaxDiscountAmount { get; set; }

        public string TargetAudience { get; set; } = string.Empty;

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }


        // PackagePromotion Status
        public bool IsActive { get; set; }
    }
}
