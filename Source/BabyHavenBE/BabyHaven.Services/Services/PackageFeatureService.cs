using BabyHaven.Common.DTOs.FeatureDTOs;
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
using BabyHaven.Common.DTOs.PackageFeatureDTOs;

namespace BabyHaven.Services.Services
{
    public class PackageFeatureService : IPackageFeatureService
    {
        private readonly UnitOfWork _unitOfWork;

        public PackageFeatureService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IServiceResult> GetAll()
        {
            var packageFeatures = await _unitOfWork.PackageFeatureRepository.GetAllPackageFeatureAsync();

            if (packageFeatures == null || !packageFeatures.Any())
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG,
                    new List<PackageFeatureViewAllDto>());
            }
            else
            {
                var packageFeatureDtos = packageFeatures
                    .Select(packageFeature => packageFeature.MapToPackageFeatureViewAllDto())
                    .ToList();

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG,
                    packageFeatureDtos);
            }
        }

        public async Task<IServiceResult> GetById(int PackageId, int FeatureId)
        {
            var packageFeature = await _unitOfWork.PackageFeatureRepository.GetByIdPackageFeatureAsync(PackageId, FeatureId);

            if (packageFeature == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG,
                    new FeatureViewDetailsDto());
            }
            else
            {
                var packageFeatureDto = packageFeature.MapToPackageFeatureViewDetailsDto();

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG,
                    packageFeatureDto);
            }
        }
    }
}
