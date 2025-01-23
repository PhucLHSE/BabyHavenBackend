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
        // Mapper MembershipPackageViewAllDto
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

                // Convert Status from string to enum
                Status = Enum.TryParse<MembershipPackageStatus>(model.Status, true, out var status)
                          ? status
                          : MembershipPackageStatus.Inactive
            };
        }

        // Mapper MembershipPackageViewDetailsDto
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

                // Convert Status from string to enum
                Status = Enum.TryParse<MembershipPackageStatus>(model.Status, true, out var status)
                          ? status
                          : MembershipPackageStatus.Inactive,

                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt
            };
        }

        // Mapper MembershipPackageCreateDto
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

        // Mapper MembershipPackageUpdateDto
        public static void MapToMembershipPackageUpdateDto(this MembershipPackageUpdateDto updateDto, MembershipPackage package)
        {
            if (!string.IsNullOrWhiteSpace(updateDto.PackageName))
                package.PackageName = updateDto.PackageName;

            if (!string.IsNullOrWhiteSpace(updateDto.Description))
                package.Description = updateDto.Description;

            if (updateDto.Price.HasValue)
                package.Price = updateDto.Price.Value;

            if (!string.IsNullOrWhiteSpace(updateDto.Currency))
                package.Currency = updateDto.Currency;

            if (updateDto.DurationMonths.HasValue)
                package.DurationMonths = updateDto.DurationMonths.Value;

            if (updateDto.TrialPeriodDays.HasValue)
                package.TrialPeriodDays = updateDto.TrialPeriodDays;

            if (updateDto.MaxChildrenAllowed.HasValue)
                package.MaxChildrenAllowed = updateDto.MaxChildrenAllowed.Value;

            if (!string.IsNullOrWhiteSpace(updateDto.SupportLevel))
                package.SupportLevel = updateDto.SupportLevel;

            if (updateDto.Status.HasValue)
                package.Status = updateDto.Status.ToString();
        }

        // Mapper MembershipPackageDeleteDto
        public static MembershipPackageDeleteDto MapToMembershipPackageDeleteDto(this MembershipPackage model)
        {
            return new MembershipPackageDeleteDto
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

                // Convert Status from string to enum
                Status = Enum.TryParse<MembershipPackageStatus>(model.Status, true, out var status)
                          ? status
                          : MembershipPackageStatus.Inactive,

                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt
            };
        }
    }
}
