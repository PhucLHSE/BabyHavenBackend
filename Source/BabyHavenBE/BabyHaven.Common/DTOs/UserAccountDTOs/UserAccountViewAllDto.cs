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
    public class UserAccountViewAllDto
    {
        public string Username { get; set; } = string.Empty;


        public string Email { get; set; } = string.Empty;


        public string PhoneNumber { get; set; } = string.Empty;


        public string Name { get; set; } = string.Empty;


        public string Gender { get; set; } = string.Empty;


        public DateOnly DateOfBirth { get; set; }


        public string Address { get; set; } = string.Empty;


        public byte[] ProfilePicture { get; set; }


        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserAccountStatus Status { get; set; } = UserAccountStatus.Inactive;

        public bool? IsVerified { get; set; }

        public string RoleName { get; set; } = string.Empty;
    }
}
