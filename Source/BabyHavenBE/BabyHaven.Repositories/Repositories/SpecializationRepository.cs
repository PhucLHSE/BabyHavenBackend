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
    public class SpecializationRepository:GenericRepository<Specialization>
    {
        public SpecializationRepository() { }
        public SpecializationRepository(SWP391_ChildGrowthTrackingSystemContext context) => _context = context;
        public async Task<Specialization?> GetBySpecializationNameAsync(string specializationName)
        {
            return await _context.Specializations
                .FirstOrDefaultAsync(s => s.SpecializationName == specializationName);
        }

        public async Task<Dictionary<string, int>> GetAllSpecializationNameToIdMappingAsync()
        {
            return await _context.Specializations
                .ToDictionaryAsync(s => s.SpecializationName, s => s.SpecializationId);
        }

        public async Task<List<Specialization>> GetByIds(int[] specIds)
        {
            var distinctSpecIds = specIds.Distinct().ToArray();
            return await _context.Specializations
                .Where(s => distinctSpecIds.Contains(s.SpecializationId))
                .ToListAsync();
        }
    }
}
