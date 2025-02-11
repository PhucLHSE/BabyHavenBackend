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
        Task<IServiceResult> CreateGrowthRecordInfant(GrowthRecordInfantDto dto);
        Task<IServiceResult> CreateGrowthRecordToddler(GrowthRecordToddlerDto dto);
        Task<IServiceResult> CreateGrowthRecordChild(GrowthRecordChildDto dto);
        Task<IServiceResult> CreateGrowthRecordTeenager(GrowthRecordTeenagerDto dto);
        Task<IServiceResult> DeleteGrowthRecord(int recordId);
        Task<IServiceResult> GetGrowthRecordById(int recordId, Guid childId);
        Task<IServiceResult> GetAllGrowthRecordsByChild(Guid childId);
        Task<IServiceResult> GetRecordsByDateRangeAsync(Guid childId, DateTime startDate, DateTime endDate);
    }
}
