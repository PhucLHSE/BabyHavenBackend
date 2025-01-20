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
            var membershipPackages = await _unitOfWork.MembershipPackageRepository.GetAllMembershipPackageAsync();

            if (membershipPackages == null || !membershipPackages.Any())
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, 
                    new List<MembershipPackageViewAllDto>());
            }
            else
            {
                var membershipPackageDtos = membershipPackages.Select(package => package.MapToMembershipPackageViewAllDto()).ToList();

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG,
                    membershipPackageDtos);
            }
        }

        public async Task<IServiceResult> GetById(int PackageId)
        {
            var membershipPackage = await _unitOfWork.MembershipPackageRepository.GetByIdMembershipPackageAsync(PackageId);

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

        public async Task<IServiceResult> Save(MembershipPackage membershipPackage)
        {
            try
            {
                int result = -1;

                var membershipPackageTmp = _unitOfWork.MembershipPackageRepository.GetById(membershipPackage.PackageId);

                if (membershipPackageTmp != null)
                {
                    result = await _unitOfWork.MembershipPackageRepository.UpdateAsync(membershipPackage);

                    if (result > 0)
                    {
                        return new ServiceResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, membershipPackage);
                    }
                    else
                    {
                        return new ServiceResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                    }
                }
                else
                {
                    result = await _unitOfWork.MembershipPackageRepository.CreateAsync(membershipPackage);

                    if (result > 0)
                    {
                        return new ServiceResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, membershipPackage);
                    }
                    else
                    {
                        return new ServiceResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, membershipPackage);
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
