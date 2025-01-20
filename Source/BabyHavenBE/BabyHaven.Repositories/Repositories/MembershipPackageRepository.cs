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
    public class MembershipPackageRepository : GenericRepository<MembershipPackage>
    {
        public MembershipPackageRepository() 
        { 

        }

        public MembershipPackageRepository(SWP391_ChildGrowthTrackingSystemContext context)
            => _context = context;

        public async Task<List<MembershipPackage>> GetAllMembershipPackageAsync()
        {
            var membershipPackages = await _context.MembershipPackages.
                Include(mp => mp.Promotion).ToListAsync();

            return membershipPackages;
        }

        public async Task<MembershipPackage> GetByIdMembershipPackageAsync(int PackegeID)
        {
            var membershipPackage = await _context.MembershipPackages
                .Include(mp => mp.Promotion)
                .Include(mp => mp.PackageFeatures)
                   .ThenInclude(pf => pf.Feature)
                .FirstOrDefaultAsync(mp => mp.PackageId == PackegeID);

            return membershipPackage;
        }
    }
}