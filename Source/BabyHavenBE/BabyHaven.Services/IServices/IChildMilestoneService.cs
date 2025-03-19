using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common.DTOs.ChildMilestoneDTOs;
using BabyHaven.Common.DTOs.ChildrenDTOs;
using BabyHaven.Services.Base;

namespace BabyHaven.Services.IServices
{
    public interface IChildMilestoneService
    {
        Task<IServiceResult> GetAll();

        Task<IQueryable<ChildMilestoneViewAllDto>> GetQueryable();

        Task<IServiceResult> GetById(Guid childId, int milestoneId);

        Task<IServiceResult> Create(ChildMilestoneCreateDto childMilestone);

        Task<IServiceResult> Update(ChildMilestoneUpdateDto childMilestone);

        Task<IServiceResult> Delete(Guid childId, int milestoneId);
    }
}
