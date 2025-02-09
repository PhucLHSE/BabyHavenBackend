using BabyHaven.Common.DTOs.MembershipPackageDTOs;
using BabyHaven.Common.DTOs.PromotionDTOs;
using BabyHaven.Common.Enum.MembershipPackageEnums;
using BabyHaven.Common.Enum.PromotionEnums;
using BabyHaven.Repositories.Models;
using BabyHaven.Repositories.Repositories;
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

        // Mapper PromotionCreateDto
        public static Promotion MapToPromotionCreateDto(this PromotionCreateDto dto)
        {
            return new Promotion
            {
                PromotionCode = dto.PromotionCode,
                Description = dto.Description,
                DiscountPercent = dto.DiscountPercent,
                MinPurchaseAmount = dto.MinPurchaseAmount,
                MaxDiscountAmount = dto.MaxDiscountAmount,
                ApplicablePackageIds = dto.ApplicablePackageIds,
                TargetAudience = dto.TargetAudience,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Status = dto.Status.ToString(), // Convert enum to string for storage
                RedemptionCount = dto.RedemptionCount ?? 0, // Default to 0 if null
                UsageLimit = dto.UsageLimit,
                CreatedBy = dto.CreatedBy,
                ModifiedBy = dto.ModifiedBy
            };
        }

        // Mapper PromotionUpdateDto
        public static void MapToPromotionUpdateDto(this PromotionUpdateDto updateDto, Promotion promotion)
        {
            if (!string.IsNullOrWhiteSpace(updateDto.PromotionCode))
                promotion.PromotionCode = updateDto.PromotionCode;

            if (!string.IsNullOrWhiteSpace(updateDto.Description))
                promotion.Description = updateDto.Description;

            if (updateDto.DiscountPercent >= 0 && updateDto.DiscountPercent <= 100)
                promotion.DiscountPercent = updateDto.DiscountPercent;

            if (updateDto.MinPurchaseAmount.HasValue)
                promotion.MinPurchaseAmount = updateDto.MinPurchaseAmount;

            if (updateDto.MaxDiscountAmount.HasValue)
                promotion.MaxDiscountAmount = updateDto.MaxDiscountAmount;

            if (!string.IsNullOrWhiteSpace(updateDto.ApplicablePackageIds))
                promotion.ApplicablePackageIds = updateDto.ApplicablePackageIds;

            if (!string.IsNullOrWhiteSpace(updateDto.TargetAudience))
                promotion.TargetAudience = updateDto.TargetAudience;

            if (updateDto.EndDate <= updateDto.StartDate)
                throw new ArgumentException("EndDate must be greater than StartDate.");

            promotion.StartDate = updateDto.StartDate;
            promotion.EndDate = updateDto.EndDate;

            if (updateDto.Status.HasValue)
                promotion.Status = updateDto.Status.ToString();

            if (updateDto.RedemptionCount.HasValue)
                promotion.RedemptionCount = updateDto.RedemptionCount;

            if (updateDto.UsageLimit.HasValue)
                promotion.UsageLimit = updateDto.UsageLimit;

            promotion.ModifiedBy = updateDto.ModifiedBy;
        }
    }
}
