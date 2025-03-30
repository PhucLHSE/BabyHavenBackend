using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.AIChatDTOs
{
    public class GrowthRecordChatRequest
    {
        public string SessionId { get; set; }
        public string UserMessage { get; set; } = string.Empty;
        public GrowthRecordAnalysisDto? InitialRecord { get; set; }
    }
}
