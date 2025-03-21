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
