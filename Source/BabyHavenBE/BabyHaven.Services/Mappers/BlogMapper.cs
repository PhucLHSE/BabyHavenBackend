using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common.DTOs.BlogDTOs;
using BabyHaven.Common.Enum.BlogEnums;
using BabyHaven.Repositories.Models;

namespace BabyHaven.Services.Mappers
{
    public static class BlogMapper
    {
        public static Blog ToEntity(this BlogCreateDto dto, int categoryId)
        {
            return new Blog
            {
                Title = dto.Title,
                Content = dto.Content,
                AuthorId = Guid.NewGuid(), // Assuming AuthorId is generated here
                CategoryId = categoryId,
                ImageBlog = dto.ImageBlog,
                Status = BlogStatus.Approved.ToString(),
                Tags = dto.Tags,
                ReferenceSources = dto.ReferenceSources,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        public static Blog ToEntity(this BlogUpdateDto dto, Blog existingBlog, int categoryId)
        {
            if (!string.IsNullOrEmpty(dto.Title))
                existingBlog.Title = dto.Title;

            if (!string.IsNullOrEmpty(dto.Content))
                existingBlog.Content = dto.Content;

            existingBlog.CategoryId = categoryId;

            if (!string.IsNullOrEmpty(dto.ImageBlog))
                existingBlog.ImageBlog = dto.ImageBlog;

            if (dto.Status.HasValue)
                existingBlog.Status = dto.Status.ToString();

            if (!string.IsNullOrEmpty(dto.RejectionReason))
                existingBlog.RejectionReason = dto.RejectionReason;

            if (!string.IsNullOrEmpty(dto.Tags))
                existingBlog.Tags = dto.Tags;

            if (!string.IsNullOrEmpty(dto.ReferenceSources))
                existingBlog.ReferenceSources = dto.ReferenceSources;

            existingBlog.UpdatedAt = DateTime.UtcNow;

            return existingBlog;
        }
    }
}
