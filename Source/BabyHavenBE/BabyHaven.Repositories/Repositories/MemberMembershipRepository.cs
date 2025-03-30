using BabyHaven.Common.Enum.MemberMembershipEnums;
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
    public class MemberMembershipRepository : GenericRepository<MemberMembership>
    {
        public MemberMembershipRepository()
        {

        }

        public MemberMembershipRepository(SWP391_ChildGrowthTrackingSystemContext context)
            => _context = context;

        public async Task<List<MemberMembership>> GetAllMemberMembershipAsync()
        {
            var memberMemberships = await _context.MemberMemberships
                .Include(mm => mm.Member)
                   .ThenInclude(m => m.User) // Include User from Member
                .Include(mm => mm.Package)
                .ToListAsync();

            return memberMemberships;
        }

        public async Task<List<MemberMembership>> GetAllOldByMemberIdAsync(Guid memberId)
        {
            var memberMemberships = await _context.MemberMemberships
                .Where(mm => mm.MemberId == memberId && mm.Status == "Active")
                .OrderByDescending(mm => mm.CreatedAt) // Sort by CreatedAt in descending order
                .Skip(1) // Exclude the first membership
                .ToListAsync();

            return memberMemberships;
        }

        public async Task<MemberMembership?> GetByIdMemberMembershipAsync(Guid memberMembershipId)
        {
            return await _context.MemberMemberships
                .Include(mm => mm.Member)
                   .ThenInclude(m => m.User) // Include User from Member
                .Include(mm => mm.Package)
                .FirstOrDefaultAsync(mm => mm.MemberMembershipId == memberMembershipId);
        }

        public async Task<MemberMembership?> GetByMemberId(Guid memberId)
        {
            return await _context.MemberMemberships
                .Include(mm => mm.Member)
                   .ThenInclude(m => m.User) // Include User from Member
                .Include(mm => mm.Package)
                .FirstOrDefaultAsync(mm => mm.MemberId == memberId);
        }

        public async Task<bool> HasActiveMembershipAsync(Guid memberId, int packageId)
        {
            return await _context.MemberMemberships
                .AnyAsync(mm => mm.MemberId == memberId
                             && mm.PackageId == packageId
                             && mm.Status == MemberMembershipStatus.Active.ToString()
                             && mm.IsActive == true);
        }
    }
}
