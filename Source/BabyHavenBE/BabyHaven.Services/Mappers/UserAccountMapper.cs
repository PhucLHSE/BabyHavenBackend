﻿using BabyHaven.Common.DTOs.FeatureDTOs;
using BabyHaven.Common.DTOs.UserAccountDTOs;
using BabyHaven.Common.Enum.FeatureEnums;
using BabyHaven.Common.Enum.UserAccountEnums;
using BabyHaven.Repositories.Models;
using Microsoft.EntityFrameworkCore.Storage.Json;
using NanoidDotNet;
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
                UserId = model.UserId,
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
                UserId = model.UserId,
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
            //DateOnly? dateOfBirth = null;
            //if (!string.IsNullOrWhiteSpace(dto.DateOfBirth))
            //{
            //    if (DateOnly.Parse(dto.DateOfBirth))
            //    {
            //        if (parsedDate >= DateOnly.MinValue && parsedDate <= DateOnly.MaxValue)
            //        {
            //            dateOfBirth = parsedDate;
            //        }
            //        else
            //        {
            //            throw new ArgumentOutOfRangeException(nameof(dto.DateOfBirth), "DateOfBirth must be between 1/1/1753 and 12/31/9999.");
            //        }
            //    }
            //    else
            //    {
            //        throw new FormatException("DateOfBirth is not in a valid format.");
            //    }
            //}

            return new UserAccount
            {
                Username = dto.Username,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Name = dto.Name,
                LastLogin = DateTime.UtcNow,
                RegistrationDate = DateTime.UtcNow,
                Gender = dto.Gender,
                DateOfBirth = DateOnly.Parse(dto.DateOfBirth),
                Address = dto.Address,
                Password = dto.Password,
                RoleId = dto.RoleId,
                Status = UserAccountStatus.Active.ToString(),
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

            if (!string.IsNullOrWhiteSpace(updateDto.DateOfBirth))
            {
                userAccount.DateOfBirth = DateOnly.Parse(updateDto.DateOfBirth);
            }

            
                userAccount.IsVerified = updateDto.IsVerified;

            if (!string.IsNullOrWhiteSpace(updateDto.Address))
                userAccount.Address = updateDto.Address;

            if (!string.IsNullOrWhiteSpace(updateDto.Password))
                userAccount.Password = updateDto.Password;

            if (updateDto.ProfilePicture != null)
                userAccount.ProfilePicture = updateDto.ProfilePicture;

            if (updateDto.Status.HasValue)
                userAccount.Status = updateDto.Status.ToString();

            userAccount.UpdatedAt = DateTime.UtcNow;
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

        public static UserAccount MapToGoogleUser(this LoginGoogleDto googleDto, byte[]? profilePicture)
        {
            return new UserAccount
            {
                Username = googleDto.Email.Split('@')[0],  // Lấy phần trước @ làm username
                Email = googleDto.Email,
                Name = googleDto.Name,
                PhoneNumber = Nanoid.Generate(size: 16), // Không có phone từ Google
                Gender = "Other",  // Google không cung cấp giới tính
                DateOfBirth = null, // Không có DOB từ Google
                Address = "N/A",
                Password = Guid.NewGuid().ToString(), // Dummy password
                ProfilePicture = profilePicture,
                Status = UserAccountStatus.Active.ToString(),
                RoleId = 1,
                RegistrationDate = DateTime.UtcNow,
                IsVerified = false
            };
        }

        public static ForgotPasswordDto MapToForgotPasswordDto(this UserAccount user)
        {
            return new ForgotPasswordDto
            {
                Email = user.Email
            };
        }

        public static VerifyOtpDto MapToVerifyOtpDto(this UserAccount user, string otp)
        {
            return new VerifyOtpDto
            {
                Email = user.Email,
                Otp = otp
            };
        }

        public static ResetPasswordDto MapToResetPasswordDto(this UserAccount user, string newPassword)
        {
            return new ResetPasswordDto
            {
                Email = user.Email,
                NewPassword = newPassword
            };
        }

    }
}
