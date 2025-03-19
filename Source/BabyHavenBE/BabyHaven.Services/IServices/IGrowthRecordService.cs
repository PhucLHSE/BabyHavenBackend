using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common.DTOs.GrowthRecordDTOs;
using BabyHaven.Services.Base;

namespace BabyHaven.Services.IServices
{
    public interface IGrowthRecordService
    {
        Task<IServiceResult> CreateGrowthRecordRequired(GrowthRecordRequiredDto dto);

        Task<IServiceResult> CreateGrowthRecord(GrowthRecordCreateDto dto);

        Task<IServiceResult> UpdateGrowthRecord(GrowthRecordUpdateDto dto);

        Task<IServiceResult> DeleteGrowthRecord(int recordId);

        Task<IServiceResult> GetGrowthRecordById(int recordId, Guid childId);

        Task<IServiceResult> GetAllGrowthRecordsByChild(Guid childId);

        Task<IQueryable<GrowthRecordViewAllDto>> GetQueryable();

        Task<IServiceResult> GetRecordsByDateRangeAsync(Guid childId, DateTime startDate, DateTime endDate);
    }
}
