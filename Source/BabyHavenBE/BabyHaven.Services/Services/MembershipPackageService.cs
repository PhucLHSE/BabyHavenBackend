using BabyHaven.Common;
using BabyHaven.Common.Enum.MembershipPackageEnums;
using BabyHaven.Repositories;
using BabyHaven.Repositories.DTOs.MembershipPackageDTOs;
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
                var membershipPackageDtos = membershipPackages.Select(package => new MembershipPackageViewAllDto
                {
                    PackageId = package.PackageId,
                    PackageName = package.PackageName,
                    Description = package.Description,
                    Price = package.Price,
                    Currency = package.Currency,
                    DurationMonths = package.DurationMonths,
                    IsRecurring = package.IsRecurring,
                    MaxChildrenAllowed = package.MaxChildrenAllowed,
                    Status = Enum.Parse<MembershipPackageStatus>(package.Status, true),
                    PromotionCode = package.Promotion?.PromotionCode,
                    DiscountPercent = package.Promotion?.DiscountPercent
                }).ToList();

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG,
                    membershipPackageDtos);
            }
        }
    }
}
