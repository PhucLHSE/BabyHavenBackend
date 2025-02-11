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

        public async Task<Promotion?> GetByPromotionCodeAsync(string promotionCode)
        {
            return await _context.Promotions
                .FirstOrDefaultAsync(p => p.PromotionCode == promotionCode);
        }

        public async Task<int> UpdatePromotionAsync(Promotion promotion)
        {
            _context.Promotions.Attach(promotion);
            _context.Entry(promotion).State = EntityState.Modified;

            int result = await _context.SaveChangesAsync();

            // Turn off tracking immediately after update
            _context.Entry(promotion).State = EntityState.Detached;
            return result;
        }

        public async Task<Dictionary<string, Guid>> GetAllPromotionCodeToIdMappingAsync()
        {
            return await _context.Promotions
                .ToDictionaryAsync(p => p.PromotionCode, p => p.PromotionId);
        }
    }
}
