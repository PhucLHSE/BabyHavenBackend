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
    public class AlertRepository : GenericRepository<Alert>
    {
        public AlertRepository()
        {
        }

        public AlertRepository(SWP391_ChildGrowthTrackingSystemContext context)
            => _context = context;
    }
}
