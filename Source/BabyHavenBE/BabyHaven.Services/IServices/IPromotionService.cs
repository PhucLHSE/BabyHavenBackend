using BabyHaven.Common.DTOs.MembershipPackageDTOs;
using BabyHaven.Common.DTOs.PromotionDTOs;
using BabyHaven.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.IServices
{
    public interface IPromotionService
    {
        Task<IServiceResult> GetAll();

        Task<IQueryable<PromotionViewAllDto>> GetQueryable();

        Task<IServiceResult> GetById(Guid PromotionId);

        Task<IServiceResult> Create(PromotionCreateDto promotionDto);

        Task<IServiceResult> Update(PromotionUpdateDto promotionDto);

        Task<IServiceResult> DeleteById(Guid PromotionId);
    }
}
