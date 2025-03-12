using BabyHaven.Common;
using BabyHaven.Common.DTOs.FeatureDTOs;
using BabyHaven.Repositories;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.Services
{
    public class FeatureService : IFeatureService
    {
        private readonly UnitOfWork _unitOfWork;

        public FeatureService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork
                ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<IServiceResult> GetAll()
        {

            var features = await _unitOfWork.FeatureRepository
                .GetAllAsync();

            if (features == null || !features.Any())
            {

                return new ServiceResult(Const.WARNING_NO_DATA_CODE, 
                    Const.WARNING_NO_DATA_MSG, 
                    new List<FeatureViewAllDto>());
            }
            else
            {

                var featureDtos = features
                    .Select(features => features.MapToFeatureViewAllDto())
                    .ToList();

                return new ServiceResult(Const.SUCCESS_READ_CODE, 
                    Const.SUCCESS_READ_MSG, 
                    featureDtos);
            }
        }

        public async Task<IServiceResult> GetById(int FeatureId)
        {

            var feature = await _unitOfWork.FeatureRepository
                .GetByIdAsync(FeatureId);

            if (feature == null)
            {

                return new ServiceResult(Const.WARNING_NO_DATA_CODE, 
                    Const.WARNING_NO_DATA_MSG,
                    new FeatureViewDetailsDto());
            }
            else
            {

                var featureDto = feature.MapToFeatureViewDetailsDto();

                return new ServiceResult(Const.SUCCESS_READ_CODE, 
                    Const.SUCCESS_READ_MSG,
                    featureDto);
            }
        }

        public async Task<IServiceResult> Create(FeatureCreateDto featureDto)
        {
            try
            {

                // Check if the feature exists in the database
                var feature = await _unitOfWork.FeatureRepository
                    .GetByFeatureNameAsync(featureDto.FeatureName);

                if (feature != null)
                {

                    return new ServiceResult(Const.FAIL_CREATE_CODE, 
                        "Feature with the same name already exists.");
                }

                // Map DTO to Entity
                var newFeature = featureDto.MapToFeatureCreateDto();

                // Add creation and update time information
                newFeature.CreatedAt = DateTime.UtcNow;
                newFeature.UpdatedAt = DateTime.UtcNow;

                // Save data to database
                var result = await _unitOfWork.FeatureRepository
                    .CreateAsync(newFeature);

                if (result > 0)
                {

                    // Map the saved entity to a response DTO
                    var responseDto = newFeature.MapToFeatureViewDetailsDto();

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

        public async Task<IServiceResult> Update(FeatureUpdateDto featureDto)
        {
            try
            {

                // Check if the package exists in the database
                var feature = await _unitOfWork.FeatureRepository
                    .GetByIdAsync(featureDto.FeatureId);

                if (feature == null)
                {

                    return new ServiceResult(Const.FAIL_UPDATE_CODE,
                        "Feature not found.");
                }

                //Map DTO to Entity
                featureDto.MapToFeatureUpdateDto(feature);

                // Update time information
                feature.UpdatedAt = DateTime.UtcNow;

                // Save data to database
                var result = await _unitOfWork.FeatureRepository
                    .UpdateAsync(feature);

                if (result > 0)
                {

                    // Map the saved entity to a response DTO
                    var responseDto = feature.MapToFeatureViewDetailsDto();

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

        public async Task<IServiceResult> DeleteById(int FeatureId)
        {
            try
            {

                var feature = await _unitOfWork.FeatureRepository
                    .GetByIdAsync(FeatureId);

                if (feature == null)
                {

                    return new ServiceResult(Const.WARNING_NO_DATA_CODE,
                        Const.WARNING_NO_DATA_MSG,
                        new FeatureDeleteDto());
                }
                else
                {

                    var deleteFeatureDto = feature.MapToFeatureDeleteDto();

                    var result = await _unitOfWork.FeatureRepository
                        .RemoveAsync(feature);

                    if (result)
                    {

                        return new ServiceResult(Const.SUCCESS_DELETE_CODE, 
                            Const.SUCCESS_DELETE_MSG,
                            deleteFeatureDto);
                    }
                    else
                    {

                        return new ServiceResult(Const.FAIL_DELETE_CODE,
                            Const.FAIL_DELETE_MSG,
                            deleteFeatureDto);
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
