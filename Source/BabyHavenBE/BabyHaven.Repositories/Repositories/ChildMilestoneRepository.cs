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
    public class ChildMilestoneRepository : GenericRepository<ChildMilestone>
    {
        public ChildMilestoneRepository()
        {
        }

        public ChildMilestoneRepository(SWP391_ChildGrowthTrackingSystemContext context)
            => _context = context;

        public async Task<ChildMilestone> GetByIdAsync(Guid childId, int milestoneId)
        {
            return await _context.ChildMilestones
                .FirstOrDefaultAsync(cm => cm.ChildId == childId && cm.MilestoneId == milestoneId);
        }

        public async Task<int> CreateAsync(ChildMilestone childMilestone, Guid childId, int milestoneId)
        {
            childMilestone.ChildId = childId;
            childMilestone.MilestoneId = milestoneId;
            _context.ChildMilestones.Add(childMilestone);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<ChildMilestone>> GetByChild(Guid childId)
        {
            return await _context.ChildMilestones
                .Include(cm => cm.Milestone)
                .Where(cm => cm.ChildId == childId)
                .ToListAsync();
        }
    }
}
