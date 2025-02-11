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
                // Feature
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
                // Feature
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

        //Mapper FeatureUpdateDto
        public static void MapToFeatureUpdateDto(this FeatureUpdateDto updateDto, Feature feature)
        {
            if (!string.IsNullOrWhiteSpace(updateDto.FeatureName))
                feature.FeatureName = updateDto.FeatureName;

            if (!string.IsNullOrWhiteSpace(updateDto.Description))
                feature.Description = updateDto.Description;

            if (updateDto.Status.HasValue)
                feature.Status = updateDto.Status.ToString();
        }

        // Mapper FeatureDeleteDto
        public static FeatureDeleteDto MapToFeatureDeleteDto(this Feature model)
        {
            return new FeatureDeleteDto
            {
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
    }
}
