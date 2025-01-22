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
    public class FeatureRepository : GenericRepository<Feature>
    {
        public FeatureRepository()
        {

        }

        public FeatureRepository(SWP391_ChildGrowthTrackingSystemContext context)
            => _context = context;
    }
}
