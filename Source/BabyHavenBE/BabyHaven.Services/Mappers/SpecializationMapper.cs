using BabyHaven.Common.DTOs.FeatureDTOs;
using BabyHaven.Common.DTOs.SpecializationDTOs;
using BabyHaven.Common.Enum.FeatureEnums;
using BabyHaven.Common.Enum.SpecializationEnums;
using BabyHaven.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.Mappers
{
    public static class SpecializationMapper
    {
        // Mapper SpecializationViewAllDto
        public static SpecializationViewAllDto MapToSpecializationViewAllDto(this Specialization model)
        {
            return new SpecializationViewAllDto
            {
                SpecializationId = model.SpecializationId,
                // Feature
                SpecializationName = model.SpecializationName,

                // Convert Status from string to enum
                Status = Enum.TryParse<SpecializationStatus>(model.Status, true, out var status)
                          ? status
                          : SpecializationStatus.Inactive
            };
        }

        // Mapper SpecializationViewDetailsDto
        public static SpecializationViewDetailsDto MapToSpecializationViewDetailsDto(this Specialization model)
        {
            return new SpecializationViewDetailsDto
            {
                // Specialization  
                SpecializationName = model.SpecializationName,
                Description = model.Description,

                //Convert Status from string to enum
                Status = Enum.TryParse<SpecializationStatus>(model.Status, true, out var status)
                          ? status
                          : SpecializationStatus.Inactive,

                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt
            };
        }

        //Mapper SpecializationCreateDto
        public static Specialization MapToSpecializationCreateDto(this SpecializationCreateDto dto)
        {
            return new Specialization
            {
                SpecializationName = dto.SpecializationName,
                Description = dto.Description,
                Status = dto.Status.ToString()
            };
        }

        //Mapper SpecializationUpdateDto
        public static void MapToSpecializationUpdateDto(this SpecializationUpdateDto updateDto, Specialization specialization)
        {
            if (!string.IsNullOrWhiteSpace(updateDto.SpecializationName))
                specialization.SpecializationName = updateDto.SpecializationName;

            if (!string.IsNullOrWhiteSpace(updateDto.Description))
                specialization.Description = updateDto.Description;

            if (updateDto.Status.HasValue)
                specialization.Status = updateDto.Status.ToString();
        }

        // Mapper SpecializationDeleteDto
        public static SpecializationDeleteDto MapToSpecializationDeleteDto(this Specialization model)
        {
            return new SpecializationDeleteDto
            {
                SpecializationName = model.SpecializationName,
                Description = model.Description,

                //Convert Status from string to enum
                Status = Enum.TryParse<SpecializationStatus>(model.Status, true, out var status)
                          ? status
                          : SpecializationStatus.Inactive,

                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt
            };
        }
    }
}
