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
    public class GrowthRecordRepository : GenericRepository<GrowthRecord>
    {
        public GrowthRecordRepository()
        {

        }

        public GrowthRecordRepository(SWP391_ChildGrowthTrackingSystemContext context)
            => _context = context;

        //public async Task<IEnumerable<GrowthRecord>> GetAllGrowthRecordsByChild(Guid childId)
        //{
        //    if (childId == Guid.Empty) throw new ArgumentException("Invalid child ID", nameof(childId));

        //    return await _context.GrowthRecords
        //        .Include(gr => gr.Child)
        //        .Include(gr => gr.RecordedByNavigation)
        //        .Where(gr => gr.ChildId == childId)
        //        .ToListAsync();
        //}
        public async Task<List<GrowthRecord>> GetAllGrowthRecordsByChild(Guid childId)
        {
            if (childId == Guid.Empty) throw new ArgumentException("Invalid child ID", nameof(childId));

            return await _context.GrowthRecords
                .AsNoTracking()
                .Where(gr => gr.ChildId == childId)
                .OrderByDescending(gr => gr.CreatedAt) //Lấy mới nhất trước
                .ToListAsync();
        }

        public async Task<GrowthRecord> GetLatestGrowthRecordByChildAsync(Guid childId)
        {
            if (childId == Guid.Empty)
            {
                throw new ArgumentException("Invalid child ID", nameof(childId));
            }

            return await _context.GrowthRecords
                .Include(gr => gr.Child)
                .Include(gr => gr.RecordedByNavigation)
                .AsNoTracking()
                .Where(gr => gr.ChildId == childId)
                .OrderByDescending(gr => gr.CreatedAt)
                .FirstOrDefaultAsync();
        }

        public async Task<GrowthRecord> GetGrowthRecordById(int recordId, Guid childId)
        {
            if (recordId <= 0) throw new ArgumentException("Invalid record ID", nameof(recordId));
            if (childId == Guid.Empty) throw new ArgumentException("Invalid child ID", nameof(childId));

            return await _context.GrowthRecords
                .Include(gr => gr.Child)
                .Include(gr => gr.RecordedByNavigation)
                .FirstOrDefaultAsync(gr => gr.RecordId == recordId && gr.ChildId == childId);
        }

        public async Task<IEnumerable<GrowthRecord>> GetRecordsByDateAsync(Guid childId, DateTime date)
        {
            if (childId == Guid.Empty) throw new ArgumentException("Invalid child ID", nameof(childId));

            return await _context.GrowthRecords
                .Include(gr => gr.Child)
                .Include(gr => gr.RecordedByNavigation)
                .Where(gr => gr.ChildId == childId && gr.CreatedAt.HasValue && gr.CreatedAt.Value.Date == date.Date)
                .OrderByDescending(gr => gr.CreatedAt)
                .ToListAsync();
        }

        //public async Task<IEnumerable<GrowthRecord>> GetRecordsByDateRangeAsync(Guid childId, DateTime startDate, DateTime endDate)
        //{
        //    if (childId == Guid.Empty) throw new ArgumentException("Invalid child ID", nameof(childId));
        //    if (startDate > endDate) throw new ArgumentException("Start date must be earlier than end date", nameof(startDate));

        //    return await _context.GrowthRecords
        //        .Include(gr => gr.Child)
        //        .Include(gr => gr.RecordedByNavigation)
        //        .Where(gr => gr.ChildId == childId && gr.CreatedAt.HasValue && gr.CreatedAt.Value.Date >= startDate.Date && gr.CreatedAt.Value.Date <= endDate.Date)
        //        .OrderByDescending(gr => gr.CreatedAt)
        //        .ToListAsync();
        //}

        public async Task<List<GrowthRecord>> GetRecordsByDateRangeAsync(Guid childId, DateTime startDate, DateTime endDate)
        {
            if (childId == Guid.Empty) throw new ArgumentException("Invalid child ID", nameof(childId));
            if (startDate > endDate) throw new ArgumentException("Start date must be earlier than end date", nameof(startDate));

            return await _context.GrowthRecords
                .AsNoTracking()
                .Where(gr => gr.ChildId == childId && gr.CreatedAt >= startDate && gr.CreatedAt <= endDate)
                .OrderByDescending(gr => gr.CreatedAt)
                .ToListAsync();
        }
    }
}
