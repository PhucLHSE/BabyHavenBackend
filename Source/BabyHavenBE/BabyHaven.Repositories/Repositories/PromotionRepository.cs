using BabyHaven.Repositories.Base;
using BabyHaven.Repositories.DBContext;
using BabyHaven.Repositories.Models;
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
    }
}
