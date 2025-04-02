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
                .Select(cr => new ConsultationRequest
                {
                    RequestId = cr.RequestId,
                    MemberId = cr.MemberId,
                    DoctorId = cr.DoctorId,
                    RequestDate = cr.RequestDate,
                    Description = cr.Description,
                    Status = cr.Status,
                    Urgency = cr.Urgency,
                    Category = cr.Category,
                    Member = cr.Member == null ? null : new Member
                    {
                        MemberId = cr.Member.MemberId,
                        User = cr.Member.User == null ? null : new UserAccount
                        {
                            Name = cr.Member.User.Name
                        }
                    },
                    Child = cr.Child == null ? null : new Child
                    {
                        Name = cr.Child.Name
                    },
                })
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
