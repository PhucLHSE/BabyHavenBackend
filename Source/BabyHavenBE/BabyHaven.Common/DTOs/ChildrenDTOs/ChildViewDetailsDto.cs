using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.ChildrenDTOs
{
    public class ChildViewDetailsDto
    {
        public Guid ChildId { get; set; }

        public Guid MemberId { get; set; }

        public int Age { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateOnly DateOfBirth { get; set; }

        public string Gender { get; set; } = string.Empty;

        public double? BirthWeight { get; set; }

        public double? BirthHeight { get; set; }

        public string BloodType { get; set; } = string.Empty;

        public string Allergies { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        public string RelationshipToMember { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
