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
    public class UserAccountUpdateDto
    {
        [Required(ErrorMessage = "UserId is required.")]
        public Guid UserId { get; set; }

        [MaxLength(255, ErrorMessage = "UserName cannot exceed 255 characters.")]
        public string Username { get; set; } = string.Empty;

        [MaxLength(255, ErrorMessage = "Email cannot exceed 255 characters.")]
        public string Email { get; set; } = string.Empty;


        [MaxLength(20, ErrorMessage = "PhoneNumber cannot exceed 20 characters.")]
        public string PhoneNumber { get; set; } = string.Empty;


        [MaxLength(255, ErrorMessage = "Name cannot exceed 255 characters.")]
        public string Name { get; set; } = string.Empty;


        [MaxLength(20, ErrorMessage = "Gender cannot exceed 20 characters.")]
        public string Gender { get; set; } = string.Empty;


        public string DateOfBirth { get; set; } = string.Empty;


        [MaxLength(255, ErrorMessage = "Address cannot exceed 255 characters.")]
        public string Address { get; set; } = string.Empty;


        [MaxLength(255, ErrorMessage = "Password cannot exceed 255 characters.")]
        public string Password { get; set; } = string.Empty;

        public byte[]? ProfilePicture { get; set; }


        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserAccountStatus? Status { get; set; } = UserAccountStatus.Inactive;
        public bool? IsVerified { get; set; } = true;
    }
}
