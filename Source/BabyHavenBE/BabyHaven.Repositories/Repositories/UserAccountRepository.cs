using BabyHaven.Repositories.Base;
using BabyHaven.Repositories.DBContext;
using BabyHaven.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Repositories.Repositories
{
    public class UserAccountRepository : GenericRepository<UserAccount>
    {
        public UserAccountRepository() { }
        public UserAccountRepository(SWP391_ChildGrowthTrackingSystemContext context)
            => _context = context;
        public async Task<UserAccount?> GetByUserNameAsync(string userName)
        {
            return await _context.UserAccounts
                .FirstOrDefaultAsync(u => u.Username == userName);
        }

        public async Task<Dictionary<string, Guid>> GetAllUserNameToIdMappingAsync()
        {
            return await _context.UserAccounts
                .ToDictionaryAsync(u => u.Username, u => u.UserId);
        }
        public async Task<Dictionary<string, Guid>> GetAllNameToIdMappingAsync()
        {
            return await _context.UserAccounts
                .ToDictionaryAsync(u => u.Email, u => u.UserId);
        }

        public async Task<IEnumerable<UserAccount>> GetAllWithRolesAsync()
        {
            return await _context.UserAccounts
                .Include(u => u.Role)
                .ToListAsync();
        }

        public async Task<UserAccount> GetUserAccount(string email, string password)
        {
            return await _context.UserAccounts.FirstOrDefaultAsync(u => u.Email == email && u.Password == password && u.Status == "Active");
        }
        public async Task<UserAccount> GetByIdWithRolesAsync(Guid userId)
        {
            return await _context.UserAccounts
                .Include(u => u.Role)
                .FirstOrDefaultAsync( u => u.UserId == userId);
        }
        public async Task<UserAccount?> GetByEmailAsync(string email)
        {
            return await _context.UserAccounts.FirstOrDefaultAsync(u => u.Email == email);
        }
        
        public async Task<byte[]?> DownloadImageAsByteArray(string imageUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    return await client.GetByteArrayAsync(imageUrl);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public async Task<UserAccount?> GetByVerificationCodeAsync(string code)
        {
            return await _context.UserAccounts.FirstOrDefaultAsync(u => u.VerificationCode == code);
        }

        public async Task UpdateVerificationCodeAsync(Guid userId, string code)
        {
            var user = await _context.UserAccounts.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user != null)
            {
                user.VerificationCode = code;
                user.IsVerified = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task VerifyAccountAsync(Guid userId)
        {
            var user = await _context.UserAccounts.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user != null)
            {
                user.IsVerified = true;
                user.VerificationCode = null;
                await _context.SaveChangesAsync();
            }
        }

        public async Task ResetPasswordAsync(Guid userId, string newPassword)
        {
            var user = await _context.UserAccounts.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user != null)
            {
                user.Password = newPassword;
                user.VerificationCode = null;
                await _context.SaveChangesAsync();
            }
        }

    }
}
