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
    public class DoctorRepository:GenericRepository<Doctor>
    {
        public DoctorRepository() { }
        public DoctorRepository(SWP391_ChildGrowthTrackingSystemContext context) => _context = context;
        public async Task<Doctor?> GetByDoctorNameAsync(string name)
        {
            return await _context.Doctors
                .FirstOrDefaultAsync(d => d.Name == name);
        }

        public async Task<Dictionary<string, int>> GetAllDoctorNameToIdMappingAsync()
        {
            return await _context.Doctors
                .ToDictionaryAsync(d => d.Name, d => d.DoctorId);
        }

        public async Task<IEnumerable<Doctor>> GetAllWithUsersAsync()
        {
            return await _context.Doctors
                .Include(d => d.User)
                .ToListAsync();
        }
        public async Task<Doctor> GetByIdWithUsersAsync(int DoctorId)
        {
            return await _context.Doctors
                .Include(u => u.User)
                .FirstOrDefaultAsync(u => u.DoctorId == DoctorId);
        }
    }
}
