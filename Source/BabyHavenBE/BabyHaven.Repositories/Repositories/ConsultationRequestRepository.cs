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
    public class ConsultationRequestRepository : GenericRepository<ConsultationRequest>
    {
        public ConsultationRequestRepository()
        {

        }

        public ConsultationRequestRepository(SWP391_ChildGrowthTrackingSystemContext context)
            => _context = context; 

        public async Task<List<ConsultationRequest>> GetAllConsultationRequestAsync()
        {
            return await _context.ConsultationRequests
                .Include(cr => cr.Member)
                   .ThenInclude(cr => cr.User)
                .Include(cr => cr.Child)
                .ToListAsync();
        }
    }
}
