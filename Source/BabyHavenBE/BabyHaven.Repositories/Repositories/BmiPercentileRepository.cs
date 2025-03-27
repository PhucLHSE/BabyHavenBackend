using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Repositories.Base;
using BabyHaven.Repositories.DBContext;
using BabyHaven.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace BabyHaven.Repositories.Repositories
{
    public class BmiPercentileRepository : GenericRepository<BmiPercentile>
    {
        public BmiPercentileRepository()
        {
        }

        public BmiPercentileRepository(SWP391_ChildGrowthTrackingSystemContext context)
            => _context = context;

        public async Task<BmiPercentile> GetByAgeAndGender(int age, string gender)
        {
            return await _context.BmiPercentiles
                .FirstOrDefaultAsync(bp => bp.Age == age && bp.Gender == gender);
        }
    }
}
