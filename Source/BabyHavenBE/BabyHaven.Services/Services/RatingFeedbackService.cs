using BabyHaven.Common.DTOs.ConsultationResponseDTOs;
using BabyHaven.Common;
using BabyHaven.Repositories;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common.DTOs.RatingFeedbackDTOs;

namespace BabyHaven.Services.Services
{
    public class RatingFeedbackService : IRatingFeedbackService
    {
        private readonly UnitOfWork _unitOfWork;
        public RatingFeedbackService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IServiceResult> Create(RatingFeedbackCreateDto dto)
        {
            if (dto == null)
            {
                return new ServiceResult(Const.FAIL_DELETE_CODE,
                    Const.FAIL_DELETE_MSG);
            }
            var feedback = dto.MapToRatingFeedBack();

            var result = await _unitOfWork.RatingFeedbackRepository
                .CreateAsync(feedback);

            if (result > 0)
            {
                return new ServiceResult(Const.SUCCESS_CREATE_CODE,
                    Const.SUCCESS_CREATE_MSG, feedback);
            }
            else
            {
                return new ServiceResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
            }
        }

        public async Task<IServiceResult> GetAll()
        {

            var feedbacks = await _unitOfWork.RatingFeedbackRepository
                .GetAllRatingFeedbackAsync();

            if (feedbacks == null || !feedbacks.Any())
            {

                return new ServiceResult(Const.WARNING_NO_DATA_CODE,
                    Const.WARNING_NO_DATA_MSG,
                    new List<RatingFeedbackViewAllDto>());
            }
            else
            {

                var feedbackDtos = feedbacks
                    .Select(feedbacks => feedbacks.MapToRatingFeedbackViewAllDto())
                    .ToList();

                return new ServiceResult(Const.SUCCESS_READ_CODE,
                    Const.SUCCESS_READ_MSG,
                    feedbackDtos);
            }

        }

        public Task<IServiceResult> GetByid(int ratingId)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<RatingFeedbackViewAllDto>> GetQueryable()
        {

            var feedbacks = await _unitOfWork.RatingFeedbackRepository
                .GetAllRatingFeedbackAsync();

            return feedbacks
                .Select(feedbacks => feedbacks.MapToRatingFeedbackViewAllDto())
                .AsQueryable();
        }
    }
}
