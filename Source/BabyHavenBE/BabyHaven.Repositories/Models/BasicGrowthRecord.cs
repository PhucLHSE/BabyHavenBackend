﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace BabyHaven.Repositories.Models;

public partial class BasicGrowthRecord
{
    public int BasicRecordId { get; set; }

    public int RecordId { get; set; }

    public double Weight { get; set; }

    public double Height { get; set; }

    public double? HeadCircumference { get; set; }

    public double? MuscleMass { get; set; }

    public double? ChestCircumference { get; set; }

    public string Notes { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual GrowthRecord Record { get; set; }
}