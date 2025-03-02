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
    public class ConsultationResponseRepository:GenericRepository<ConsultationResponse>
    {
        public ConsultationResponseRepository() { }
        public ConsultationResponseRepository(SWP391_ChildGrowthTrackingSystemContext context)
            => _context = context;

        public async Task<List<ConsultationResponse>> GetAllConsultationResponseAsync()
        {
            return await _context.ConsultationResponses
                .Include(cr => cr.Doctor)
                   .ThenInclude(cr => cr.User)
                .Include(cr => cr.Request)
                .ToListAsync();
        }

        public async Task<ConsultationResponse?> GetByIdConsultationResponseAsync(int responseId)
        {
            return await _context.ConsultationResponses
                .Include(cr => cr.Doctor)
                    .ThenInclude(m => m.User)
                .Include(cr => cr.Request)
                .FirstOrDefaultAsync(cr => cr.ResponseId == responseId);
        }
    }
}
