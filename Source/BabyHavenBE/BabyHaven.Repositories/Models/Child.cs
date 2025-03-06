﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BabyHaven.Repositories.Models;

public partial class Child
{
    [Key]
    public Guid ChildId { get; set; }

    public Guid MemberId { get; set; }

    public string Name { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public string Gender { get; set; }

    public double? BirthWeight { get; set; }

    public double? BirthHeight { get; set; }

    public string BloodType { get; set; }

    public string Allergies { get; set; }

    public string Notes { get; set; }

    public string RelationshipToMember { get; set; }

    public string Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<ChildMilestone> ChildMilestones { get; set; } = new List<ChildMilestone>();

    public virtual ICollection<ConsultationRequest> ConsultationRequests { get; set; } = new List<ConsultationRequest>();

    public virtual ICollection<GrowthRecord> GrowthRecords { get; set; } = new List<GrowthRecord>();

    public virtual Member Member { get; set; }
}