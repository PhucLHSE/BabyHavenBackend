using BabyHaven.Common;
using BabyHaven.Common.DTOs.MembershipPackageDTOs;
using BabyHaven.Repositories;
using BabyHaven.Repositories.Mappers;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.Services
{
    public class MembershipPackageService : IMembershipPackageService
    {
        private readonly UnitOfWork _unitOfWork;

        public MembershipPackageService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork
                ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<IServiceResult> GetAll()
        {

            var membershipPackages = await _unitOfWork.MembershipPackageRepository
                .GetAllAsync();

            if (membershipPackages == null || !membershipPackages.Any())
            {

                return new ServiceResult(Const.WARNING_NO_DATA_CODE, 
                    Const.WARNING_NO_DATA_MSG, 
                    new List<MembershipPackageViewAllDto>());
            }
            else
            {

                var membershipPackageDtos = membershipPackages
                    .Select(packages => packages.MapToMembershipPackageViewAllDto())
                    .ToList();

                return new ServiceResult(Const.SUCCESS_READ_CODE, 
                    Const.SUCCESS_READ_MSG,
                    membershipPackageDtos);
            }
        }

        public async Task<IQueryable<MembershipPackageViewAllDto>> GetQueryable()
        {

            var membershipPackages = await _unitOfWork.MembershipPackageRepository
                .GetAllAsync();

            return membershipPackages
                .Select(packages => packages.MapToMembershipPackageViewAllDto())
                .AsQueryable();
        }

        public async Task<IServiceResult> GetById(int PackageId)
        {

            var membershipPackage = await _unitOfWork.MembershipPackageRepository
                .GetByIdAsync(PackageId);

            if (membershipPackage == null)
            {

                return new ServiceResult(Const.WARNING_NO_DATA_CODE, 
                    Const.WARNING_NO_DATA_MSG,
                    new MembershipPackageViewDetailsDto());
            }
            else
            {

                var membershipPackageDto = membershipPackage.MapToMembershipPackageViewDetailsDto();

                return new ServiceResult(Const.SUCCESS_READ_CODE, 
                    Const.SUCCESS_READ_MSG,
                    membershipPackageDto);
            }
        }

        public async Task<IServiceResult> Create(MembershipPackageCreateDto membershipPackageDto)
        {
            try
            {

                // Check if the package exists in the database
                var membershipPackage = await _unitOfWork.MembershipPackageRepository
                    .GetByPackageNameAsync(membershipPackageDto.PackageName);

                if (membershipPackage != null)
                {

                    return new ServiceResult(Const.FAIL_CREATE_CODE,
                        "Package with the same name already exists.");
                }

                // Map DTO to Entity
                var newMembershipPackage = membershipPackageDto.MapToMembershipPackageCreateDto();

                // Add creation and update time information
                newMembershipPackage.CreatedAt = DateTime.UtcNow;
                newMembershipPackage.UpdatedAt = DateTime.UtcNow;

                // Save data to database
                var result = await _unitOfWork.MembershipPackageRepository
                    .CreateAsync(newMembershipPackage);

                if (result > 0)
                {

                    // Map the saved entity to a response DTO
                    var responseDto = newMembershipPackage.MapToMembershipPackageViewDetailsDto();

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

        public async Task<IServiceResult> Update(MembershipPackageUpdateDto membershipPackageDto)
        {
            try
            {

                // Check if the package exists in the database
                var membershipPackage = await _unitOfWork.MembershipPackageRepository
                    .GetByIdAsync(membershipPackageDto.PackageId);

                if (membershipPackage == null)
                {

                    return new ServiceResult(Const.FAIL_UPDATE_CODE,
                        "Package not found.");
                }

                //Map DTO to Entity
                membershipPackageDto.MapToMembershipPackageUpdateDto(membershipPackage);

                // Update time information
                membershipPackage.UpdatedAt = DateTime.UtcNow;

                // Save data to database
                var result = await _unitOfWork.MembershipPackageRepository
                    .UpdateAsync(membershipPackage);

                if (result > 0)
                {

                    // Map the saved entity to a response DTO
                    var responseDto = membershipPackage.MapToMembershipPackageViewDetailsDto();

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

        public async Task<IServiceResult> DeleteById(int PackageId)
        {
            try
            {

                var membershipPackage = await _unitOfWork.MembershipPackageRepository
                    .GetByIdAsync(PackageId);

                if (membershipPackage == null)
                {

                    return new ServiceResult(Const.WARNING_NO_DATA_CODE, 
                        Const.WARNING_NO_DATA_MSG,
                        new MembershipPackageDeleteDto());
                }
                else
                {

                    var deleteMembershipPackageDto = membershipPackage.MapToMembershipPackageDeleteDto();

                    var result = await _unitOfWork.MembershipPackageRepository
                        .RemoveAsync(membershipPackage);

                    if (result)
                    {

                        return new ServiceResult(Const.SUCCESS_DELETE_CODE, 
                            Const.SUCCESS_DELETE_MSG,
                            deleteMembershipPackageDto);
                    }
                    else
                    {

                        return new ServiceResult(Const.FAIL_DELETE_CODE, 
                            Const.FAIL_DELETE_MSG,
                            deleteMembershipPackageDto);
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
