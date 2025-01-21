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
        // Mapper cho MembershipPackageViewAllDto
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
                MaxChildrenAllowed = model.MaxChildrenAllowed,

                // Chuyển đổi Status từ string sang enum
                Status = Enum.TryParse<MembershipPackageStatus>(model.Status, true, out var status)
                          ? status
                          : MembershipPackageStatus.Inactive
            };
        }

        // Mapper cho MembershipPackageViewDetailsDto
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
                TrialPeriodDays = model.TrialPeriodDays,
                MaxChildrenAllowed = model.MaxChildrenAllowed,
                SupportLevel = model.SupportLevel,

                // Chuyển đổi Status từ string sang enum
                Status = Enum.TryParse<MembershipPackageStatus>(model.Status, true, out var status)
                          ? status
                          : MembershipPackageStatus.Inactive,

                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt
            };
        }

        public static MembershipPackage MapToMembershipPackageCreateDto(this MembershipPackageCreateDto dto)
        {
            return new MembershipPackage
            {
                PackageName = dto.PackageName,
                Description = dto.Description,
                Price = dto.Price,
                Currency = dto.Currency,
                DurationMonths = dto.DurationMonths,
                TrialPeriodDays = dto.TrialPeriodDays,
                MaxChildrenAllowed = dto.MaxChildrenAllowed,
                SupportLevel = dto.SupportLevel,
                Status = dto.Status.ToString()
            };
        }
    }
}
