using BabyHaven.Common;
using BabyHaven.Repositories;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common.DTOs.MemberMembershipDTOs;
using BabyHaven.Services.Mappers;

namespace BabyHaven.Services.Services
{
    public class MemberMembershipService : IMemberMembershipService
    {
        private readonly UnitOfWork _unitOfWork;

        public MemberMembershipService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork
                ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<IServiceResult> GetAll()
        {

            var memberMemberships = await _unitOfWork.MemberMembershipRepository
            .GetAllMemberMembershipAsync();

            if (memberMemberships == null || !memberMemberships.Any())
            {

                return new ServiceResult(Const.WARNING_NO_DATA_CODE, 
                    Const.WARNING_NO_DATA_MSG,
                    new List<MemberMembershipViewAllDto>());
            }
            else
            {

                var memberMembershipDtos = memberMemberships
                    .Select(memberMemberships => memberMemberships.MapToMemberMembershipViewAllDto())
                    .ToList();

                return new ServiceResult(Const.SUCCESS_READ_CODE,
                    Const.SUCCESS_READ_MSG,
                    memberMembershipDtos);
            }
        }

        public async Task<IQueryable<MemberMembershipViewAllDto>> GetQueryable()
        {

            var memberMemberships = await _unitOfWork.MemberMembershipRepository
                .GetAllMemberMembershipAsync();

            return memberMemberships
                .Select(memberMemberships => memberMemberships.MapToMemberMembershipViewAllDto())
                .AsQueryable();
        }

        public async Task<IServiceResult> GetById(Guid MemberMembershipId)
        {

            var memberMembership = await _unitOfWork.MemberMembershipRepository
                .GetByIdMemberMembershipAsync(MemberMembershipId);

            if (memberMembership == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, 
                    Const.WARNING_NO_DATA_MSG,
                    new MemberMembershipViewDetailsDto());
            }
            else
            {
                var memberMembershipDto = memberMembership.MapToMemberMembershipViewDetailsDto();

                return new ServiceResult(Const.SUCCESS_READ_CODE,
                    Const.SUCCESS_READ_MSG,
                    memberMembershipDto);
            }
        }

        public async Task<IServiceResult> GetByMemberId(Guid memberId)
        {

            var memberMembership = await _unitOfWork.MemberMembershipRepository
                .GetByMemberId(memberId);

            if (memberMembership == null)
            {

                return new ServiceResult(Const.WARNING_NO_DATA_CODE, 
                    Const.WARNING_NO_DATA_MSG,
                    new MemberMembershipViewDetailsDto());
            }
            else
            {

                var memberMembershipDto = memberMembership.MapToMemberMembershipViewDetailsDto();

                return new ServiceResult(Const.SUCCESS_READ_CODE, 
                    Const.SUCCESS_READ_MSG,
                    memberMembershipDto);
            }
        }

