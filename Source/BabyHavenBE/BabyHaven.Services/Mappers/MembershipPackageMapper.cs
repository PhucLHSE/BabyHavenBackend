using BabyHaven.Common.DTOs.MembershipPackageDTOs;
using BabyHaven.Common.Enum.MembershipPackageEnums;
using BabyHaven.Repositories.DBContext;
using BabyHaven.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Repositories.Mappers
{
    public static class MembershipPackageMapper
    {
        public static MembershipPackageViewAllDto MapToMembershipPackageViewAllDto(this MembershipPackage model)
        {
            return new MembershipPackageViewAllDto
            {
                PackageId = model.PackageId,
                PackageName = model.PackageName,
                Description = model.Description,
                Price = model.Price,
                Currency = model.Currency,
                DurationMonths = model.DurationMonths,
                IsRecurring = model.IsRecurring,
                MaxChildrenAllowed = model.MaxChildrenAllowed,

                Status = Enum.TryParse<MembershipPackageStatus>(model.Status, true, out var status)
                          ? status
                          : MembershipPackageStatus.Inactive,

                // Map promotion
                PromotionCode = model.Promotion?.PromotionCode,
                DiscountPercent = model.Promotion?.DiscountPercent
            };
        }

        public static MembershipPackageViewDetailsDto MapToMembershipPackageViewDetailsDto(this MembershipPackage model)
        {
            return new MembershipPackageViewDetailsDto
            {
                PackageId = model.PackageId,
                PackageName = model.PackageName,
                Description = model.Description,
                Price = model.Price,
                Currency = model.Currency,
                DurationMonths = model.DurationMonths,
                IsRecurring = model.IsRecurring,
                TrialPeriodDays = model.TrialPeriodDays,
                MaxChildrenAllowed = model.MaxChildrenAllowed,
                SupportLevel = model.SupportLevel,

                Status = Enum.TryParse<MembershipPackageStatus>(model.Status, true, out var status)
                          ? status
                          : MembershipPackageStatus.Inactive,

                // Map promotion details
                PromotionCode = model.Promotion?.PromotionCode ?? string.Empty,
                DiscountPercent = model.Promotion?.DiscountPercent,
                PromotionStartDate = model.Promotion?.StartDate,
                PromotionEndDate = model.Promotion?.EndDate,
                MinPurchaseAmount = model.Promotion?.MinPurchaseAmount,
                MaxDiscountAmount = model.Promotion?.MaxDiscountAmount,

                // Timestamps
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,

                //Map features
                Features = model.PackageFeatures?
                               .Where(pf => pf.Feature?.Status == "Active")
                               .Select(pf => pf.Feature.FeatureName)
                               .ToList() ?? new List<string>()
            };
        }

        public static async Task<MembershipPackage> MapToMembershipPackageCreateDtoAsync(this MembershipPackageCreateDto dto, SWP391_ChildGrowthTrackingSystemContext dbContext)
        {
            // Step 1: Create membership package
            var membershipPackage = new MembershipPackage
            {
                PackageName = dto.PackageName,
                Description = dto.Description,
                Price = dto.Price,
                Currency = dto.Currency,
                DurationMonths = dto.DurationMonths,
                IsRecurring = dto.IsRecurring,
                TrialPeriodDays = dto.TrialPeriodDays,
                MaxChildrenAllowed = dto.MaxChildrenAllowed,
                SupportLevel = dto.SupportLevel,
                Status = dto.Status.ToString()
            };

            // Step 2: Add features to membership package
            membershipPackage.PackageFeatures = dto.FeatureNames?.Select(name => new PackageFeature
            {
                Feature = new Feature
                {
                    FeatureName = name,
                    Status = "Active"
                },
                CreatedAt = DateTime.Now,
                Status = "Active"
            }).ToList() ?? new List<PackageFeature>();

            // Step 3: Promotion processing
            if (!string.IsNullOrEmpty(dto.PromotionCode))
            {
                var promotion = await dbContext.Promotions
                                               .FirstOrDefaultAsync(p => p.PromotionCode == dto.PromotionCode);

                if (promotion == null)
                {
                    throw new InvalidOperationException("Promotion not found.");
                }

                membershipPackage.PromotionId = promotion?.PromotionId;
            }

            return membershipPackage;
        }
    }
}
