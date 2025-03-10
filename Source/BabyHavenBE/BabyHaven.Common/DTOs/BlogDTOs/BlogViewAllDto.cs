using BabyHaven.Common.Enum.BlogEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.BlogDTOs
{
    public class BlogViewAllDto
    {
        public int BlogId { get; set; }
        public string Title { get; set; } = string.Empty;


        public string Content { get; set; } = string.Empty;


        public string AuthorName { get; set; } = string.Empty;


        public string CategoryName { get; set; } = string.Empty;

        public string Tags { get; set; } = string.Empty;

        public string ImageBlog { get; set; } = string.Empty;



        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BlogStatus Status { get; set; } = BlogStatus.Approved;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
