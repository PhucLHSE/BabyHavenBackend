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
                MemberId = model.MemberId,
                UserId = model.UserId,
                MemberName = model.User?.Name ?? "Unknown",
                EmergencyContact = model.EmergencyContact,

                // Convert Status from string to enum
                Status = Enum.TryParse<MemberStatus>(model.Status, true, out var status)
                          ? status
                          : MemberStatus.Inactive,
                JoinDate = model.JoinDate,

                Notes = model.Notes
            };
        }

        // Mapper MemberViewDetailsDto
        public static MemberViewDetailsDto MapToMemberViewDetailsDto(this Member model)
        {
            return new MemberViewDetailsDto
            {
                MemberName = model.User?.Name ?? "Unknown",
                EmergencyContact = model.EmergencyContact,

                // Convert Status from string to enum
                Status = Enum.TryParse<MemberStatus>(model.Status, true, out var status)
                          ? status
                          : MemberStatus.Inactive,

                JoinDate = model.JoinDate,
                LeaveDate = model.LeaveDate,
                Notes = model.Notes
            };
        }

        public static MemberAPIResponseDto MapToMemberAPIResonseDto(this Member model)
        {
            return new MemberAPIResponseDto
            {
                MemberId = model.MemberId,
                MemberName = model.User?.Name ?? "Unknown",
                EmergencyContact = model.EmergencyContact,

                // Convert Status from string to enum
                Status = Enum.TryParse<MemberStatus>(model.Status, true, out var status)
                          ? status
                          : MemberStatus.Inactive,

                JoinDate = model.JoinDate,
                LeaveDate = model.LeaveDate,
                Notes = model.Notes
            };
        }

        // Mapper MemberUpdateDto
        public static void MapToMemberUpdateDto(this MemberUpdateDto updateDto, Member member)
        {
            // Do not update UserId to prevent changing the foreign key
            //if (updateDto.UserId != Guid.Empty)
            //    member.UserId = updateDto.UserId;

            if (!string.IsNullOrWhiteSpace(updateDto.EmergencyContact))
                member.EmergencyContact = updateDto.EmergencyContact;

            if (Enum.IsDefined(typeof(MemberStatus), updateDto.Status))
                member.Status = updateDto.Status.ToString();
            else
                throw new ArgumentException("Invalid status value.");

            // Do not update JoinDate to preserve the original membership start date
            //if (updateDto.JoinDate != default)
            //    member.JoinDate = updateDto.JoinDate;

            if (updateDto.LeaveDate.HasValue)
                member.LeaveDate = updateDto.LeaveDate;

            if (!string.IsNullOrWhiteSpace(updateDto.Notes))
                member.Notes = updateDto.Notes;
        }

        // Mapper MemberDeleteDto
        public static MemberDeleteDto MapToMemberDeleteDto(this Member model)
        {
            return new MemberDeleteDto
            {
                MemberName = model.User?.Name ?? "Unknown",
                EmergencyContact = model.EmergencyContact,

                // Convert Status from string to enum
                Status = Enum.TryParse<MemberStatus>(model.Status, true, out var status)
                          ? status
                          : MemberStatus.Inactive,

                JoinDate = model.JoinDate,
                LeaveDate = model.LeaveDate,
                Notes = model.Notes
            };
        }

        // Mapper MemberCreateDto
        public static Member MapToMemberCreateDto(this MemberCreateDto createDto)
        {
            return new Member
            {
                MemberId = Guid.NewGuid(),
                UserId = createDto.UserId,
                EmergencyContact = createDto.EmergencyContact,
                Status = createDto.Status.ToString(),
                JoinDate = createDto.JoinDate,
                LeaveDate = createDto.LeaveDate,
                Notes = createDto.Notes
            };
        }
    }
}
