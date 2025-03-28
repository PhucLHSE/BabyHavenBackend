using BabyHaven.Common.DTOs.AlertDTOS;
using BabyHaven.Common.DTOs.BlogDTOs;
using BabyHaven.Common.DTOs.ConsultationResponseDTOs;
using BabyHaven.Common.DTOs.RatingFeedbackDTOs;
using BabyHaven.Common.Enum.BlogEnums;
using BabyHaven.Common.Enum.ConsultationResponseEnums;
using BabyHaven.Common.Enum.RatingFeedbackEnums;
using BabyHaven.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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
                FeedbackId = model.FeedbackId,
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

        public static RatingFeedback MapToRatingFeedBack(this RatingFeedbackCreateDto dto)
        {
            return new RatingFeedback
            {
                UserId = dto.UserId,
                ResponseId = dto.ResponseId,
                Rating = dto.Rating,
                Comment = dto.Comment,
                FeedbackDate = DateTime.Parse(dto.FeedbackDate),
                FeedbackType = dto.FeedbackType.ToString(),
                Status = dto.Status.ToString(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }

        public static RatingFeedback ToRatingFeedback(this RatingFeedbackUpdateDto dto, RatingFeedback existingRatingFeedback)
        {
            existingRatingFeedback.Rating = dto.Rating;
            existingRatingFeedback.Comment = dto.Comment;
            existingRatingFeedback.FeedbackDate = dto.FeedbackDate;

            return existingRatingFeedback;
        }

        public static RatingFeedbackViewDetailsDto ToRatingFeedbackViewDetailsDto(this RatingFeedback feedback)
        {
            return new RatingFeedbackViewDetailsDto
            {
                Rating = feedback.Rating,
                Comment = feedback.Comment,
                FeedbackDate = feedback.FeedbackDate,
                FeedbackType = Enum.TryParse<RatingFeedbackType>(feedback.FeedbackType, true, out var type)
                                ? type
                                : RatingFeedbackType.General,

                // Convert Status from string to enum
                Status = Enum.TryParse<FeedbackStatus>(feedback.Status, true, out var status)
                          ? status
                          : FeedbackStatus.Pending,

                CreatedAt = feedback.CreatedAt,
                UpdatedAt = feedback.UpdatedAt,
                UserId = feedback.User?.UserId ?? Guid.Empty,       
                ResponseId = feedback.Response?.ResponseId?? 0 ,
            };
        }

        //Mapper RatingFeedbackDeleteDto
        public static RatingFeedbackDeleteDto MapToRatingFeedbackDeleteDto(this RatingFeedback feedback)
        {
            return new RatingFeedbackDeleteDto
            {
                Rating = feedback.Rating,
                Comment = feedback.Comment,
                FeedbackDate = feedback.FeedbackDate,
                FeedbackType = Enum.TryParse<RatingFeedbackType>(feedback.FeedbackType, true, out var type)
                                ? type
                                : RatingFeedbackType.General,

                // Convert Status from string to enum
                Status = Enum.TryParse<FeedbackStatus>(feedback.Status, true, out var status)
                          ? status
                          : FeedbackStatus.Pending,

                CreatedAt = feedback.CreatedAt,
                UpdatedAt = feedback.UpdatedAt,
                UserId = feedback.User?.UserId ?? Guid.Empty,
                ResponseId = feedback.Response?.ResponseId ?? 0,

            };
        }
    }
}
