using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BabyHaven.Common.Enum.RatingFeedbackEnums;

namespace BabyHaven.Common.DTOs.RatingFeedbackDTOs
{
    public class RatingFeedbackCreateDto
    {
        [Required(ErrorMessage = "User ID is required")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Response ID is required")]
        public int ResponseId { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        [MaxLength(2000, ErrorMessage = "Comment cannot exceed 255 characters.")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "FeedbackDate is required.")]
        public string FeedbackDate { get; set; }

        [Required(ErrorMessage = "Rating date is required")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RatingFeedbackType FeedbackType { get; set; }

        [Required(ErrorMessage = "Rating status is required")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FeedbackStatus Status { get; set; }
    }
}
