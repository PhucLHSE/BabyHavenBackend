using BabyHaven.Common.DTOs.FeatureDTOs;
using BabyHaven.Common;
using BabyHaven.Repositories;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common.DTOs.RoleDTOs;
using BabyHaven.Services.Mappers;

namespace BabyHaven.Services.Services
{
    public class RoleService : IRoleService
    {
        private readonly UnitOfWork _unitOfWork;

        public RoleService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IServiceResult> GetAll()
        {
            var roles = await _unitOfWork.RoleRepository.GetAllAsync();

            if (roles == null || !roles.Any())
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE,
                    Const.WARNING_NO_DATA_MSG,
                    new List<RoleViewAllDto>());
            }
            else
            {
                var roleDtos = roles
                    .Select(roles => roles.MapToRoleViewAllDto())
                    .ToList();

                return new ServiceResult(Const.SUCCESS_READ_CODE,
                    Const.SUCCESS_READ_MSG,
                    roleDtos);
            }
        }

        public async Task<IServiceResult> GetById(int RoleId)
        {
            var role = await _unitOfWork.RoleRepository.GetByIdAsync(RoleId);

            if (role == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE,
                    Const.WARNING_NO_DATA_MSG,
                    new RoleViewDetailsDto());
            }
            else
            {
                var roleDto = role.MapToRoleViewDetailsDto();

                return new ServiceResult(Const.SUCCESS_READ_CODE,
                    Const.SUCCESS_READ_MSG,
                    roleDto);
            }
        }

        public async Task<IServiceResult> Create(RoleCreateDto roleDto)
        {
            try
            {
                // Check if the role exists in the database
                var role = await _unitOfWork.RoleRepository.GetByRoleNameAsync(roleDto.RoleName);

                if (role != null)
                {
                    return new ServiceResult(Const.FAIL_CREATE_CODE,
                        "Role with the same name already exists.");
                }

                // Map DTO to Entity
                var newRole = roleDto.MapToRoleCreateDto();

                // Add creation and update time information
                newRole.CreatedAt = DateTime.UtcNow;
                newRole.UpdatedAt = DateTime.UtcNow;

                // Save data to database
                var result = await _unitOfWork.RoleRepository.CreateAsync(newRole);

                if (result > 0)
                {
                    return new ServiceResult(Const.
                        SUCCESS_CREATE_CODE,
                        Const.
                        SUCCESS_CREATE_MSG,
                        newRole);
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

        public async Task<IServiceResult> Update(RoleUpdateDto roleDto)
        {
            try
            {
                // Check if the role exists in the database
                var role = await _unitOfWork.RoleRepository.GetByIdAsync(roleDto.RoleId);

                if (role == null)
                {
                    return new ServiceResult(Const.
                        FAIL_UPDATE_CODE,
                        "Role not found.");
                }

                //Map DTO to Entity
                roleDto.MapToRoleUpdateDto(role);

                // Update time information
                role.UpdatedAt = DateTime.UtcNow;

                // Save data to database
                var result = await _unitOfWork.RoleRepository.UpdateAsync(role);

                if (result > 0)
                {
                    return new ServiceResult(Const.
                        SUCCESS_UPDATE_CODE,
                        Const.
                        SUCCESS_UPDATE_MSG,
                        role);
                }
                else
                {
                    return new ServiceResult(Const.
                        FAIL_UPDATE_CODE,
                        Const.
                        FAIL_UPDATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.
                    ERROR_EXCEPTION,
                    ex.
                    ToString());
            }
        }

        public async Task<IServiceResult> DeleteById(int RoleId)
        {
            try
            {
                var role = await _unitOfWork.RoleRepository.GetByIdAsync(RoleId);

                if (role == null)
                {
                    return new ServiceResult(Const.
                        WARNING_NO_DATA_CODE,
                        Const.
                        WARNING_NO_DATA_MSG,
                        new RoleDeleteDto());
                }
                else
                {
                    var deleteRoleDto = role.MapToRoleDeleteDto();

                    var result = await _unitOfWork.RoleRepository.RemoveAsync(role);

                    if (result)
                    {
                        return new ServiceResult(Const.
                            SUCCESS_DELETE_CODE,
                            Const.
                            SUCCESS_DELETE_MSG,
                            deleteRoleDto);
                    }
                    else
                    {
                        return new ServiceResult(Const.
                            FAIL_DELETE_CODE,
                            Const.
                            FAIL_DELETE_MSG,
                            deleteRoleDto);
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
