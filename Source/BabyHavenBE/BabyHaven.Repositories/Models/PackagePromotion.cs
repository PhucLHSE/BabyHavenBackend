﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BabyHaven.Repositories.Models;

public partial class PackagePromotion
{
    [Key]
    public int PackageId { get; set; }

    [Key]
    public Guid PromotionId { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual MembershipPackage Package { get; set; }

    public virtual Promotion Promotion { get; set; }
}