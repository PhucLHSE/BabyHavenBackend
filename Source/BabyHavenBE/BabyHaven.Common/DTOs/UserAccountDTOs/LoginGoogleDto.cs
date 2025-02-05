using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.UserAccountDTOs
{
    public class LoginGoogleDto
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? ProfilePictureUrl { get; set; }

        public string? GoogleId { get; set; }

        [Required]
        public int RoleId { get; set; }
    }
}
