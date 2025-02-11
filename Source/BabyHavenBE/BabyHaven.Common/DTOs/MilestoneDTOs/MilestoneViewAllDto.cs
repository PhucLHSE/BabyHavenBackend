using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.MilestoneDTOS
{
    public class MilestoneViewAllDto
    {
        public string MilestoneName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Importance { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public int? MinAge { get; set; }

        public int? MaxAge { get; set; }

        public bool IsPersonal { get; set; }
    }
}
