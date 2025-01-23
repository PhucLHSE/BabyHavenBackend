using BabyHaven.Common.DTOs.FeatureDTOs;
using BabyHaven.Common.DTOs.PackageFeatureDTOs;
using BabyHaven.Common.Enum.FeatureEnums;
using BabyHaven.Common.Enum.PackageFeatureEnums;
using BabyHaven.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.Mappers
{
    public static class PackageFeatureMapper
    {
        // Mapper PackageFeatureViewAllDto
        public static PackageFeatureViewAllDto MapToPackageFeatureViewAllDto(this PackageFeature model)
        {
            return new PackageFeatureViewAllDto
            {
                // MembershipPackage
                PackageName = model.Package?.PackageName ?? string.Empty,

                // Feature
                FeatureName = model.Feature?.FeatureName ?? string.Empty,

                // Convert Status from string to enum
                Status = Enum.TryParse<PackageFeatureStatus>(model.Status, true, out var status)
                          ? status
                          : PackageFeatureStatus.Inactive
            };
        }
    }
}
