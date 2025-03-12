using BabyHaven.Common.DTOs.MemberMembershipDTOs;
using BabyHaven.Common.Enum.MemberMembershipEnums;
using BabyHaven.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.Mappers
{
    public static class MemberMembershipMapper
    {
        // Mapper MemberMembershipViewAllDto
        public static MemberMembershipViewAllDto MapToMemberMembershipViewAllDto(this MemberMembership model)
        {
            return new MemberMembershipViewAllDto
            {
                MemberMembershipId = model.MemberMembershipId,
                MemberId = model.MemberId,

                MemberName = model.Member?.User?.Name ?? "Unknown",
                PackageName = model.Package?.PackageName ?? "Unknown",
                StartDate = model.StartDate,
                EndDate = model.EndDate,

                // Convert Status from string to enum
                Status = Enum.TryParse<MemberMembershipStatus>(model.Status, true, out var status)
                          ? status
                          : MemberMembershipStatus.Inactive,

                IsActive = model.IsActive
            };
        }

        // Mapper MemberMembershipViewDetailsDto
        public static MemberMembershipViewDetailsDto MapToMemberMembershipViewDetailsDto(this MemberMembership model)
        {
            return new MemberMembershipViewDetailsDto
            {
                MemberName = model.Member?.User?.Name ?? "Unknown",
                PackageName = model.Package?.PackageName ?? "Unknown",
                StartDate = model.StartDate,
                EndDate = model.EndDate,

                // Convert Status from string to enum
                Status = Enum.TryParse<MemberMembershipStatus>(model.Status, true, out var status)
                          ? status
                          : MemberMembershipStatus.Inactive,

                IsActive = model.IsActive,
                Description = model.Description,

                // Audit Information
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt
            };
        }

        //Mapper MemberMembershipCreateDto
        public static MemberMembership MapToMemberMembershipCreateDto(this MemberMembershipCreateDto dto, Member member, MembershipPackage package)
        {
            return new MemberMembership
            {
                Description = package.Description,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddMonths(package.DurationMonths),

                // If the package is free, set active
                Status = package.PackageName.Equals("Free") ? MemberMembershipStatus.Active.ToString() : MemberMembershipStatus.Pending.ToString(),
                IsActive = package.PackageName.Equals("Free") ? true : false,

                PackageId = package.PackageId,
                MemberId = member.MemberId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        // Mapper MemberMembershipUpdateDto
        public static void MapToMemberMembershipUpdateDto(this MemberMembership memberMembership, MemberMembershipUpdateDto updateDto)
        {
            // Update basic properties with checks
            if (updateDto.MemberMembershipId != Guid.Empty)
                memberMembership.MemberMembershipId = updateDto.MemberMembershipId;

            if (updateDto.StartDate != default)
                memberMembership.StartDate = updateDto.StartDate;

            if (updateDto.EndDate != default)
                memberMembership.EndDate = updateDto.EndDate;

            if (updateDto.IsActive != memberMembership.IsActive)
                memberMembership.IsActive = updateDto.IsActive;

            if (!string.IsNullOrWhiteSpace(updateDto.Description))
                memberMembership.Description = updateDto.Description;


            // Validate and convert enum to string
            if (Enum.IsDefined(typeof(MemberMembershipStatus), updateDto.Status))
                memberMembership.Status = updateDto.Status.ToString();
            else
                throw new ArgumentException("Invalid status value.");


            // Validate date range
            if (updateDto.EndDate <= updateDto.StartDate)
            {
                throw new ArgumentException("EndDate must be greater than StartDate.");
            }
        }

        // Mapper MemberMembershipDeleteDto
        public static MemberMembershipDeleteDto MapToMemberMembershipDeleteDto(this MemberMembership model)
        {
            return new MemberMembershipDeleteDto
            {
                MemberName = model.Member?.User?.Name ?? "Unknown",
                PackageName = model.Package?.PackageName ?? "Unknown",
                StartDate = model.StartDate,
                EndDate = model.EndDate,

                // Convert Status from string to enum
                Status = Enum.TryParse<MemberMembershipStatus>(model.Status, true, out var status)
                          ? status
                          : MemberMembershipStatus.Inactive,

                IsActive = model.IsActive,
                Description = model.Description,

                // Audit Information
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt
            };
        }

        ////Mapper MemberMembershipFromTransactionResponse
        //public static void UpdateFromTransactionResponse(this MemberMembership membership)
        //{
        //    membership.Status = MemberMembershipStatus.Active.ToString();
        //    membership.StartDate = DateTime.UtcNow;
        //    membership.EndDate = DateTime.UtcNow.AddMonths(membership.Package.DurationMonths);
        //    membership.IsActive = true;
        //    membership.UpdatedAt = DateTime.UtcNow;
        //}
    }
}
