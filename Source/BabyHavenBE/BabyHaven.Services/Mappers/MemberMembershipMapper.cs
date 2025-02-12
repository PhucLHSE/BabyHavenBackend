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
    }
}
