using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.BlogCategoryDTOs
{
    public class BlogCategoryViewAllDto
    {
        public string CategoryName { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public int? ParentCategoryId { get; set; }

          
    }
}
