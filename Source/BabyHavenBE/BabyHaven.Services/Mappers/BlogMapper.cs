using BabyHaven.Common.DTOs.BlogDTOs;
using BabyHaven.Common.Enum.BlogEnums;
using BabyHaven.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


namespace BabyHaven.Services.Mappers
{
    public static class BlogMapper
    {
        // Mapper BlogViewAllDto
        public static BlogViewAllDto MapToBlogViewAllDto(this Blog model)
        {
            return new BlogViewAllDto
            {
                Title = model.Title,
                Content = model.Content,
                ImageBlog = model.ImageBlog,
                Tags = model.Tags,
                // Author
                AuthorName = model.Author?.Name ?? string.Empty,

                // Feature
                CategoryName = model.Category?.CategoryName ?? string.Empty,

                // Convert Status from string to enum
                Status = Enum.TryParse<BlogStatus>(model.Status, true, out var status)
                          ? status
                          : BlogStatus.Approved
            };
        }

        // Mapper BlogViewDetailsDto
        public static BlogViewDetailsDto MapToBlogViewDetailsDto(this Blog model)
        {
            return new BlogViewDetailsDto
            {
                //Blog details
                Title= model.Title,
                Content= model.Content,
                ImageBlog = model.ImageBlog,
                RejectionReason = model.RejectionReason,
                Tags = model.Tags,
                ReferenceSources = model.ReferenceSources,

                // Convert Status from string to enum
                Status = Enum.TryParse<BlogStatus>(model.Status, true, out var status)
                          ? status
                          : BlogStatus.Approved,

                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,

                // Author details
                Username = model.Author?.Username ?? string.Empty,
                Email = model.Author?.Email ?? string.Empty,
                PhoneNumber = model.Author?.PhoneNumber ?? string.Empty,
                Name = model.Author?.Name ?? string.Empty,
                Gender = model.Author?.Gender ?? string.Empty,
                Address = model.Author?.Address ?? string.Empty,
                Password = model.Author?.Password ?? string.Empty,
                VerificationCode = model.Author?.VerificationCode ?? string.Empty,

                // Category details
                CategoryName = model.Category?.CategoryName ?? string.Empty,
                Description = model.Category?.Description ?? string.Empty,
                IsActive = model.Category?.IsActive ?? false,
                ParentCategoryId = model.Category?.ParentCategoryId 

            };
        }

        //Mapper BlogCreateDto
        public static Blog MapToBlog(this BlogCreateDto dto, Guid AuthorId, int CategoryId)
        {
            return new Blog
            {
                AuthorId = AuthorId,
                CategoryId = CategoryId,
                Title = dto.Title,
                Content = dto.Content,
                ImageBlog = dto.ImageBlog,
                Tags = dto.Tags,
                ReferenceSources = dto.ReferenceSources,
                // Convert Status from string to enum
                Status = dto.Status.ToString()
            };
        }

        //Mapper BlogUpdateDto
        public static void MapToUpdatedBlog(this Blog blog, BlogUpdateDto updateDto)
        {
            if (!string.IsNullOrWhiteSpace(updateDto.Title))
                blog.Title = updateDto.Title;

            if (!string.IsNullOrWhiteSpace(updateDto.Content))
                blog.Content = updateDto.Content;

            if (!string.IsNullOrWhiteSpace(updateDto.ImageBlog))
                blog.ImageBlog = updateDto.ImageBlog;

            if (updateDto.Status.HasValue)
                blog.Status = updateDto.Status.ToString();

            if (!string.IsNullOrWhiteSpace(updateDto.RejectionReason))
                blog.RejectionReason = updateDto.RejectionReason;

            if (!string.IsNullOrWhiteSpace(updateDto.Tags))
                blog.Tags = updateDto.Tags;

            if (!string.IsNullOrWhiteSpace(updateDto.ReferenceSources))
                blog.ReferenceSources = updateDto.ReferenceSources;


        }

        //Mapper BlogDeleteDto
        public static BlogDeleteDto MapToBlogDeleteDto(this Blog model)
        {
            return new BlogDeleteDto
            {
                //Blog details
                Title = model.Title,
                Content = model.Content,
                ImageBlog = model.ImageBlog,
                RejectionReason = model.RejectionReason,
                Tags = model.Tags,
                ReferenceSources = model.ReferenceSources,
                // Blog details
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,

                // Blog status
                Status = Enum.TryParse<BlogStatus>(model.Status, true, out var status)
                          ? status
                          : BlogStatus.Approved,

                //UserAccount details
                Username = model.Author?.Username ?? string.Empty,
                Email = model.Author?.Email ?? string.Empty,
                PhoneNumber = model.Author?.PhoneNumber ?? string.Empty,
                Name = model.Author?.Name ?? string.Empty,
                Gender = model.Author?.Gender ?? string.Empty,
                Address = model.Author?.Address ?? string.Empty,

                //BlogCategory details
                CategoryName = model.Category?.CategoryName ?? string.Empty,
                Description = model.Category?.Description ?? string.Empty,
                IsActive = model.Category?.IsActive ?? false,
                ParentCategoryId = model.Category?.ParentCategoryId 

            };
        }
        }

}
