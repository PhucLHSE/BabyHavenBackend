using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common.DTOs.GrowthRecordDTOs;
using BabyHaven.Services.Base;

namespace BabyHaven.Services.IServices
{
    public interface IGrowthAnalysisService
    {
        Task<IServiceResult> AnalyzeGrowthRecord(GrowthRecordAnalysisDto record);
    }
}
