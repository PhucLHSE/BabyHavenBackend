﻿using BabyHaven.Repositories.Base;
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

        public async Task<UserAccount?> GetByEmailAsync(string email)
        {
            return await _context.UserAccounts.FirstOrDefaultAsync(u => u.Email == email);
        }

        //public async Task<UserAccount> CreateGoogleUserAsync(UserAccount userAccount)
        //{
        //    await _context.UserAccounts.AddAsync(userAccount);
        //    await _context.SaveChangesAsync();
        //    return userAccount;
        //}

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
    }
}
