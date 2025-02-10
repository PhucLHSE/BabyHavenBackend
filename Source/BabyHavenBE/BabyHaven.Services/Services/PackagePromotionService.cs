using BabyHaven.Common;
using BabyHaven.Repositories;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common.DTOs.PackagePromotionDTOs;

namespace BabyHaven.Services.Services
{
    public class PackagePromotionService : IPackagePromotionService
    {
        private readonly UnitOfWork _unitOfWork;

        public PackagePromotionService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IServiceResult> GetAll()
        {
            var packagePromotions = await _unitOfWork.PackagePromotionRepository.GetAllPackagePromotionAsync();

            if (packagePromotions == null || !packagePromotions.Any())
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG,
                    new List<PackagePromotionViewAllDto>());
            }
            else
            {
                var packagePromotionDtos = packagePromotions
                    .Select(packagePromotion => packagePromotion.MapToPackagePromotionViewAllDto())
                    .ToList();

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG,
                    packagePromotionDtos);
            }
        }
    }
}
