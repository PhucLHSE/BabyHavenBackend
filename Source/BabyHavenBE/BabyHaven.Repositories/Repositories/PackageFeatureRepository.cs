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
    public class PackageFeatureRepository : GenericRepository<PackageFeature>
    {
        public PackageFeatureRepository()
        {

        }

        public PackageFeatureRepository(SWP391_ChildGrowthTrackingSystemContext context) 
            => _context = context;

        public async Task<List<PackageFeature>> GetAllPackageFeatureAsync()
        {
            var packageFeatures = await _context.PackageFeatures
                .Include(pf => pf.Package)
                .Include(pf => pf.Feature)
                .ToListAsync();

            return packageFeatures;
        }

        public async Task<PackageFeature> GetByIdPackageFeatureAsync(int packageId, int featureId)
        {
            var packageFeature = await _context.PackageFeatures
                .Include(pf => pf.Package)
                .Include(pf => pf.Feature)
                .FirstOrDefaultAsync(pf => pf.PackageId == packageId && pf.FeatureId == featureId);

            return packageFeature;
        }
    }
}
