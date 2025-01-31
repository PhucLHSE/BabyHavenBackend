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
    public class RoleRepository: GenericRepository<Role>
    {
        public RoleRepository()
        {

        }

        public RoleRepository(SWP391_ChildGrowthTrackingSystemContext context)
            => _context = context;
        public async Task<Role?> GetByRoleNameAsync(string roleName)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(r => r.RoleName == roleName);
        }

        public async Task<Dictionary<string, int>> GetAllRoleNameToIdMappingAsync()
        {
            return await _context.Roles
                .ToDictionaryAsync(r => r.RoleName, r => r.RoleId);
        }
    }
}
