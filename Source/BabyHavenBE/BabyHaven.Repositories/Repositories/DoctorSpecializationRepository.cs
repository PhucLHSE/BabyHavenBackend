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
    public class DoctorSpecializationRepository:GenericRepository<DoctorSpecialization>
    {
        public DoctorSpecializationRepository() { }
        public DoctorSpecializationRepository(SWP391_ChildGrowthTrackingSystemContext context) => _context = context;
        public async Task<List<DoctorSpecialization>> GetAllDoctorSpecializationAsync()
        {
            var doctorSpecializations = await _context.DoctorSpecializations
                .Include(ds => ds.Doctor)
                .Include(ds => ds.Specialization)
                .ToListAsync();

            return doctorSpecializations;
        }

        public async Task<DoctorSpecialization> GetByIdDoctorSpecializationAsync(int doctorId, int specializationId)
        {
            var doctorSpecialization = await _context.DoctorSpecializations
                .Include(ds => ds.Doctor)
                .Include(ds => ds.Specialization)
                .FirstOrDefaultAsync(pf => pf.DoctorId == doctorId && pf.SpecializationId == specializationId);

            return doctorSpecialization;
        }
    }
}
