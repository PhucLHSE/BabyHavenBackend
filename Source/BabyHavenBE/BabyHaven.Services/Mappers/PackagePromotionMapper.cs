using BabyHaven.Common.DTOs.PackagePromotionDTOs;
using BabyHaven.Common.Enum.MembershipPackageEnums;
using BabyHaven.Common.Enum.PackageFeatureEnums;
using BabyHaven.Common.Enum.PromotionEnums;
using BabyHaven.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.Mappers
{
    public static class PackagePromotionMapper
    {
        // Mapper PackagePromotionViewAllDto
        public static PackagePromotionViewAllDto MapToPackagePromotionViewAllDto(this PackagePromotion model)
        {
            return new PackagePromotionViewAllDto
            {
                // Mapping package details
                PackageName = model.Package?.PackageName ?? string.Empty,
                DurationMonths = model.Package?.DurationMonths ?? 0,
                MaxChildrenAllowed = model.Package?.MaxChildrenAllowed ?? 0,
                SupportLevel = model.Package?.SupportLevel ?? string.Empty,

                // Mapping promotion details
                PromotionCode = model.Promotion?.PromotionCode ?? string.Empty,
                DiscountPercent = model.Promotion?.DiscountPercent ?? 0,
                MaxDiscountAmount = model.Promotion?.MaxDiscountAmount,
                TargetAudience = model.Promotion?.TargetAudience ?? string.Empty,
                StartDate = model.Promotion?.StartDate ?? default,
                EndDate = model.Promotion?.EndDate ?? default,

                // Mapping IsActive as a boolean
                IsActive = model.IsActive
            };
        }

        // Mapper PackagePromotionViewDetailsDto
        public static PackagePromotionViewDetailsDto MapToPackagePromotionViewDetailsDto(this PackagePromotion model)
        {
            return new PackagePromotionViewDetailsDto
            {
                // Mapping package details
                PackageName = model.Package?.PackageName ?? string.Empty,
                Description = model.Package?.Description ?? string.Empty,
                Price = model.Package?.Price ?? 0,
                Currency = model.Package?.Currency ?? string.Empty,
                DurationMonths = model.Package?.DurationMonths ?? 0,
                TrialPeriodDays = model.Package?.TrialPeriodDays,
                MaxChildrenAllowed = model.Package?.MaxChildrenAllowed ?? 0,
                SupportLevel = model.Package?.SupportLevel ?? string.Empty,
                PackageStatus = model.Package != null 
                     ? (MembershipPackageStatus)Enum.Parse(
                         typeof(MembershipPackageStatus), model.Package.Status, true) 
                     : MembershipPackageStatus.Inactive,

                // Mapping promotion details
                PromotionCode = model.Promotion?.PromotionCode ?? string.Empty,
                PromotionDescription = model.Promotion?.Description ?? string.Empty,
                DiscountPercent = model.Promotion?.DiscountPercent ?? 0,
                MinPurchaseAmount = model.Promotion?.MinPurchaseAmount,
                MaxDiscountAmount = model.Promotion?.MaxDiscountAmount,
                TargetAudience = model.Promotion?.TargetAudience ?? string.Empty,
                StartDate = model.Promotion?.StartDate ?? default,
                EndDate = model.Promotion?.EndDate ?? default,
                PromotionStatus = model.Promotion != null 
                     ? (PromotionStatus)Enum.Parse(
                         typeof(PromotionStatus), model.Promotion.Status, true) 
                     : PromotionStatus.Inactive,

                // Mapping IsActive as a boolean
                IsActive = model.IsActive,

                // Audit Information
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt
            };
        }

        //Mapper PackagePromotionCreateDto
        public static PackagePromotion MapToPackagePromotionCreateDto(this PackagePromotionCreateDto dto, int packageId, Guid promotionId)
        {
            return new PackagePromotion
            {
                PackageId = packageId,
                PromotionId = promotionId,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow
            };
        }

        //Mapper PackagePromotionUpdateDto
        public static void MapToUpdatedPackagePromotion(this PackagePromotion packagePromotion, PackagePromotionUpdateDto updateDto)
        {
            packagePromotion.IsActive = updateDto.IsActive;
        }

        //Mapper PackagePromotionDeleteDto
        public static PackagePromotionDeleteDto MapToPackagePromotionDeleteDto(this PackagePromotion model)
        {
            return new PackagePromotionDeleteDto
            {
                // Mapping package details
                PackageName = model.Package?.PackageName ?? string.Empty,
                Description = model.Package?.Description ?? string.Empty,
                Price = model.Package?.Price ?? 0,
                Currency = model.Package?.Currency ?? string.Empty,
                DurationMonths = model.Package?.DurationMonths ?? 0,
                TrialPeriodDays = model.Package?.TrialPeriodDays,
                MaxChildrenAllowed = model.Package?.MaxChildrenAllowed ?? 0,
                SupportLevel = model.Package?.SupportLevel ?? string.Empty,

                // Mapping promotion details
                PromotionCode = model.Promotion?.PromotionCode ?? string.Empty,
                PromotionDescription = model.Promotion?.Description ?? string.Empty,
                DiscountPercent = model.Promotion?.DiscountPercent ?? 0,
                MinPurchaseAmount = model.Promotion?.MinPurchaseAmount,
                MaxDiscountAmount = model.Promotion?.MaxDiscountAmount,
                TargetAudience = model.Promotion?.TargetAudience ?? string.Empty,
                StartDate = model.Promotion?.StartDate ?? default,
                EndDate = model.Promotion?.EndDate ?? default,

                // Mapping IsActive as a boolean
                IsActive = model.IsActive,

                // Audit Information
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt
            };
        }
    }
}
