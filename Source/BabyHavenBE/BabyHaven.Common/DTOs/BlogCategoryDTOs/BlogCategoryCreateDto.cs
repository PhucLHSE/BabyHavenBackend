using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.BlogCategoryDTOs
{
    public class BlogCategoryCreateDto
    {
        [Required(ErrorMessage = "CategoryName is required.")]
        [StringLength(255, ErrorMessage = "CategoryName can't be longer than 255 characters.")]
        public string CategoryName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(2000, ErrorMessage = "Description can't be longer than 2000 characters.")]
        public string Description { get; set; } = string.Empty;


        [Required(ErrorMessage = "IsActive is required.")]
        public bool IsActive { get; set; }


        public int? ParentCategoryId { get; set; }
    }
}
