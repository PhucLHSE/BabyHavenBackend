using BabyHaven.Common.Enum.PackageFeatureEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.PackagePromotionDTOs
{
    public class PackagePromotionCreateDto
    {
        // MembershipPackage
        [Required(ErrorMessage = "PackageName is required.")]
        [MaxLength(255, ErrorMessage = "PackageName cannot exceed 255 characters.")]
        public string PackageName { get; set; } = string.Empty;

        // Promotion
        [Required(ErrorMessage = "PromotionCode is required.")]
        [MaxLength(50, ErrorMessage = "PromotionCode cannot exceed 50 characters.")]
        public string PromotionCode { get; set; } = string.Empty;

        // PackagePromotion Status
        public bool IsActive { get; set; }
    }
}
