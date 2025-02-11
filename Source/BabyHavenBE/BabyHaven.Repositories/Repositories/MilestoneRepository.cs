using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Repositories.Base;
using BabyHaven.Repositories.DBContext;
using BabyHaven.Repositories.Models;

namespace BabyHaven.Repositories.Repositories
{
    public class MilestoneRepository : GenericRepository<Milestone>
    {
        public MilestoneRepository()
        {
        }

        public MilestoneRepository(SWP391_ChildGrowthTrackingSystemContext context)
            => _context = context;
    }
}
