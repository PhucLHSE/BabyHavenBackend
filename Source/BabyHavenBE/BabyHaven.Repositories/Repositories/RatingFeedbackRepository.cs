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
    public class RatingFeedbackRepository:GenericRepository<RatingFeedback>
    {
        public RatingFeedbackRepository() { }
        public RatingFeedbackRepository(SWP391_ChildGrowthTrackingSystemContext context) => _context = context;

        public async Task<List<RatingFeedback>> GetAllRatingFeedbackAsync()
        {
            return await _context.RatingFeedbacks
                .Include(cr => cr.User)
                .Include(cr => cr.Response)
                .ToListAsync();
        }
        public async Task<RatingFeedback?> GetByIdRatingFeedbackAsync(int FeedbackId)
        {
            return await _context.RatingFeedbacks
                .Include(t => t.User)
                .Include(t => t.Response)
                    .ThenInclude(t => t.Request)   
                .FirstOrDefaultAsync(t => t.FeedbackId == FeedbackId);
        }
    }
}
