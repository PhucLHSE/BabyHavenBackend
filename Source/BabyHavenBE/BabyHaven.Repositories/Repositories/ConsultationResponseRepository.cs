using Azure;
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
    public class ConsultationResponseRepository : GenericRepository<ConsultationResponse>
    {
        public ConsultationResponseRepository()
        {

        }

        public ConsultationResponseRepository(SWP391_ChildGrowthTrackingSystemContext context)
            => _context = context;

        public async Task<List<ConsultationResponse>> GetAllConsultationResponseAsync()
        {
            return await _context.ConsultationResponses
                .Include(cr => cr.Request)
                   .ThenInclude(cr => cr.Doctor)
                .ToListAsync();
        }

        public async Task<ConsultationResponse?> GetByIdConsultationResponseAsync(int responseId)
        {
            return await _context.ConsultationResponses
                .Include(cr => cr.Request)
                   .ThenInclude(cr => cr.Doctor)
                .FirstOrDefaultAsync(cr => cr.ResponseId == responseId);
        }

        public async Task<List<ConsultationResponse?>> GetByMemberIdConsultationResponseAsync(Guid memberId)
        {
            return await _context.ConsultationResponses
                .AsNoTracking()
                .Include(cr => cr.Request)
                    .ThenInclude(cr => cr.Doctor)
                .Where(cr => cr.Request.MemberId == memberId)
                .ToListAsync();
        }

        public async Task<ConsultationResponse?> GetByRequestId(int requestId)
        {
            return await _context.ConsultationResponses
                .Include(cr => cr.Request)
                .FirstOrDefaultAsync(cr => cr.RequestId == requestId);
        }
    }
}
