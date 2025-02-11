using BabyHaven.Common.DTOs.MemberDTOs;
using BabyHaven.Common.Enum.MemberEnums;
using BabyHaven.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.Mappers
{
    public static class MemberMapper
    {
        // Mapper MemberViewAllDto
        public static MemberViewAllDto MapToMemberViewAllDto(this Member model)
        {
            return new MemberViewAllDto
            {
                MemberName = model.User?.Name ?? "Unknown",
                EmergencyContact = model.EmergencyContact,

                // Convert Status from string to enum
                Status = Enum.TryParse<MemberStatus>(model.Status, true, out var status)
                          ? status
                          : MemberStatus.Inactive,
            };
        }
    }
}
