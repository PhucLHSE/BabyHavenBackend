using BabyHaven.Repositories.Models;
using BabyHaven.Repositories;
using BabyHaven.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Repositories.Repositories;
using BabyHaven.Common;
using BabyHaven.Services.Base;
using BabyHaven.Services.Mappers;
using BabyHaven.Common.DTOs.UserAccountDTOs;

namespace BabyHaven.Services.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly UserAccountRepository _repository;

        public UserAccountService(UnitOfWork unitOfWork, UserAccountRepository accountRepository)
        {
            _unitOfWork = unitOfWork;
            _repository = accountRepository;
        }

        public async Task<UserAccount> Authenticate(string email, string password)
        {
            return await _repository.GetUserAccount(email, password);
        }
        public async Task<IServiceResult> GetAll()
        {
            var users = await _unitOfWork.UserAccountRepository.GetAllWithRolesAsync();

            if (users == null || !users.Any())
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG,
                    new List<UserAccountViewAllDto>());
            }
            else
            {
                var userDtos = users
                    .Select(user => user.MapToUserAccountViewAllDto())
                    .ToList();

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG,
                    userDtos);
            }
        }

        public async Task<IServiceResult> GetById(Guid UserId)
        {
            var user = await _unitOfWork.UserAccountRepository.GetByIdWithRolesAsync(UserId);

            if (user == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG,
                    new UserAccountViewDetailsDto());
            }
            else
            {
                var userDto = user.MapToUserAccountViewDetailsDto();

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG,
                    userDto);
            }
        }



        public async Task<IServiceResult> Update(UserAccountUpdateDto userAccountDto)
        {
            try
            {
                // Check if the package exists in the database
                var user = await _unitOfWork.UserAccountRepository.GetByIdAsync(userAccountDto.UserId);

                if (user == null)
                {
                    return new ServiceResult(Const.FAIL_UPDATE_CODE, "User not found.");
                }

                //Map DTO to Entity
                userAccountDto.MapToUserAccountUpdateDto(user);

                // Update time information
                user.UpdatedAt = DateTime.UtcNow;

                // Save data to database
                var result = await _unitOfWork.UserAccountRepository.UpdateAsync(user);

                if (result > 0)
                {
                    return new ServiceResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG,
                        user);
                }
                else
                {
                    return new ServiceResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IServiceResult> DeleteById(Guid UserId)
        {
            try
            {
                var user = await _unitOfWork.UserAccountRepository.GetByIdAsync(UserId);

                if (user == null)
                {
                    return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG,
                        new UserAccountDeleteDto());
                }
                else
                {
                    var deleteUserAccountDto = user.MapToUserAccountDeleteDto();

                    var result = await _unitOfWork.UserAccountRepository.RemoveAsync(user);

                    if (result)
                    {
                        return new ServiceResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG,
                            deleteUserAccountDto);
                    }
                    else
                    {
                        return new ServiceResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG,
                            deleteUserAccountDto);
                    }
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }
        public async Task<UserAccount?> GetByEmailAsync(string email)
        {
            return await _repository.GetByEmailAsync(email);
        }

        public async Task<bool> CreateAsync(UserAccount userAccount)
        {
            await _repository.CreateAsync(userAccount);
            return true;
        }

    }
}
