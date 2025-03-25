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

            string subject = "[BabyHaven] Xác nhận đăng ký tài khoản";
            string body = $"<html><body style='font-family:Arial, sans-serif; color:#333; text-align:center;'>"
                        + "<div style='max-width:600px; margin:auto; padding:20px; border-radius:10px; background:#f9f9f9; box-shadow:0 0 10px rgba(0,0,0,0.1);'>"
                        + "<img src='https://i.pinimg.com/736x/5a/99/6a/5a996a8df2a8ea9452ea11da2160df80.jpg' alt='BabyHaven Logo' style='width:120px; height:120px; border-radius:50%; margin-bottom:20px;'>"
                        + "<h2 style='color:#00d0bc;'>Chào mừng bạn đến với BabyHaven!</h2>"
                        + "<p>Cảm ơn bạn đã đăng ký tài khoản tại <b>BabyHaven</b>. Để hoàn tất quá trình đăng ký, vui lòng sử dụng mã OTP bên dưới:</p>"
                        + "<h3 style='color:#00d0bc; font-size:24px; background:#e0f7f5; display:inline-block; padding:10px 20px; border-radius:5px;'><b>" + otp + "</b></h3>"
                        + "<p><i>Lưu ý: Mã OTP này có hiệu lực trong 5 phút. Vui lòng không chia sẻ với bất kỳ ai.</i></p>"
                        + "<br><p>Trân trọng,<br><b>Đội ngũ BabyHaven</b></p>"
                        + "</div></body></html>";

            await _emailService.SendEmailAsync(email, subject, body);
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

            string subject = "[BabyHaven] Xác nhận đặt lại mật khẩu";
            string body = $"<html><body style='font-family:Arial, sans-serif; color:#333; text-align:center;'>"
                        + "<div style='max-width:600px; margin:auto; padding:20px; border-radius:10px; background:#f9f9f9; box-shadow:0 0 10px rgba(0,0,0,0.1);'>"
                        + "<img src='https://i.pinimg.com/736x/5a/99/6a/5a996a8df2a8ea9452ea11da2160df80.jpg' alt='BabyHaven Logo' style='width:120px; height:120px; border-radius:50%; margin-bottom:20px;'>"
                        + "<h2 style='color:#00d0bc;'>Xin chào,</h2>"
                        + "<p>Chúng tôi nhận được yêu cầu đặt lại mật khẩu cho tài khoản của bạn. Để tiếp tục, vui lòng sử dụng mã OTP bên dưới:</p>"
                        + "<h3 style='color:#00d0bc; font-size:24px; background:#e0f7f5; display:inline-block; padding:10px 20px; border-radius:5px;'><b>" + otp + "</b></h3>"
                        + "<p><i>Mã OTP này có hiệu lực trong 5 phút. Nếu bạn không yêu cầu đặt lại mật khẩu, vui lòng bỏ qua email này.</i></p>"
                        + "<br><p>Trân trọng,<br><b>Đội ngũ BabyHaven</b></p>"
                        + "</div></body></html>";
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
