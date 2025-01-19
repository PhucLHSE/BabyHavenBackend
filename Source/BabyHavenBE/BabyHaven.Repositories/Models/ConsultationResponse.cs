﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace BabyHaven.Repositories.Models;

public partial class ConsultationResponse
{
    public int ResponseId { get; set; }

    public int RequestId { get; set; }

    public int DoctorId { get; set; }

    public DateTime ResponseDate { get; set; }

    public string Content { get; set; }

    public string Attachments { get; set; }

    public bool? IsHelpful { get; set; }

    public string Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Doctor Doctor { get; set; }

    public virtual ICollection<RatingFeedback> RatingFeedbacks { get; set; } = new List<RatingFeedback>();

    public virtual ConsultationRequest Request { get; set; }
}