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

        public async Task<IServiceResult> Create(PackageFeatureCreateDto packageFeatureDto)
        {
            try
            {
                // Retrieve mappings: PackageName -> PackageId and FeatureName -> FeatureId
                var packageNameToIdMapping = await _unitOfWork.MembershipPackageRepository.GetAllPackageNameToIdMappingAsync();
                var featureNameToIdMapping = await _unitOfWork.FeatureRepository.GetAllFeatureNameToIdMappingAsync();

                // Check if the provided PackageName exists
                if (!packageNameToIdMapping.ContainsKey(packageFeatureDto.PackageName))
                {
                    return new ServiceResult(Const.FAIL_CREATE_CODE, 
                        $"PackageName '{packageFeatureDto.PackageName}' does not exist.");
                }

                // Check if the provided FeatureName exists
                if (!featureNameToIdMapping.ContainsKey(packageFeatureDto.FeatureName))
                {
                    return new ServiceResult(Const.FAIL_CREATE_CODE, 
                        $"FeatureName '{packageFeatureDto.FeatureName}' does not exist.");
                }

                // Check if PackageFeature already exists in the database
                var packageId = packageNameToIdMapping[packageFeatureDto.PackageName];
                var featureId = featureNameToIdMapping[packageFeatureDto.FeatureName];

                // Check if the PackageFeature already exists in the database
                var existingPackageFeature = await _unitOfWork.PackageFeatureRepository
                    .GetByIdPackageFeatureAsync(packageId, featureId);

                if (existingPackageFeature != null && existingPackageFeature.PackageId > 0)
                {
                    return new ServiceResult(Const.FAIL_CREATE_CODE, 
                        "The specified PackageFeature already exists.");
                }

                // Map the DTO to an entity object
                var newPackageFeature = packageFeatureDto.MapToPackageFeature(packageId, featureId);

                // Add timestamp for creation
                newPackageFeature.CreatedAt = DateTime.UtcNow;

                // Save the new entity to the database
                var result = await _unitOfWork.PackageFeatureRepository.CreateAsync(newPackageFeature);

                if (result > 0)
                {
                    var responseDto = new PackageFeatureCreateDto
                    {
                        PackageName = packageFeatureDto.PackageName,
                        FeatureName = packageFeatureDto.FeatureName,
                        Status = packageFeatureDto.Status
                    };

                    return new ServiceResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, 
                        responseDto);
                }
                else
                {
                    return new ServiceResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }
    }
}
