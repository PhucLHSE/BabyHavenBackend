﻿using BabyHaven.Common.Enum.MembershipPackageEnums;
using BabyHaven.Common.Enum.PromotionEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.VNPayDTOS.PackagePromotionDTOs
{
    public class PackagePromotionDeleteDto
    {
        // Package Information
        public string PackageName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string Currency { get; set; } = string.Empty;

        public int DurationMonths { get; set; }

        public int? TrialPeriodDays { get; set; }

        public int MaxChildrenAllowed { get; set; }

        public string SupportLevel { get; set; } = string.Empty;


        // Promotion Information
        public string PromotionCode { get; set; } = string.Empty;

        public string PromotionDescription { get; set; } = string.Empty;

        public int DiscountPercent { get; set; }

        public decimal? MinPurchaseAmount { get; set; }

        public decimal? MaxDiscountAmount { get; set; }

        public string TargetAudience { get; set; } = string.Empty;

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }


        // PackagePromotion Details
        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
