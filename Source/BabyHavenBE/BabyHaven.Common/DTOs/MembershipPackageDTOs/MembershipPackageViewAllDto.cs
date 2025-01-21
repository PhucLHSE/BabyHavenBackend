using BabyHaven.Common.Enum.MembershipPackageEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.MembershipPackageDTOs
{
    public class MembershipPackageViewAllDto
    {
        public int PackageId { get; set; }

        public string PackageName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string Currency { get; set; } = string.Empty;

        public int DurationMonths { get; set; }

        public int MaxChildrenAllowed { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MembershipPackageStatus Status { get; set; } = MembershipPackageStatus.Inactive;
    }
}
