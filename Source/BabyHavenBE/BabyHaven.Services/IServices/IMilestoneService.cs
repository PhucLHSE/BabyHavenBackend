using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common.DTOs.MilestoneDTOS;
using BabyHaven.Services.Base;

namespace BabyHaven.Services.IServices
{
    public interface IMilestoneService
    {
        Task<IServiceResult> GetAll();

        Task<IQueryable<MilestoneViewAllDto>> GetQueryable();

        Task<IServiceResult> GetById(int milestoneId);

        Task<IServiceResult> Create(MilestoneCreateDto milestone);

        Task<IServiceResult> Update(MilestoneUpdateDto milestone);

        Task<IServiceResult> Delete(int milestoneId);
    }
}
