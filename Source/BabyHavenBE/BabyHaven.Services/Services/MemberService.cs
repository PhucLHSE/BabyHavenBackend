using BabyHaven.Common;
using BabyHaven.Repositories;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common.DTOs.MemberDTOs;
using BabyHaven.Services.Mappers;
using BabyHaven.Common.DTOs.PromotionDTOs;
using BabyHaven.Common.DTOs.TransactionDTOs;
using BabyHaven.Repositories.Models;

namespace BabyHaven.Services.Services
{
    public class MemberService : IMemberService
    {
        private readonly UnitOfWork _unitOfWork;

        public MemberService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork
                ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<IServiceResult> GetAll()
        {

            var members = await _unitOfWork.MemberRepository
                .GetAllMemberAsync();

            if (members == null || !members.Any())
            {

                return new ServiceResult(Const.WARNING_NO_DATA_CODE, 
                    Const.WARNING_NO_DATA_MSG,
                    new List<MemberViewAllDto>());
            }
            else
            {

                var memberDtos = members
                    .Select(promotions => promotions.MapToMemberViewAllDto())
                    .ToList();

                return new ServiceResult(Const.SUCCESS_READ_CODE, 
                    Const.SUCCESS_READ_MSG,
                    memberDtos);
            }
        }

        public async Task<IQueryable<MemberViewAllDto>> GetQueryable()
        {

            var members = await _unitOfWork.MemberRepository
                .GetAllMemberAsync();

            return members
                .Select(members => members.MapToMemberViewAllDto())
                .AsQueryable();
        }

        public async Task<IServiceResult> GetById(Guid memberId)
        {

            var member = await _unitOfWork.MemberRepository
                .GetByIdMemberAsync(memberId);

            if (member == null)
            {

                return new ServiceResult(Const.WARNING_NO_DATA_CODE, 
                    Const.WARNING_NO_DATA_MSG,
                    new MemberViewDetailsDto());
            }
            else
            {

                var memberDto = member.MapToMemberViewDetailsDto();

                return new ServiceResult(Const.SUCCESS_READ_CODE, 
                    Const.SUCCESS_READ_MSG,
                    memberDto);
            }
        }

        public async Task<IServiceResult> GetByUserId(Guid userId)
        {

            var member = await _unitOfWork.MemberRepository
                .GetMemberByUserId(userId);

            if (member == null)
            {

                return new ServiceResult(Const.WARNING_NO_DATA_CODE, 
                    Const.WARNING_NO_DATA_MSG,
                    new MemberViewDetailsDto());
            }
            else
            {

                var memberDto = member.MapToMemberAPIResonseDto();

                return new ServiceResult(Const.SUCCESS_READ_CODE, 
                    Const.SUCCESS_READ_MSG,
                    memberDto);
            }
        }

        public async Task<IServiceResult> Update(MemberUpdateDto memberDto)
        {
            try
            {

                // Check if the member exists in the database
                var member = await _unitOfWork.MemberRepository
                    .GetByIdMemberAsync(memberDto.MemberId);

                if (member == null)
                {

                    return new ServiceResult(Const.FAIL_UPDATE_CODE,
                        "Member not found.");
                }

                //Map DTO to Entity
                memberDto.MapToMemberUpdateDto(member);

                // Save data to database
                var result = await _unitOfWork.MemberRepository
                    .UpdateAsync(member);

                if (result > 0)
                {

                    // Map the saved entity to a response DTO
                    var responseDto = member.MapToMemberViewDetailsDto();

                    return new ServiceResult(Const.SUCCESS_UPDATE_CODE, 
                        Const.SUCCESS_UPDATE_MSG,
                        responseDto);
                }
                else
                {

                    return new ServiceResult(Const.FAIL_UPDATE_CODE, 
                        Const.FAIL_UPDATE_MSG);
                }
            }
            catch (Exception ex)
            {

                return new ServiceResult(Const.ERROR_EXCEPTION, 
                    ex.ToString());
            }
        }

        public async Task<IServiceResult> DeleteById(Guid memberId)
        {
            try
            {

                var member = await _unitOfWork.MemberRepository
                    .GetByIdMemberAsync(memberId);

                if (member == null)
                {

                    return new ServiceResult(Const.WARNING_NO_DATA_CODE, 
                        Const.WARNING_NO_DATA_MSG,
                        new MemberDeleteDto());
                }
                else
                {

                    var deleteMemberDto = member.MapToMemberDeleteDto();

                    var result = await _unitOfWork.MemberRepository
                        .RemoveAsync(member);

                    if (result)
                    {

                        return new ServiceResult(Const.SUCCESS_DELETE_CODE, 
                            Const.SUCCESS_DELETE_MSG,
                            deleteMemberDto);
                    }
                    else
                    {

                        return new ServiceResult(Const.FAIL_DELETE_CODE,
                            Const.FAIL_DELETE_MSG,
                            deleteMemberDto);
                    }
                }
            }
            catch (Exception ex)
            {

                return new ServiceResult(Const.ERROR_EXCEPTION, 
                    ex.ToString());
            }
        }

        public async Task<IServiceResult> Create(MemberCreateDto memberCreateDto)
        {
            try
            {

                // Check if the member exists in the database
                var member = await _unitOfWork.MemberRepository
                    .GetMemberByUserId(memberCreateDto.UserId);

                if (member != null)
                {

                    return new ServiceResult(Const.FAIL_CREATE_CODE,
                        "Member already exists.");
                }

                // Map DTO to Entity
                var newMember = memberCreateDto.MapToMemberCreateDto();

                // Save data to database
                var result = await _unitOfWork.MemberRepository
                    .CreateAsync(newMember);

                if (result > 0)
                {

                    // Assign Free package if PackageId is invalid or not passed
                    var packageId = memberCreateDto.PackageId > 0 
                        ? memberCreateDto.PackageId : 1;

                    // Create MemberMembership with Free plan
                    var newMemberMembership = new MemberMembership
                    {
                        MemberMembershipId = Guid.NewGuid(),
                        MemberId = newMember.MemberId,
                        PackageId = packageId,
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now.AddMonths(120),
                        Status = "Active",
                        IsActive = true,
                        Description = "Gói thành viên Free",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };

                    await _unitOfWork.MemberMembershipRepository
                        .CreateAsync(newMemberMembership);

                    // Get Member back from database to ensure full data
                    var savedMember = await _unitOfWork.MemberRepository
                        .GetMemberByUserId(newMember.UserId);

                    if (savedMember == null)
                    {
                        return new ServiceResult(Const.FAIL_CREATE_CODE,
                            "Failed to retrieve member details after creation.");
                    }

                    // Map the saved entity to a response DTO
                    var responseDto = savedMember.MapToMemberViewDetailsDto();

                    return new ServiceResult(Const.SUCCESS_CREATE_CODE,
                        Const.SUCCESS_CREATE_MSG,
                        responseDto);
                }
                else
                {

                    return new ServiceResult(Const.FAIL_CREATE_CODE,
                        Const.FAIL_CREATE_MSG);
                }
            }
            catch (Exception ex)
            {

                return new ServiceResult(Const.ERROR_EXCEPTION,
                    ex.ToString());
            }
        }
    }
}
