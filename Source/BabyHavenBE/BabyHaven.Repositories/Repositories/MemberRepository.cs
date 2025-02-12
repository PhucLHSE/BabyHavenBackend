﻿using BabyHaven.Repositories.Base;
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
    public class MemberRepository : GenericRepository<Member>
    {
        public MemberRepository()
        {

        }

        public MemberRepository(SWP391_ChildGrowthTrackingSystemContext context)
            => _context = context;

        public async Task<List<Member>> GetAllMemberAsync()
        {
            var members = await _context.Members
                .Include(m => m.User)
                .ToListAsync();

            return members;
        }

        public async Task<Member?> GetByIdMemberAsync(Guid memberId)
        {
            return await _context.Members
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.MemberId == memberId);
        }
    }
}
