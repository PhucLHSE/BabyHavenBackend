using BabyHaven.Repositories;
using BabyHaven.Repositories.Models;
using BabyHaven.Services.IServices;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private static readonly ConcurrentDictionary<string, (string Otp, DateTime Expiry)> _otpStorage = new();
        private static readonly ConcurrentDictionary<string, bool> _verifiedEmails = new();
        public AuthService(UnitOfWork unitOfWork, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public async Task<bool> SendOtpAsync(string email)
        {
            var user = await _unitOfWork.UserAccountRepository.GetByEmailAsync(email);
            if (user != null) return false; // Email đã tồn tại

            string otp = new Random().Next(100000, 999999).ToString();
            _otpStorage[email] = (otp, DateTime.UtcNow.AddMinutes(5));

            await _emailService.SendEmailAsync(email, "OTP for Registration", $"Your OTP is: {otp}");
            return true;
        }

        public async Task<bool> VerifyOtpAsync(string email, string otp)
        {
            if (_otpStorage.TryGetValue(email, out var storedOtp) && storedOtp.Otp == otp && storedOtp.Expiry > DateTime.UtcNow)
            {
                _otpStorage.Remove(email, out _);
                return true;
            }
            return false;
        }

        public async Task<bool> SendResetPasswordOtpAsync(string email)
        {
            var user = await _unitOfWork.UserAccountRepository.GetByEmailAsync(email);
            if (user == null) return false;

            string otp = new Random().Next(100000, 999999).ToString();
            _otpStorage[email] = (otp, DateTime.UtcNow.AddMinutes(5));

            await _emailService.SendEmailAsync(email, "OTP for Password Reset", $"Your OTP is: {otp}");
            return true;
        }

        public async Task<bool> VerifyResetPasswordOtpAsync(string email, string otp)
        {
            return _otpStorage.TryGetValue(email, out var storedOtp)
                && storedOtp.Otp == otp
                && storedOtp.Expiry > DateTime.UtcNow;
        }

        public async Task<bool> ResetPasswordAsync(string email, string newPassword)
        {
            var user = await _unitOfWork.UserAccountRepository.GetByEmailAsync(email);
            if (user == null) return false;

            user.Password = newPassword;
            await _unitOfWork.UserAccountRepository.UpdateAsync(user);
            _otpStorage.Remove(email, out _);

            return true;
        }


        public async Task<bool> IsOtpVerified(string email)
        {
            return _verifiedEmails.TryGetValue(email, out var isVerified) && isVerified;
        }

        public async Task MarkOtpVerified(string email)
        {
            _verifiedEmails[email] = true;
        }


    }

}
