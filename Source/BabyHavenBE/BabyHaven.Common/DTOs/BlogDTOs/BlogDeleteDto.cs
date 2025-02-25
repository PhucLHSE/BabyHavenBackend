using BabyHaven.Common.Enum.BlogEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.BlogDTOs
{
    public class BlogDeleteDto
    {
        //Blog details
        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public string ImageBlog { get; set; } = string.Empty;


        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BlogStatus Status { get; set; } = BlogStatus.Approved;


        public string RejectionReason { get; set; } = string.Empty;


        public string Tags { get; set; } = string.Empty;


        public string ReferenceSources { get; set; } = string.Empty;


        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }


        //UserAccount details
        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        public DateOnly? DateOfBirth { get; set; }

        public string Address { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public byte[] ProfilePicture { get; set; }

        public string VerificationCode { get; set; } = string.Empty;


        //Blog Category details
        public string CategoryName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public int? ParentCategoryId { get; set; }
    }
}
