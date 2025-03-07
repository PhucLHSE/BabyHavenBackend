﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.BlogCategoryDTOs
{
    public class BlogCategoryUpdateDto
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "CategoryName is required.")]
        [StringLength(100, ErrorMessage = "CategoryName can't be longer than 100 characters.")]
        public string CategoryName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(2000, ErrorMessage = "Description can't be longer than 2000 characters.")]
        public string Description { get; set; } = string.Empty ;

        [Required(ErrorMessage = "IsActive is required.")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "ParentCategoryId is required.")]
        public int? ParentCategoryId { get; set; }
    }
}
