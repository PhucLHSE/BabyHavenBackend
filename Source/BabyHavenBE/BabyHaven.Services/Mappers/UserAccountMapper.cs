﻿using BabyHaven.Common.DTOs.FeatureDTOs;
using BabyHaven.Common.DTOs.UserAccountDTOs;
using BabyHaven.Common.Enum.FeatureEnums;
using BabyHaven.Common.Enum.UserAccountEnums;
using BabyHaven.Repositories.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.Mappers
{
    public static class UserAccountMapper
    {
        // Mapper UserAccountViewAllDto
        public static UserAccountViewAllDto MapToUserAccountViewAllDto(this UserAccount model)
        {
            return new UserAccountViewAllDto
            {
                Username = model.Username,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Name = model.Name,
                Gender = model.Gender,
                DateOfBirth = model.DateOfBirth,
                Address = model.Address,
                ProfilePicture = model.ProfilePicture,
                IsVerified = model.IsVerified,
                RoleName = model.Role?.RoleName ?? string.Empty,


                // Convert Status from string to enum
                Status = Enum.TryParse<UserAccountStatus>(model.Status, true, out var status)
                          ? status
                          : UserAccountStatus.Inactive
            };
        }

        // Mapper UserAccountViewDetailsDto
        public static UserAccountViewDetailsDto MapToUserAccountViewDetailsDto(this UserAccount model)
        {
            return new UserAccountViewDetailsDto
            {
                Username = model.Username,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Name = model.Name,
                Gender = model.Gender,
                DateOfBirth = model.DateOfBirth,
                Address = model.Address,
                RegistrationDate = model.RegistrationDate,
                LastLogin = model.LastLogin,
                ProfilePicture = model.ProfilePicture,
                VerificationCode = model.VerificationCode,
                IsVerified = model.IsVerified,
                RoleName = model.Role?.RoleName ?? string.Empty,

                //Convert Status from string to enum
                Status = Enum.TryParse<UserAccountStatus>(model.Status, true, out var status)
                          ? status
                          : UserAccountStatus.Inactive,

                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt
            };
        }

        //Mapper UserAccountCreateDto
        public static UserAccount MapToUserAccount(this UserAccountCreateDto dto)
        {
            return new UserAccount
            {
                Username = dto.Username,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Name = dto.Name,
                Gender = dto.Gender,
                DateOfBirth = dto.DateOfBirth,
                Address = dto.Address,
                Password = dto.Password,
                RoleId = dto.RoleId,
                Status = dto.Status.ToString(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        //Mapper UserAccountUpdateDto
        public static void MapToUserAccountUpdateDto(this UserAccountUpdateDto updateDto, UserAccount userAccount)
        {
            if (!string.IsNullOrWhiteSpace(updateDto.Username))
                userAccount.Username = updateDto.Username;

            if (!string.IsNullOrWhiteSpace(updateDto.Email))
                userAccount.Email = updateDto.Email;

            if (!string.IsNullOrWhiteSpace(updateDto.PhoneNumber))
                userAccount.PhoneNumber = updateDto.PhoneNumber;

            if (!string.IsNullOrWhiteSpace(updateDto.Name))
                userAccount.Name = updateDto.Name;

            if (!string.IsNullOrWhiteSpace(updateDto.Gender))
                userAccount.Gender = updateDto.Gender;

            if (updateDto.DateOfBirth.HasValue)
                userAccount.DateOfBirth = updateDto.DateOfBirth.Value;

            if (!string.IsNullOrWhiteSpace(updateDto.Address))
                userAccount.Address = updateDto.Address;

            if (!string.IsNullOrWhiteSpace(updateDto.Password))
                userAccount.Password = updateDto.Password;

            if (updateDto.ProfilePicture != null)
                userAccount.ProfilePicture = updateDto.ProfilePicture;

            if (updateDto.Status.HasValue)
                userAccount.Status = updateDto.Status.ToString();
        }

        // Mapper UserAccountDeleteDto
        public static UserAccountDeleteDto MapToUserAccountDeleteDto(this UserAccount model)
        {
            return new UserAccountDeleteDto
            {
                Username = model.Username,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Name = model.Name,
                Gender = model.Gender,
                DateOfBirth = model.DateOfBirth,
                Address = model.Address,
                ProfilePicture = model.ProfilePicture,
                RoleId = model.RoleId,


                //Convert Status from string to enum
                Status = Enum.TryParse<UserAccountStatus>(model.Status, true, out var status)
                          ? status
                          : UserAccountStatus.Inactive,

                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt
            };
        }
        public static LoginDto MapToLoginDto(this LoginDto dto)
        {
            return new LoginDto
            {
                Email = dto.Email,
                Password = dto.Password
            };
        }
    }
}
