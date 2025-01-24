﻿using BabyHaven.Common.DTOs.FeatureDTOs;
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

        // Mapper PackageFeatureViewDetailsDto
        public static PackageFeatureViewDetailsDto MapToPackageFeatureViewDetailsDto(this PackageFeature model)
        {
            return new PackageFeatureViewDetailsDto
            {
                // MembershipPackage details
                PackageName = model.Package?.PackageName ?? string.Empty,
                PackageDescription = model.Package?.Description ?? string.Empty,
                PackagePrice = model.Package?.Price ?? 0m,
                PackageCurrency = model.Package?.Currency ?? string.Empty,
                DurationMonths = model.Package?.DurationMonths ?? 0,
                TrialPeriodDays = model.Package?.TrialPeriodDays,
                MaxChildrenAllowed = model.Package?.MaxChildrenAllowed ?? 0,
                SupportLevel = model.Package?.SupportLevel ?? string.Empty,

                // Feature details
                FeatureName = model.Feature?.FeatureName ?? string.Empty,
                FeatureDescription = model.Feature?.Description ?? string.Empty,

                // PackageFeature details
                CreatedAt = model.CreatedAt,

                // Convert Status from string to enum
                Status = Enum.TryParse<PackageFeatureStatus>(model.Status, true, out var status)
                          ? status
                          : PackageFeatureStatus.Inactive
            };
        }

        //Mapper PackageFeatureCreateDto
        public static PackageFeature MapToPackageFeature(this PackageFeatureCreateDto dto, int packageId, int featureId)
        {
            return new PackageFeature
            {
                PackageId = packageId,
                FeatureId = featureId,
                Status = dto.Status.ToString(),
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
