using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.BlogCategoryDTOs
{
    public class BlogCategoryDeleteDto
    {
        public string CategoryName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty ;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsActive { get; set; }

        public int? ParentCategoryId { get; set; }
    }
}
