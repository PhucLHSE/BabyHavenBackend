using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.Enum.AlertEnums
{
    [JsonConverter(typeof(JsonStringEnumConverter))] // Serialize Enum thành string khi trả về API
    public enum SeverityLevelEnum
    {
        Mild,        // Nhẹ
        Moderate,    // Trung bình
        Severe,      // Nghiêm trọng
        Critical    // Nguy cấp
    }
}
