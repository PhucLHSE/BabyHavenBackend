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
using BabyHaven.Common.DTOs.UserAccountDTOs;
using BabyHaven.Services.Mappers;

namespace BabyHaven.Services.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IJwtTokenService _jwtTokenService;

        public UserAccountService(UnitOfWork unitOfWork, IJwtTokenService jwtTokenService)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<UserAccount> Authenticate(string email, string password)
        {
            return await _unitOfWork.UserAccountRepository.GetUserAccount(email, password);
        }

        public async Task<IServiceResult> AuthenticateWithGoogle(LoginGoogleDto googleDto)
        {
            var user = await _unitOfWork.UserAccountRepository.GetByEmailAsync(googleDto.Email);

            if (user == null)
            {
                user = await CreateGoogleUserAsync(googleDto);
            }

            return new ServiceResult(Const.SUCCESS_LOGIN_CODE, Const.SUCCESS_LOGIN_GOOGLE_MSG, user);
        }

        private async Task<UserAccount> CreateGoogleUserAsync(LoginGoogleDto googleDto)
        {
            byte[]? profilePicture = googleDto.ProfilePictureUrl != null
                ? await _unitOfWork.UserAccountRepository.DownloadImageAsByteArray(googleDto.ProfilePictureUrl)
                : Array.Empty<byte>();

            var user = googleDto.MapToGoogleUser(profilePicture);
            await _unitOfWork.UserAccountRepository.CreateAsync(user);


            return user;
        }
    }
}
