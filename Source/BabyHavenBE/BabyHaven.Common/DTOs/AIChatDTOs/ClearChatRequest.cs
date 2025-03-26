using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.AIChatDTOs
{
    public class ClearChatRequest
    {
        [Required]
        public string SessionId { get; set; } = string.Empty;
    }
}
