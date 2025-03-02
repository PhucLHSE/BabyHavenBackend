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

        public PackageFeatureService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork
                ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<IServiceResult> GetAll()
        {
            var packageFeatures = await _unitOfWork.PackageFeatureRepository
                .GetAllPackageFeatureAsync();

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
            var packageFeature = await _unitOfWork.PackageFeatureRepository
                .GetByIdPackageFeatureAsync(PackageId, FeatureId);

            if (packageFeature == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG,
                    new PackageFeatureViewDetailsDto());
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
                var packageNameToIdMapping = await _unitOfWork.MembershipPackageRepository
                    .GetAllPackageNameToIdMappingAsync();

                var featureNameToIdMapping = await _unitOfWork.FeatureRepository
                    .GetAllFeatureNameToIdMappingAsync();

                // Check existence and retrieve PackageId
                if (!packageNameToIdMapping
                    .TryGetValue(packageFeatureDto.PackageName, out var packageId))
                {
                    return new ServiceResult(Const.FAIL_UPDATE_CODE,
                        $"PackageName '{packageFeatureDto.PackageName}' does not exist.");
                }

                // Check if the provided FeatureName exists
                if (!featureNameToIdMapping
                    .TryGetValue(packageFeatureDto.FeatureName, out var featureId))
                {
                    return new ServiceResult(Const.FAIL_UPDATE_CODE,
                        $"FeatureName '{packageFeatureDto.FeatureName}' does not exist.");
                }

                // Check if the PackageFeature already exists in the database
                var existingPackageFeature = await _unitOfWork.PackageFeatureRepository
                    .GetByIdPackageFeatureAsync(packageId, featureId);

                if (existingPackageFeature != null && existingPackageFeature.PackageId > 0)
                {
                    return new ServiceResult(Const.FAIL_CREATE_CODE, 
                        "The specified PackageFeature already exists.");
                }

                // Map the DTO to an entity object
                var newPackageFeature = packageFeatureDto.MapToPackageFeatureCreateDto(packageId, featureId);

                // Save the new entity to the database
                var result = await _unitOfWork.PackageFeatureRepository
                    .CreateAsync(newPackageFeature);

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

        public async Task<IServiceResult> Update(PackageFeatureUpdateDto packageFeatureDto)
        {
            try
            {
                // // Retrieve mappings: PackageName -> PackageId and FeatureName -> FeatureId
                var packageNameToIdMapping = await _unitOfWork.MembershipPackageRepository
                    .GetAllPackageNameToIdMappingAsync();

                var featureNameToIdMapping = await _unitOfWork.FeatureRepository
                    .GetAllFeatureNameToIdMappingAsync();

                // Check existence and retrieve PackageId
                if (!packageNameToIdMapping
                    .TryGetValue(packageFeatureDto.PackageName, out var packageId))
                {
                    return new ServiceResult(Const.FAIL_UPDATE_CODE,
                        $"PackageName '{packageFeatureDto.PackageName}' does not exist.");
                }    
                    
                // Check if the provided FeatureName exists
                if (!featureNameToIdMapping
                    .TryGetValue(packageFeatureDto.FeatureName, out var featureId))
                {
                    return new ServiceResult(Const.FAIL_UPDATE_CODE,
                        $"FeatureName '{packageFeatureDto.FeatureName}' does not exist.");
                }   

                //  Check if the PackageFeature already exists in the database
                var existingPackageFeature = await _unitOfWork.PackageFeatureRepository.
                    GetByIdPackageFeatureAsync(packageId, featureId);

                if (existingPackageFeature == null)
                    return new ServiceResult(Const.FAIL_UPDATE_CODE, 
                        "The specified PackageFeature does not exist.");

                // Map the update data
                existingPackageFeature.MapToUpdatedPackageFeature(packageFeatureDto);

                // Update time information
                existingPackageFeature.UpdatedAt = DateTime.Now;

                // Save the new entity to the database
                var result = await _unitOfWork.PackageFeatureRepository
                    .UpdateAsync(existingPackageFeature);

                if (result > 0)
                {
                    return new ServiceResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, 
                        packageFeatureDto);
                }
                else
                {
                    return new ServiceResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG, 
                        packageFeatureDto);
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IServiceResult> DeleteById(int PackageId, int FeatureId)
        {
            try
            {
                // Retrieve the PackageFeature using the provided PackageId and FeatureId
                var packageFeature = await _unitOfWork.PackageFeatureRepository
                    .GetByIdPackageFeatureAsync(PackageId, FeatureId);

                // Check if the PackageFeature exists
                if (packageFeature == null)
                {
                    return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG,
                        new PackageFeatureDeleteDto());
                }
                else
                {
                    // Map to PackageFeatureDeleteDto for response
                    var deletePackageFeatureDto = packageFeature.MapToPackageFeatureDeleteDto();

                    var result = await _unitOfWork.PackageFeatureRepository
                        .RemoveAsync(packageFeature);

                    if (result)
                    {
                        return new ServiceResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG,
                            deletePackageFeatureDto);
                    }
                    else
                    {
                        return new ServiceResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG,
                            deletePackageFeatureDto);
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
