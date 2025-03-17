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

        public PromotionService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork
                ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<IServiceResult> GetAll()
        {

            var promotions = await _unitOfWork.PromotionRepository
                .GetAllAsync();

            if (promotions == null || !promotions.Any())
            {

                return new ServiceResult(Const.WARNING_NO_DATA_CODE, 
                    Const.WARNING_NO_DATA_MSG,
                    new List<PromotionViewAllDto>());
            }
            else
            {

                var promotionDtos = promotions
                    .Select(promotions => promotions.MapToPromotionViewAllDto())
                    .ToList();

                return new ServiceResult(Const.SUCCESS_READ_CODE, 
                    Const.SUCCESS_READ_MSG,
                    promotionDtos);
            }
        }

        public async Task<IQueryable<PromotionViewAllDto>> GetQueryable()
        {

            var promotions = await _unitOfWork.PromotionRepository
                .GetAllAsync();

            return promotions
                .Select(promotions => promotions.MapToPromotionViewAllDto())
                .AsQueryable();
        }

        public async Task<IServiceResult> GetById(Guid PromotionId)
        {

            var promotion = await _unitOfWork.PromotionRepository
                .GetByIdPromotionAsync(PromotionId);

            if (promotion == null)
            {

                return new ServiceResult(Const.WARNING_NO_DATA_CODE, 
                    Const.WARNING_NO_DATA_MSG,
                    new PromotionViewDetailsDto());
            }
            else
            {

                var promotionDto = promotion.MapToPromotionViewDetailsDto();

                return new ServiceResult(Const.SUCCESS_READ_CODE, 
                    Const.SUCCESS_READ_MSG,
                    promotionDto);
            }
        }

        public async Task<IServiceResult> Create(PromotionCreateDto promotionDto)
        {
            try
            {

                // Check if the promotion exists in the database
                var promotion = await _unitOfWork.PromotionRepository
                    .GetByPromotionCodeAsync(promotionDto.PromotionCode);

                if (promotion != null)
                {

                    return new ServiceResult(Const.FAIL_CREATE_CODE,
                        "PromotionCode with the same code already exists.");
                }

                // Map DTO to Entity
                var newPromotion = promotionDto.MapToPromotionCreateDto();

                // Add creation and update time information
                newPromotion.CreatedAt = DateTime.UtcNow;
                newPromotion.UpdatedAt = DateTime.UtcNow;

                // Save data to database
                var result = await _unitOfWork.PromotionRepository
                    .CreateAsync(newPromotion);

                if (result > 0)
                {

                    // Retrieve user details from UserAccountRepository
                    var createdByUser = await _unitOfWork.UserAccountRepository
                        .GetByIdAsync(newPromotion.CreatedBy);

                    var modifiedByUser = await _unitOfWork.UserAccountRepository
                        .GetByIdAsync(newPromotion.ModifiedBy);

                    // Assign retrieved user details to navigation properties
                    newPromotion.CreatedByNavigation = createdByUser;
                    newPromotion.ModifiedByNavigation = modifiedByUser;

                    // Map the saved entity to a response DTO
                    var responseDto = newPromotion.MapToPromotionViewDetailsDto();

                    return new ServiceResult(Const.SUCCESS_CREATE_CODE, 
                        Const.SUCCESS_CREATE_MSG,
                        responseDto);
                }
                else
                {

                    return new ServiceResult(Const.FAIL_CREATE_CODE, 
                        Const.FAIL_CREATE_MSG);
                }
            }
            catch (Exception ex)
            {

                return new ServiceResult(Const.ERROR_EXCEPTION, 
                    ex.ToString());
            }
        }

        public async Task<IServiceResult> Update(PromotionUpdateDto promotionDto)
        {
            try
            {

                // Check if the package exists in the database
                var promotion = await _unitOfWork.PromotionRepository
                    .GetByIdPromotionAsync(promotionDto.PromotionId);

                if (promotion == null)
                {

                    return new ServiceResult(Const.FAIL_UPDATE_CODE,
                        "Promotion not found.");
                }

                //Map DTO to Entity
                promotionDto.MapToPromotionUpdateDto(promotion);

                // Update time information
                promotion.UpdatedAt = DateTime.UtcNow;

                // Save data to database
                var result = await _unitOfWork.PromotionRepository
                    .UpdatePromotionAsync(promotion);

                if (result > 0)
                {

                    // Retrieve user details from UserAccountRepository
                    var createdByUser = await _unitOfWork.UserAccountRepository
                        .GetByIdAsync(promotion.CreatedBy);

                    var modifiedByUser = await _unitOfWork.UserAccountRepository
                        .GetByIdAsync(promotion.ModifiedBy);

                    // Assign retrieved user details to navigation properties
                    promotion.CreatedByNavigation = createdByUser;
                    promotion.ModifiedByNavigation = modifiedByUser;

                    // Map the saved entity to a response DTO
                    var responseDto = promotion.MapToPromotionViewDetailsDto();

                    return new ServiceResult(Const.SUCCESS_UPDATE_CODE, 
                        Const.SUCCESS_UPDATE_MSG,
                        responseDto);
                }
                else
                {

                    return new ServiceResult(Const.FAIL_UPDATE_CODE, 
                        Const.FAIL_UPDATE_MSG);
                }
            }
            catch (Exception ex)
            {

                return new ServiceResult(Const.ERROR_EXCEPTION, 
                    ex.ToString());
            }
        }

        public async Task<IServiceResult> DeleteById(Guid PromotionId)
        {
            try
            {

                var promotion = await _unitOfWork.PromotionRepository
                    .GetByIdPromotionAsync(PromotionId);

                if (promotion == null)
                {

                    return new ServiceResult(Const.WARNING_NO_DATA_CODE, 
                        Const.WARNING_NO_DATA_MSG,
                        new PromotionDeleteDto());
                }
                else
                {

                    var deletePromotionDto = promotion.MapToPromotionDeleteDto();

                    var result = await _unitOfWork.PromotionRepository
                        .RemoveAsync(promotion);

                    if (result)
                    {

                        return new ServiceResult(Const.SUCCESS_DELETE_CODE, 
                            Const.SUCCESS_DELETE_MSG,
                            deletePromotionDto);
                    }
                    else
                    {

                        return new ServiceResult(Const.FAIL_DELETE_CODE, 
                            Const.FAIL_DELETE_MSG,
                            deletePromotionDto);
                    }
                }
            }
            catch (Exception ex)
            {

                return new ServiceResult(Const.ERROR_EXCEPTION, 
                    ex.ToString());
            }
        }
    }
}
