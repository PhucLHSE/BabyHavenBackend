using BabyHaven.Common.Enum.BlogEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.BlogDTOs
{
    public class BlogCreateDto
    {
        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(255, ErrorMessage = "Title cannot exceed 255 characters.")]
        public string Title { get; set; } = string.Empty;


        [Required(ErrorMessage = "Content is required.")]
        [MaxLength(2000, ErrorMessage = "Content cannot exceed 2000 characters.")]
        public string Content { get; set; } = string.Empty;


        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(255, ErrorMessage = "Email cannot exceed 255 characters.")]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = "CategoryName is required.")]
        [MaxLength(255, ErrorMessage = "CategoryName cannot exceed 255 characters.")]
        public string CategoryName { get; set; } = string.Empty;


        public string ImageBlog { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tags is required.")]
        [MaxLength(255, ErrorMessage = "Tags cannot exceed 255 characters.")]
        public string Tags { get; set; } = string.Empty;


        [Required(ErrorMessage = "ReferenceSources is required.")]
        [MaxLength(2000, ErrorMessage = "ReferenceSources cannot exceed 2000 characters.")]
        public string ReferenceSources { get; set; } = string.Empty;

        [Required(ErrorMessage = "Status is required.")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BlogStatus? Status { get; set; } = BlogStatus.PendingApproval;
    }
}
