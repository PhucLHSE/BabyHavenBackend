using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.UserAccountDTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(255, ErrorMessage = "Invalid Email ")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [MaxLength(255, ErrorMessage = " Invalid Password")]
        public string Password { get; set; } = string.Empty;
    }
}
