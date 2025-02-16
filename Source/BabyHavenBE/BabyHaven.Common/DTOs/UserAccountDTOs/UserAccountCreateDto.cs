using BabyHaven.Common.Enum.Converters;
using BabyHaven.Common.Enum.UserAccountEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.UserAccountDTOs
{
    public class UserAccountCreateDto
    {
        [Required(ErrorMessage = "UserName is required.")]
        [MaxLength(255, ErrorMessage = "UserName cannot exceed 255 characters.")]
        public string Username { get; set; } = string.Empty;


        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(255, ErrorMessage = "Email cannot exceed 255 characters.")]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = "PhoneNumber is required.")]
        [MaxLength(20, ErrorMessage = "PhoneNumber cannot exceed 20 characters.")]
        public string PhoneNumber { get; set; } = string.Empty;


        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(255, ErrorMessage = "Name cannot exceed 255 characters.")]
        public string Name { get; set; } = string.Empty;


        [Required(ErrorMessage = "Gender is required.")]
        [MaxLength(20, ErrorMessage = "Gender cannot exceed 20 characters.")]
        public string Gender { get; set; } = string.Empty;


        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly DateOfBirth { get; set; }


        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(255, ErrorMessage = "Address cannot exceed 255 characters.")]
        public string Address { get; set; } = string.Empty;


        [Required(ErrorMessage = "Password is required.")]
        [MaxLength(255, ErrorMessage = "Password cannot exceed 255 characters.")]
        public string Password { get; set; } = string.Empty;

        public byte[]? ProfilePicture { get; set; }


        [Required(ErrorMessage = "Status is required.")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserAccountStatus Status { get; set; } = UserAccountStatus.Inactive;


        [Required(ErrorMessage = "RoleId is required.")]
        public int RoleId { get; set; }
    }
}
