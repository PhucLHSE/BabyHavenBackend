﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BabyHaven.Repositories.Models;

public partial class Feature
{
    [Key]
    public int FeatureId { get; set; }

    public string FeatureName { get; set; }

    public string Description { get; set; }

    public string Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<PackageFeature> PackageFeatures { get; set; } = new List<PackageFeature>();
}