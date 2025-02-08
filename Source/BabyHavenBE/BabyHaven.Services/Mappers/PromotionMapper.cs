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
    }
}
