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
    public class MembershipPackageRepository : GenericRepository<MembershipPackage>
    {
        public MembershipPackageRepository() 
        { 

        }

        public MembershipPackageRepository(SWP391_ChildGrowthTrackingSystemContext context)
            => _context = context;

        public async Task<MembershipPackage?> GetByPackageNameAsync(string packageName)
        {
            return await _context.MembershipPackages
                .FirstOrDefaultAsync(mp => mp.PackageName == packageName);
        }

        public async Task<Dictionary<string, int>> GetAllPackageNameToIdMappingAsync()
        {
            return await _context.MembershipPackages
                .ToDictionaryAsync(mp => mp.PackageName, p => p.PackageId);
        }
    }
}