        public async Task<IServiceResult> Create(MemberMembershipCreateDto memberMembershipDto)
        {
            try
            {

                // Retrieve mappings: MemberName -> MemberId and PackageName -> PackageId
                var member = await _unitOfWork.MemberRepository
                    .GetByIdAsync(memberMembershipDto.MemberId);

                if(member == null)
                {

                    return new ServiceResult(Const.FAIL_CREATE_CODE,
                        $"MemberId '{memberMembershipDto.MemberId}' does not exist.");
                }

                var package = await _unitOfWork.MembershipPackageRepository
                    .GetByPackageNameAsync(memberMembershipDto.PackageName);

                if (package == null)
                {

                    return new ServiceResult(Const.FAIL_CREATE_CODE,
                        $"PackageName '{memberMembershipDto.PackageName}' does not exist.");
                }

                //// Generate unique MemberMembership ID
                //var memberMembershipId = Guid.NewGuid();

                // Map DTO to entity with IDs
                var newMemberMembership = memberMembershipDto.MapToMemberMembershipCreateDto(member, package);

                // Save the new entity to the database
                var result = await _unitOfWork.MemberMembershipRepository
                    .CreateAsync(newMemberMembership);

                if (result > 0)
                {

                    return new ServiceResult(Const.SUCCESS_CREATE_CODE, 
                        Const.SUCCESS_CREATE_MSG,
                        newMemberMembership.MemberMembershipId);
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

        public async Task<IServiceResult> Update(MemberMembershipUpdateDto memberMembershipDto)
        {
            try
            {

                // Retrieve mappings: MemberName -> MemberId and PackageName -> PackageId
                var memberNameToIdMapping = await _unitOfWork.MemberRepository
                    .GetAllMemberNameToIdMappingAsync();

                var packageNameToIdMapping = await _unitOfWork.MembershipPackageRepository
                    .GetAllPackageNameToIdMappingAsync();

                // Check existence and retrieve MemberId from MemberName
                if (!memberNameToIdMapping
                    .TryGetValue(memberMembershipDto.MemberName, out var memberId))
                {

                    return new ServiceResult(Const.FAIL_CREATE_CODE,
                        $"MemberName '{memberMembershipDto.MemberName}' does not exist.");
                }

                // Check existence and retrieve PackageId from PackageName
                if (!packageNameToIdMapping
                    .TryGetValue(memberMembershipDto.PackageName, out var packageId))
                {

                    return new ServiceResult(Const.FAIL_CREATE_CODE,
                        $"PackageName '{memberMembershipDto.PackageName}' does not exist.");
                }

                // Check if active membership already exists
                if (await _unitOfWork.MemberMembershipRepository.HasActiveMembershipAsync(memberId, packageId))
                {

                    return new ServiceResult(Const.FAIL_CREATE_CODE,
                        "An active membership for this package already exists.");
                }

                //  Check if the MemberMembership already exists in the database
                var existingMemberMembership = await _unitOfWork.MemberMembershipRepository.
                    GetByIdMemberMembershipAsync(memberMembershipDto.MemberMembershipId);

                if (existingMemberMembership == null)
                    return new ServiceResult(Const.FAIL_UPDATE_CODE,
                        "The specified MemberMembership does not exist.");

                // Map the update data
                existingMemberMembership.MapToMemberMembershipUpdateDto(memberMembershipDto);

                // Update time information
                existingMemberMembership.UpdatedAt = DateTime.Now;

                // Save the new entity to the database
                var result = await _unitOfWork.MemberMembershipRepository
                    .UpdateAsync(existingMemberMembership);

                // Retrieve full entity with includes for Member and Package
                var memberMembership = await _unitOfWork.MemberMembershipRepository
                    .GetByIdMemberMembershipAsync(existingMemberMembership.MemberMembershipId);

                if (memberMembership?.Member?.User == null)
                {

                    return new ServiceResult(Const.FAIL_CREATE_CODE, 
                        "Member or User information is missing.");
                }

                // Retrieve names from navigation properties
                var memberName = memberMembership.Member.User.Name;

                var packageName = await _unitOfWork.MembershipPackageRepository
                    .GetByIdAsync(existingMemberMembership.PackageId);

                // Map retrieved details to response DTO
                var responseDto = memberMembership.MapToMemberMembershipViewDetailsDto();

                responseDto.MemberName = memberName;

                responseDto.PackageName = packageName?.PackageName ?? "Unknown Package";

                if (result > 0)
                {

                    return new ServiceResult(Const.SUCCESS_UPDATE_CODE, 
                        Const.SUCCESS_UPDATE_MSG,
                        responseDto);
                }
                else
                {

                    return new ServiceResult(Const.FAIL_UPDATE_CODE, 
                        Const.FAIL_UPDATE_MSG,
                        responseDto);
                }
            }
            catch (Exception ex)
            {

                return new ServiceResult(Const.ERROR_EXCEPTION, 
                    ex.ToString());
            }
        }

        public async Task<IServiceResult> DeleteById(Guid MemberMembershipId)
        {
            try
            {

                var memberMembership = await _unitOfWork.MemberMembershipRepository
                    .GetByIdMemberMembershipAsync(MemberMembershipId);

                if (memberMembership == null)
                {

                    return new ServiceResult(Const.WARNING_NO_DATA_CODE, 
                        Const.WARNING_NO_DATA_MSG,
                        new MemberMembershipDeleteDto());
                }
                else
                {

                    var deleteMemberMembershipDto = memberMembership.MapToMemberMembershipDeleteDto();

                    var result = await _unitOfWork.MemberMembershipRepository
                        .RemoveAsync(memberMembership);

                    if (result)
                    {

                        return new ServiceResult(Const.SUCCESS_DELETE_CODE, 
                            Const.SUCCESS_DELETE_MSG,
                            deleteMemberMembershipDto);
                    }
                    else
                    {

                        return new ServiceResult(Const.FAIL_DELETE_CODE, 
                            Const.FAIL_DELETE_MSG,
                            deleteMemberMembershipDto);
                    }
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
