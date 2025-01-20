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
        public static MembershipPackageViewAllDto MapToDto(this MembershipPackage model)
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
                PromotionCode = model.Promotion?.PromotionCode,
                DiscountPercent = model.Promotion?.DiscountPercent
            };
        }
    }
}
