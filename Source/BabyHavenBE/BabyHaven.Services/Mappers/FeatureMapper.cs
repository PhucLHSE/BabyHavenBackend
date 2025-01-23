using BabyHaven.Common.DTOs.FeatureDTOs;
using BabyHaven.Common.Enum.FeatureEnums;
using BabyHaven.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.Mappers
{
    public static class FeatureMapper
    {
        // Mapper FeatureViewAllDto
        public static FeatureViewAllDto MapToFeatureViewAllDto(this Feature model)
        {
            return new FeatureViewAllDto
            {
                FeatureId = model.FeatureId,
                FeatureName = model.FeatureName,
                Description = model.Description,

                // Convert Status from string to enum
                Status = Enum.TryParse<FeatureStatus>(model.Status, true, out var status)
                          ? status
                          : FeatureStatus.Inactive
            };
        }

        // Mapper FeatureViewDetailsDto
        public static FeatureViewDetailsDto MapToFeatureViewDetailsDto(this Feature model)
        {
            return new FeatureViewDetailsDto
            {
                FeatureId = model.FeatureId,
                FeatureName = model.FeatureName,
                Description = model.Description,

                //Convert Status from string to enum
                Status = Enum.TryParse<FeatureStatus>(model.Status, true, out var status)
                          ? status
                          : FeatureStatus.Inactive,

                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt
            };
        }

        //Mapper FeatureCreateDto
        public static Feature MapToFeatureCreateDto(this FeatureCreateDto dto)
        {
            return new Feature
            {
                FeatureName = dto.FeatureName,
                Description = dto.Description,
                Status = dto.Status.ToString()
            };
        }
    }
}
