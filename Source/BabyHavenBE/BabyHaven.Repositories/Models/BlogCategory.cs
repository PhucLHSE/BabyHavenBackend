﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace BabyHaven.Repositories.Models;

public partial class BlogCategory
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; }

    public string Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsActive { get; set; }

    public int? ParentCategoryId { get; set; }

    public string ThumbnailImage { get; set; }

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    public virtual ICollection<BlogCategory> InverseParentCategory { get; set; } = new List<BlogCategory>();

    public virtual BlogCategory ParentCategory { get; set; }
}