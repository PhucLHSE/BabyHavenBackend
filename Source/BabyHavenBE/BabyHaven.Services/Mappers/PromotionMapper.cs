using BabyHaven.Common.DTOs.MembershipPackageDTOs;
using BabyHaven.Common.DTOs.PromotionDTOs;
using BabyHaven.Common.Enum.MembershipPackageEnums;
using BabyHaven.Common.Enum.PromotionEnums;
using BabyHaven.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.Mappers
{
    public static class PromotionMapper
    {
        // Mapper PromotionViewAllDto
        public static PromotionViewAllDto MapToPromotionViewAllDto(this Promotion model)
        {
            return new PromotionViewAllDto
            {
                // Promotion
                PromotionCode = model.PromotionCode,
                DiscountPercent = model.DiscountPercent,
                StartDate = model.StartDate,
                EndDate = model.EndDate,

                // Convert Status from string to enum
                Status = Enum.TryParse<PromotionStatus>(model.Status, true, out var status)
                          ? status
                          : PromotionStatus.Inactive
            };
        }

        // Mapper PromotionViewDetailsDto
        public static PromotionViewDetailsDto MapToPromotionViewDetailsDto(this Promotion model)
        {
            return new PromotionViewDetailsDto
            {
                // Basic Promotion Information
                PromotionCode = model.PromotionCode,
                Description = model.Description,
                DiscountPercent = model.DiscountPercent,

                // Purchase Amount Constraints
                MinPurchaseAmount = model.MinPurchaseAmount,
                MaxDiscountAmount = model.MaxDiscountAmount,

                // Applicability and Target Audience
                ApplicablePackageIds = model.ApplicablePackageIds,
                TargetAudience = model.TargetAudience,

                // Promotion Duration
                StartDate = model.StartDate,
                EndDate = model.EndDate,

                // Convert Status from string to enum
                Status = Enum.TryParse<PromotionStatus>(model.Status, true, out var status)
                          ? status
                          : PromotionStatus.Inactive,

                // Redemption and Usage Limits
                RedemptionCount = model.RedemptionCount,
                UsageLimit = model.UsageLimit,

                // Audit Information
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,

                // Fetch User Names instead of IDs
                CreatedBy = model.CreatedByNavigation?.Name ?? "Unknown",
                ModifiedBy = model.ModifiedByNavigation?.Name ?? "Unknown"
            };
        }
    }
}
