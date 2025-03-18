using BabyHaven.Common.DTOs.BlogDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.BlogCategoryDTOs
{
    public class BlogCategoryViewDetailsDto
    {
        public string CategoryName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsActive { get; set; }

        public int? ParentCategoryId { get; set; }

        // Danh sách Blogs thuộc danh mục này
        public List<BlogViewAllDto> Blogs { get; set; }
    }
}
