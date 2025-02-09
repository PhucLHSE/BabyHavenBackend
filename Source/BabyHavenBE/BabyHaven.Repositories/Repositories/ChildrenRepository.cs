﻿using System;
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
    }
}
