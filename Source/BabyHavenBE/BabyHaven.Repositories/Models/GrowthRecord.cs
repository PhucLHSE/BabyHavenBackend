﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace BabyHaven.Repositories.Models;

public partial class GrowthRecord
{
    public int RecordId { get; set; }

    public Guid ChildId { get; set; }

    public Guid RecordedBy { get; set; }

    public double Weight { get; set; }

    public double Height { get; set; }

    public double? HeadCircumference { get; set; }

    public double? MuscleMass { get; set; }

    public double? ChestCircumference { get; set; }

    public string NutritionalStatus { get; set; }

    public double? FerritinLevel { get; set; }

    public double? Triglycerides { get; set; }

    public double? BloodSugarLevel { get; set; }

    public string PhysicalActivityLevel { get; set; }

    public int? HeartRate { get; set; }

    public double? BloodPressure { get; set; }

    public double? BodyTemperature { get; set; }

    public double? OxygenSaturation { get; set; }

    public double? SleepDuration { get; set; }

    public string Vision { get; set; }

    public string Hearing { get; set; }

    public string ImmunizationStatus { get; set; }

    public string MentalHealthStatus { get; set; }

    public double? GrowthHormoneLevel { get; set; }

    public string AttentionSpan { get; set; }

    public string NeurologicalReflexes { get; set; }

    public string DevelopmentalMilestones { get; set; }

    public string Notes { get; set; }

    public string Status { get; set; }

    public bool? Verified { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Alert> Alerts { get; set; } = new List<Alert>();

    public virtual Child Child { get; set; }

    public virtual UserAccount RecordedByNavigation { get; set; }
}