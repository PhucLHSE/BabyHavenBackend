using BabyHaven.Common.DTOs.FeatureDTOs;
using BabyHaven.Common.DTOs.RoleDTOs;
using BabyHaven.Common.Enum.FeatureEnums;
using BabyHaven.Common.Enum.RoleEnums;
using BabyHaven.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.Mappers
{
    public static class RoleMapper
    {
        // Mapper RoleViewAllDto
        public static RoleViewAllDto MapToRoleViewAllDto(this Role model)
        {
            return new RoleViewAllDto
            {
                // Role
                RoleName = model.RoleName,
                Description = model.Description,

                // Convert Status from string to enum
                Status = Enum.TryParse<RoleStatus>(model.Status, true, out var status)
                          ? status
                          : RoleStatus.Inactive
            };
        }

        // Mapper RoleViewDetailsDto
        public static RoleViewDetailsDto MapToRoleViewDetailsDto(this Role model)
        {
            return new RoleViewDetailsDto
            {
                //Role
                RoleName = model.RoleName,
                Description = model.Description,

                //Convert Status from string to enum
                Status = Enum.TryParse<RoleStatus>(model.Status, true, out var status)
                          ? status
                          : RoleStatus.Inactive,

                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt
            };
        }

        //Mapper RoleCreateDto
        public static Role MapToRoleCreateDto(this RoleCreateDto dto)
        {
            return new Role
            {
                RoleName = dto.RoleName,
                Description = dto.Description,
                Status = dto.Status.ToString()
            };
        }

        //Mapper RoleUpdateDto
        public static void MapToRoleUpdateDto(this RoleUpdateDto updateDto, Role role)
        {
            if (!string.IsNullOrWhiteSpace(updateDto.RoleName))
                role.RoleName = updateDto.RoleName;

            if (!string.IsNullOrWhiteSpace(updateDto.Description))
                role.Description = updateDto.Description;

            if (updateDto.Status.HasValue)
                role.Status = updateDto.Status.ToString();
        }

        // Mapper RoleDeleteDto
        public static RoleDeleteDto MapToRoleDeleteDto(this Role model)
        {
            return new RoleDeleteDto
            {
                RoleName = model.RoleName,
                Description = model.Description,

                //Convert Status from string to enum
                Status = Enum.TryParse<RoleStatus>(model.Status, true, out var status)
                          ? status
                          : RoleStatus.Inactive,

                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt
            };
        }
    }
}
