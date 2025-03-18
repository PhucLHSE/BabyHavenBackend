using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common.DTOs.BlogDTOs;

namespace BabyHaven.Common.DTOs.BlogCategoryDTOs
{
    public class BlogCategoryViewAllDto
    {
        public List<BlogViewAllDto> Blog { get; set; } = new List<BlogViewAllDto>();
        public string CategoryName { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public int? ParentCategoryId { get; set; }

          
    }
}
