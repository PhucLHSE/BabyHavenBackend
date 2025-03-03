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
    public class ChildrenRepository : GenericRepository<Child>
    {
        public ChildrenRepository()
        {
        }

        public ChildrenRepository(SWP391_ChildGrowthTrackingSystemContext context)
            => _context = context;

        public async Task<List<Child>> GetChildrenByMemberIdAsync(Guid memberId)
        {
            return await _context.Children
                .Where(c => c.MemberId == memberId)
                .ToListAsync();
        }

        // Combine with GrowthRecords table to update the latest data
        public async Task<List<Child>> GetChildrenByMemberIdForNowAsync(Guid memberId)
        {
            return await _context.Children
                .Include(gr => gr.GrowthRecords)
                .Where(c => c.MemberId == memberId)
                .ToListAsync();
        }

        public async Task<Dictionary<string, Guid>> GetAllChildNameToIdMappingAsync()
        {
            return await _context.Children
                .ToDictionaryAsync(mp => mp.Name, p => p.ChildId);
        }

        //Get child by name, memberId and date of birth
        public async Task<Child?> GetChildByNameAndDateOfBirthAsync(string name, DateOnly dateOfBirth, Guid memberId)
        {
            return await _context.Children
                .FirstOrDefaultAsync(c => c.Name == name && c.DateOfBirth == dateOfBirth && c.MemberId == memberId);
        }
    }
}
