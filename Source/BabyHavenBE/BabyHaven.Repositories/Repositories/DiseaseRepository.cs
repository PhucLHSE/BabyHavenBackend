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
    public class DiseaseRepository : GenericRepository<Disease>
    {
        public DiseaseRepository()
        {

        }
        public DiseaseRepository(SWP391_ChildGrowthTrackingSystemContext context)
            => _context = context;
    }
}
