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
    }
}
