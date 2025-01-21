using BabyHaven.Common;
using BabyHaven.Common.DTOs.MembershipPackageDTOs;
using BabyHaven.Common.Enum.MembershipPackageEnums;
using BabyHaven.Repositories;
using BabyHaven.Repositories.Mappers;
using BabyHaven.Repositories.Models;
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

        public MembershipPackageService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IServiceResult> GetAll()
        {
            var membershipPackages = await _unitOfWork.MembershipPackageRepository.GetAllAsync();

            if (membershipPackages == null || !membershipPackages.Any())
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, 
                    new List<MembershipPackageViewAllDto>());
            }
            else
            {
                var membershipPackageDtos = membershipPackages
                    .Select(package => package.MapToMembershipPackageViewAllDto())
                    .ToList();

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG,
                    membershipPackageDtos);
            }
        }

        public async Task<IServiceResult> GetById(int PackageId)
        {
            var membershipPackage = await _unitOfWork.MembershipPackageRepository.GetByIdAsync(PackageId);

            if (membershipPackage == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new MembershipPackageViewDetailsDto());
            }
            else
            {
                var membershipPackageDto = membershipPackage.MapToMembershipPackageViewDetailsDto();

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, membershipPackageDto);
            }
        }

        public async Task<IServiceResult> Save(MembershipPackageCreateDto MembershipPackageDto)
        {
            try
            {
                int result = -1;

                // Map DTO to Entity
                var membershipPackageDto = MembershipPackageDto.MapToMembershipPackageCreateDto();

                // Check if the package exists in the database
                var membershipPackageTmp = await _unitOfWork.MembershipPackageRepository.GetByIdAsync(membershipPackageDto.PackageId);

                if (membershipPackageTmp != null)
                {
                    // Update current fields directly
                    membershipPackageTmp.PackageName = membershipPackageDto.PackageName;
                    membershipPackageTmp.Description = membershipPackageDto.Description;
                    membershipPackageTmp.Price = membershipPackageDto.Price;
                    membershipPackageTmp.Currency = membershipPackageDto.Currency;
                    membershipPackageTmp.DurationMonths = membershipPackageDto.DurationMonths;
                    membershipPackageTmp.TrialPeriodDays = membershipPackageDto.TrialPeriodDays;
                    membershipPackageTmp.MaxChildrenAllowed = membershipPackageDto.MaxChildrenAllowed;
                    membershipPackageTmp.SupportLevel = membershipPackageDto.SupportLevel;
                    membershipPackageTmp.Status = membershipPackageDto.Status;

                    result = await _unitOfWork.MembershipPackageRepository.UpdateAsync(membershipPackageTmp);

                    if (result > 0)
                    {
                        return new ServiceResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, membershipPackageTmp);
                    }
                    else
                    {
                        return new ServiceResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                    }
                }
                else
                {
                    result = await _unitOfWork.MembershipPackageRepository.CreateAsync(membershipPackageDto);

                    if (result > 0)
                    {
                        return new ServiceResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, MembershipPackageDto);
                    }
                    else
                    {
                        return new ServiceResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, MembershipPackageDto);
                    }
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }
    }
}
