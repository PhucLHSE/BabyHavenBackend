using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Common.Enum.RatingFeedbackEnums
{
    public enum FeedbackStatus
    {
        Pending,    // Đang chờ xử lý
        Approved,   // Đã được duyệt
        Rejected,   // Bị từ chối
        Resolved    // Đã xử lý phản hồi
    }
}
