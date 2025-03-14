using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.IServices
{
    public interface IAuthService
    {
        Task<bool> SendOtpAsync(string email);
        Task<bool> VerifyOtpAsync(string email, string otp);
        Task<bool> SendResetPasswordOtpAsync(string email); // Cập nhật tên cho rõ ràng
        Task<bool> VerifyResetPasswordOtpAsync(string email, string otp); // Tách riêng API kiểm tra OTP
        Task<bool> ResetPasswordAsync(string email, string newPassword);
        Task MarkOtpVerified(string email); // ✅ Đánh dấu OTP đã xác thực
        Task<bool> IsOtpVerified(string email); // ✅ Kiểm tra trạng thái OTP
    }
}
