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
    public class PromotionRepository : GenericRepository<Promotion>
    {
        public PromotionRepository()
        {

        }

        public PromotionRepository(SWP391_ChildGrowthTrackingSystemContext context)
            => _context = context;

        public async Task<Promotion?> GetByIdPromotionAsync(Guid promotionId)
        {
            return await _context.Promotions
                .Include(p => p.CreatedByNavigation) 
                .Include(p => p.ModifiedByNavigation) 
                .FirstOrDefaultAsync(p => p.PromotionId == promotionId);
        }
    }
}
