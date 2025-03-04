using BabyHaven.Common;
using BabyHaven.Repositories;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Mappers;
using BabyHaven.Common.DTOs.VNPayDTOS.PackagePromotionDTOs;

namespace BabyHaven.Services.Services
{
    public class PackagePromotionService : IPackagePromotionService
    {
        private readonly UnitOfWork _unitOfWork;

        public PackagePromotionService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork
                ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<IServiceResult> GetAll()
        {
            var packagePromotions = await _unitOfWork.PackagePromotionRepository
                .GetAllPackagePromotionAsync();

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

        public async Task<IServiceResult> GetById(int PackageId, Guid PromotionId)
        {
            var packagePromotion = await _unitOfWork.PackagePromotionRepository
                .GetByIdPackagePromotionAsync(PackageId, PromotionId);

            if (packagePromotion == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG,
                    new PackagePromotionViewDetailsDto());
            }
            else
            {
                var packagePromotionDto = packagePromotion.MapToPackagePromotionViewDetailsDto();

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG,
                    packagePromotionDto);
            }
        }

        public async Task<IServiceResult> Create(PackagePromotionCreateDto packagePromotionDto)
        {
            try
            {
                // Retrieve mappings: PackageName -> PackageId and PromotionCode -> PromotionId
                var packageNameToIdMapping = await _unitOfWork.MembershipPackageRepository
                    .GetAllPackageNameToIdMappingAsync();

                var promotionCodeToIdMapping = await _unitOfWork.PromotionRepository
                    .GetAllPromotionCodeToIdMappingAsync();

                // Check existence and retrieve PackageId from PackageName
                if (!packageNameToIdMapping
                    .TryGetValue(packagePromotionDto.PackageName, out var packageId))
                {
                    return new ServiceResult(Const.FAIL_CREATE_CODE,
                        $"PackageName '{packagePromotionDto.PackageName}' does not exist.");
                }

                // Check existence and retrieve PromotionId from PromotionCode
                if (!promotionCodeToIdMapping
                    .TryGetValue(packagePromotionDto.PromotionCode, out var promotionId))
                {
                    return new ServiceResult(Const.FAIL_CREATE_CODE,
                        $"PromotionCode '{packagePromotionDto.PromotionCode}' does not exist.");
                }

                // Check if the PackagePromotion already exists in the database
                var existingPackagePromotion = await _unitOfWork.PackagePromotionRepository
                    .GetByIdPackagePromotionAsync(packageId, promotionId);

                if (existingPackagePromotion != null)
                {
                    return new ServiceResult(Const.FAIL_CREATE_CODE,
                        "The specified PackageFeature already exists.");
                }

                // Map the DTO to an entity object
                var newPackagePromotion = packagePromotionDto.MapToPackagePromotionCreateDto(packageId, promotionId);

                // Save the new entity to the database
                var result = await _unitOfWork.PackagePromotionRepository
                    .CreateAsync(newPackagePromotion);

                if (result > 0)
                {
                    var responseDto = new PackagePromotionCreateDto
                    {
                        PackageName = packagePromotionDto.PackageName,
                        PromotionCode = packagePromotionDto.PromotionCode,
                        IsActive = packagePromotionDto.IsActive
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

        public async Task<IServiceResult> Update(PackagePromotionUpdateDto packagePromotionDto)
        {
            try
            {
                // Retrieve mappings: PackageName -> PackageId and PromotionCode -> PromotionId
                var packageNameToIdMapping = await _unitOfWork.MembershipPackageRepository
                    .GetAllPackageNameToIdMappingAsync();

                var promotionCodeToIdMapping = await _unitOfWork.PromotionRepository
                    .GetAllPromotionCodeToIdMappingAsync();

                // Check existence and retrieve PackageId from PackageName
                if (!packageNameToIdMapping
                    .TryGetValue(packagePromotionDto.PackageName, out var packageId))
                {
                    return new ServiceResult(Const.FAIL_CREATE_CODE,
                        $"PackageName '{packagePromotionDto.PackageName}' does not exist.");
                }

                // Check existence and retrieve PromotionId from PromotionCode
                if (!promotionCodeToIdMapping
                    .TryGetValue(packagePromotionDto.PromotionCode, out var promotionId))
                {
                    return new ServiceResult(Const.FAIL_CREATE_CODE,
                        $"PromotionCode '{packagePromotionDto.PromotionCode}' does not exist.");
                }

                //  Check if the PackagePromotion already exists in the database
                var existingPackagePromotion = await _unitOfWork.PackagePromotionRepository.
                    GetByIdPackagePromotionAsync(packageId, promotionId);

                if (existingPackagePromotion == null)
                    return new ServiceResult(Const.FAIL_UPDATE_CODE,
                        "The specified PackagePromotion does not exist.");

                // Map the update data
                existingPackagePromotion.MapToUpdatedPackagePromotion(packagePromotionDto);

                // Update time information
                existingPackagePromotion.UpdatedAt = DateTime.Now;

                // Save the new entity to the database
                var result = await _unitOfWork.PackagePromotionRepository
                    .UpdateAsync(existingPackagePromotion);

                if (result > 0)
                {
                    return new ServiceResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG,
                        packagePromotionDto);
                }
                else
                {
                    return new ServiceResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG,
                        packagePromotionDto);
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IServiceResult> DeleteById(int PackageId, Guid PromotionId)
        {
            try
            {
                // Retrieve the PackagePromotion using the provided PackageId and PromotionId
                var packagePromotion = await _unitOfWork.PackagePromotionRepository
                    .GetByIdPackagePromotionAsync(PackageId, PromotionId);

                // Check if the PackagePromotion exists
                if (packagePromotion == null)
                {
                    return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG,
                        new PackagePromotionDeleteDto());
                }
                else
                {
                    // Map to PackagePromotionDeleteDto for response
                    var deletePackagePromotionDto = packagePromotion.MapToPackagePromotionDeleteDto();

                    var result = await _unitOfWork.PackagePromotionRepository
                        .RemoveAsync(packagePromotion);

                    if (result)
                    {
                        return new ServiceResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG,
                            deletePackagePromotionDto);
                    }
                    else
                    {
                        return new ServiceResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG,
                            deletePackagePromotionDto);
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
