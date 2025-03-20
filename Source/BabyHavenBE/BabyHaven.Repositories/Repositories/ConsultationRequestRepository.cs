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

        public async Task<ConsultationRequest?> GetByIdConsultationRequestAsync(int requestId)
        {
            return await _context.ConsultationRequests
                .Include(cr => cr.Member)
                    .ThenInclude(m => m.User)
                .Include(cr => cr.Child)
                .FirstOrDefaultAsync(cr => cr.RequestId == requestId);
        }

        public async Task<ConsultationRequest?> GetByRequestId(int requestId)
        {
            return await _context.ConsultationRequests
                .Include(cr => cr.ConsultationResponse)
                .FirstOrDefaultAsync(cr => cr.RequestId == requestId);
        }
    }
}
