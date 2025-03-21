using BabyHaven.Common.DTOs.ConsultationResponseDTOs;
using BabyHaven.Common.DTOs.RatingFeedbackDTOs;
using BabyHaven.Common.Enum.ConsultationResponseEnums;
using BabyHaven.Common.Enum.RatingFeedbackEnums;
using BabyHaven.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.Mappers
{
    public static class RatingFeedbackMapper
    {
        // Mapper RatingFeedbackViewAllDto
        public static RatingFeedbackViewAllDto MapToRatingFeedbackViewAllDto(this RatingFeedback model)
        {
            return new RatingFeedbackViewAllDto
            {
                UserId = model.User?.UserId ?? Guid.Empty,
                ResponseId = model.Response?.ResponseId ?? 0,
                Rating = model.Rating,
                Comment = model.Comment,
                FeedbackDate = model.FeedbackDate,
                FeedbackType = Enum.TryParse<RatingFeedbackType>(model.FeedbackType, true, out var type)
                                ? type
                                : RatingFeedbackType.General,

                // Convert Status from string to enum
                Status = Enum.TryParse<FeedbackStatus>(model.Status, true, out var status)
                          ? status
                          : FeedbackStatus.Pending
            };
        }
    }
}
