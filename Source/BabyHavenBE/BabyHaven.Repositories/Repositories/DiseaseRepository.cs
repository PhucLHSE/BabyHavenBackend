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
    public class DiseaseRepository : GenericRepository<Disease>
    {
        public DiseaseRepository()
        {
        }

        public DiseaseRepository(SWP391_ChildGrowthTrackingSystemContext context)
            => _context = context;

        public async Task<IEnumerable<Disease>> GetDiseasesBySeverityAsync(string severity)
        {
            return await _context.Diseases
                .Where(d => d.Severity == severity)
                .ToListAsync();
        }

        public async Task<List<Disease>> GetDiseasesByAgeAsync(int age)
        {
            return await _context.Diseases
                .AsNoTracking()
                .Where(d => age >= d.MinAge && age <= d.MaxAge)
                .OrderByDescending(d => d.Severity) // ✅ Sắp xếp theo mức độ nghiêm trọng
                .ToListAsync();
        }
    }
}
