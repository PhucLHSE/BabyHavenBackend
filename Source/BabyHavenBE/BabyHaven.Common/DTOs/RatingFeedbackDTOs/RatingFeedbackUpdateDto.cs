using BabyHaven.Common.Enum.RatingFeedbackEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.RatingFeedbackDTOs
{
    public class RatingFeedbackUpdateDto
    {
        [Required(ErrorMessage = "FeedbackId is required.")]
        public int FeedbackId { get; set; }

        [Required(ErrorMessage = "UserId is required.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "ResponseId is required.")]
        public int ResponseId { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Comment is required.")]
        [MaxLength(2000, ErrorMessage = "Comment cannot exceed 2000 characters.")]
        public string Comment { get; set; } = string.Empty;

        [Required(ErrorMessage = "FeedbackDate is required.")]
        public DateTime FeedbackDate { get; set; }

        [Required(ErrorMessage = "FeedbackType is required.")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RatingFeedbackType FeedbackType { get; set; } = RatingFeedbackType.General;

        [Required(ErrorMessage = "Status is required.")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FeedbackStatus Status { get; set; } = FeedbackStatus.Pending;

    }
}
