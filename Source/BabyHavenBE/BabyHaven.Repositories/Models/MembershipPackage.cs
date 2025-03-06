﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BabyHaven.Repositories.Models;

public partial class MembershipPackage
{
    [Key]
    public int PackageId { get; set; }

    public string PackageName { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public string Currency { get; set; }

    public int DurationMonths { get; set; }

    public int? TrialPeriodDays { get; set; }

    public int MaxChildrenAllowed { get; set; }

    public string SupportLevel { get; set; }

    public string Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<MemberMembership> MemberMemberships { get; set; } = new List<MemberMembership>();

    public virtual ICollection<PackageFeature> PackageFeatures { get; set; } = new List<PackageFeature>();

    public virtual ICollection<PackagePromotion> PackagePromotions { get; set; } = new List<PackagePromotion>();
}