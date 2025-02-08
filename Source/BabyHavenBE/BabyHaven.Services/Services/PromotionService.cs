using BabyHaven.Common.DTOs.MembershipPackageDTOs;
using BabyHaven.Common;
using BabyHaven.Repositories;
using BabyHaven.Repositories.Mappers;
using BabyHaven.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common.DTOs.PromotionDTOs;
using BabyHaven.Services.Mappers;
using BabyHaven.Services.IServices;

namespace BabyHaven.Services.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly UnitOfWork _unitOfWork;

        public PromotionService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IServiceResult> GetAll()
        {
            var promotions = await _unitOfWork.PromotionRepository.GetAllAsync();

            if (promotions == null || !promotions.Any())
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG,
                    new List<PromotionViewAllDto>());
            }
            else
            {
                var promotionDtos = promotions
                    .Select(promotions => promotions.MapToPromotionViewAllDto())
                    .ToList();

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG,
                    promotionDtos);
            }
        }
    }
}
