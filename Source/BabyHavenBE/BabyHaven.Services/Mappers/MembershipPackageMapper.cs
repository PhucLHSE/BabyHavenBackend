using BabyHaven.Common.DTOs.MembershipPackageDTOs;
using BabyHaven.Common.Enum.MembershipPackageEnums;
using BabyHaven.Repositories.Models;
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
    }
}
