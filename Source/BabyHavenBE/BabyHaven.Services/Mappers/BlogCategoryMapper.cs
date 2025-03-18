using BabyHaven.Common.DTOs.BlogCategoryDTOs;
using BabyHaven.Common.DTOs.BlogDTOs;
using BabyHaven.Common.DTOs.RoleDTOs;
using BabyHaven.Common.Enum.BlogEnums;
using BabyHaven.Common.Enum.MemberEnums;
using BabyHaven.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.Mappers
{
    public static class BlogCategoryMapper
    {
        // Mapper BlogCategoryViewAllDto
        public static BlogCategoryViewAllDto MapToBlogCategoryViewAllDto(this BlogCategory model)
        {
            return new BlogCategoryViewAllDto
            {
                CategoryName = model.CategoryName,
                IsActive = model.IsActive,
                ParentCategoryId = model.ParentCategoryId
              
            };
        }

        // Mapper BlogCategoryViewDetailsDto
        public static BlogCategoryViewDetailsDto MapToBlogCategoryViewDetailsDto(this BlogCategory model)
        {
            return new BlogCategoryViewDetailsDto
            {
                CategoryName = model.CategoryName,
                Description = model.Description,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,
                IsActive = model.IsActive,
                ParentCategoryId = model.ParentCategoryId,
                Blogs = model.Blogs?.Select(blog => new BlogViewAllDto
                {
                    BlogId = blog.BlogId,
                    AuthorId = blog.AuthorId,
                    Title = blog.Title,
                    Content = blog.Content,
                    AuthorName = blog.Author?.Name ?? "Unknown",
                    CategoryName = model.CategoryName, // Hoặc blog.Category?.CategoryName
                    Tags = blog.Tags,
                    ImageBlog = blog.ImageBlog,

                    // Convert Status from string to enum
                    Status = Enum.TryParse<BlogStatus>(blog.Status, true, out var status)
                          ? status
                          : BlogStatus.Approved,

                    CreatedAt = blog.CreatedAt,
                    UpdatedAt = blog.UpdatedAt
                }).ToList() ?? new()
            };
        }

        // Mapper BlogCategoryAPIResponseDto
        public static BlogCategoryAPIResponseDto MapToBlogCategoryAPIResponseDto(this BlogCategory model)
        {
            return new BlogCategoryAPIResponseDto
            {
                CategoryId = model.CategoryId,
                CategoryName = model.CategoryName,
                Description = model.Description,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,
                IsActive = model.IsActive,
                ParentCategoryId = model.ParentCategoryId
            };
        }

        //Mapper BlogCategoryUpdateDto
        public static void MapToBlogCategoryUpdateDto(this BlogCategoryUpdateDto updateDto, BlogCategory blogCategory)
        {
            if (!string.IsNullOrWhiteSpace(updateDto.CategoryName))
                blogCategory.CategoryName = updateDto.CategoryName;

            if (!string.IsNullOrWhiteSpace(updateDto.Description))
                blogCategory.Description = updateDto.Description;

            // Gán trực tiếp vì IsActive là kiểu bool
            blogCategory.IsActive = updateDto.IsActive;

            if (updateDto.ParentCategoryId.HasValue)
                blogCategory.ParentCategoryId = updateDto.ParentCategoryId.Value;
        
        }

        // Mapper BlogCategoryDeleteDto
        public static BlogCategoryDeleteDto MapToBlogCategoryDeleteDto(this BlogCategory model)
        {
            return new BlogCategoryDeleteDto
            {
                CategoryName = model.CategoryName,
                Description = model.Description,
                IsActive = model.IsActive,
                ParentCategoryId = model.ParentCategoryId,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt
            };
        }
        //Mapper BlogCategoryCreateDto
        public static BlogCategory MapToEntity(this BlogCategoryCreateDto dto)
        {
            return new BlogCategory
            {
                CategoryName = dto.CategoryName,
                Description = dto.Description,
                IsActive = dto.IsActive,
                ParentCategoryId = dto.ParentCategoryId
            };
        }
    }
}
