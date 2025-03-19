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

        // Cập nhật tên cho rõ ràng
        Task<bool> SendResetPasswordOtpAsync(string email);

        // Tách riêng API kiểm tra OTP
        Task<bool> VerifyResetPasswordOtpAsync(string email, string otp); 

        Task<bool> ResetPasswordAsync(string email, string newPassword);

        // Đánh dấu OTP đã xác thực
        Task MarkOtpVerified(string email);

        // Kiểm tra trạng thái OTP
        Task<bool> IsOtpVerified(string email); 
    }
}
