using BabyHaven.Common.DTOs.PackagePromotionDTOs;
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
                PromotionName = model.Promotion?.PromotionCode ?? string.Empty,
                DiscountPercent = model.Promotion?.DiscountPercent ?? 0,
                MaxDiscountAmount = model.Promotion?.MaxDiscountAmount,
                TargetAudience = model.Promotion?.TargetAudience ?? string.Empty,
                StartDate = model.Promotion?.StartDate ?? default,
                EndDate = model.Promotion?.EndDate ?? default,

                // Mapping IsActive as a boolean
                IsActive = model.IsActive
            };
        }
    }
}
