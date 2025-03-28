using BabyHaven.Common.Enum.RatingFeedbackEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.RatingFeedbackDTOs
{
    public class RatingFeedbackViewAllDto
    {
        public int FeedbackId { get; set; }

        public Guid UserId { get; set; }

        public int ResponseId { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; } = string.Empty;

        public DateTime FeedbackDate { get; set; }


        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RatingFeedbackType  FeedbackType { get; set; } = RatingFeedbackType.General;


        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FeedbackStatus Status { get; set; } = FeedbackStatus.Pending;
    }
}
