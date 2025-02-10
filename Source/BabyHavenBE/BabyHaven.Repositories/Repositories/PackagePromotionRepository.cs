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
    public class PackagePromotionRepository : GenericRepository<PackagePromotion>
    {
        public PackagePromotionRepository()
        {

        }

        public PackagePromotionRepository(SWP391_ChildGrowthTrackingSystemContext context)
            => _context = context;

        public async Task<List<PackagePromotion>> GetAllPackagePromotionAsync()
        {
            var packagePromotions = await _context.PackagePromotions
                .Include(pf => pf.Package)
                .Include(pf => pf.Promotion)
                .ToListAsync();

            return packagePromotions;
        }

        public async Task<PackagePromotion> GetByIdPackagePromotionAsync(int packageId, Guid promotionId)
        {
            var packagePromotion = await _context.PackagePromotions
                .Include(pf => pf.Package)
                .Include(pf => pf.Promotion)
                .FirstOrDefaultAsync(pf => pf.PackageId == packageId && pf.PromotionId == promotionId);

            return packagePromotion;
        }
    }
}
