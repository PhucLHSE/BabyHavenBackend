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
    public class FeatureRepository : GenericRepository<Feature>
    {
        public FeatureRepository()
        {

        }

        public FeatureRepository(SWP391_ChildGrowthTrackingSystemContext context)
            => _context = context;

        public async Task<Feature?> GetByFeatureNameAsync(string featureName)
        {
            return await _context.Features
                .FirstOrDefaultAsync(f => f.FeatureName == featureName);
        }

        public async Task<Dictionary<string, int>> GetAllFeatureNameToIdMappingAsync()
        {
            return await _context.Features
                .ToDictionaryAsync(f => f.FeatureName, f => f.FeatureId);
        }
    }
}
