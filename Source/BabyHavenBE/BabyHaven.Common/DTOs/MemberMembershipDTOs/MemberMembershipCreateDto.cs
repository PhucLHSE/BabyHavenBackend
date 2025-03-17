using BabyHaven.Common.Enum.MemberMembershipEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.MemberMembershipDTOs
{
    public class MemberMembershipCreateDto
    {
        [Required(ErrorMessage = "MemberId is required.")]
        public Guid MemberId { get; set; }

        [Required(ErrorMessage = "PackageName is required.")]
        [MaxLength(255, ErrorMessage = "PackageName cannot exceed 255 characters.")]
        public String PackageName { get; set; } = string.Empty;
    }
}
