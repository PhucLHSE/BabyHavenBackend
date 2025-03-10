using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common;
using BabyHaven.Common.DTOs.BlogDTOs;
using BabyHaven.Common.DTOs.DoctorSpecializationDTOs;
using BabyHaven.Repositories;
using BabyHaven.Repositories.Models;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Mappers;

namespace BabyHaven.Services.Services
{
    public class BlogService : IBlogService
    {
        private readonly UnitOfWork _unitOfWork;

        public BlogService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<IServiceResult> Create(BlogCreateDto blogCreateDto)
        {
            try
            {
                // Retrieve mappings: AuthorName -> AuthorId and CategoryName -> CategoryId
                var authorNameToIdMapping = await _unitOfWork.UserAccountRepository.GetAllNameToIdMappingAsync();
                var categoryNameToIdMapping = await _unitOfWork.BlogCategoryRepository.GetAllCategoryNameToIdMappingAsync();

                // Check if the provided AuthorName exists
                if (!authorNameToIdMapping.ContainsKey(blogCreateDto.AuthorName))
                {
                    return new ServiceResult(Const.FAIL_CREATE_CODE,
                        $"AuthorName '{blogCreateDto.AuthorName}' does not exist.");
                }

                // Check if the provided CategoryName exists
                if (!categoryNameToIdMapping.ContainsKey(blogCreateDto.CategoryName))
                {
                    return new ServiceResult(Const.FAIL_CREATE_CODE,
                        $"CategoryName '{blogCreateDto.CategoryName}' does not exist.");
                }

                // Get AuthorId and CategoryId from AuthorName and CategoryName
                var authorId = authorNameToIdMapping[blogCreateDto.AuthorName];
                var categoryId = categoryNameToIdMapping[blogCreateDto.CategoryName];

                // Check if the Blog already exists in the database
                var existingBlog = await _unitOfWork.BlogRepository
                    .GetByIdBlogAsync(authorId, categoryId);

                if (existingBlog != null && existingBlog.BlogId > 0)
                {
                    return new ServiceResult(Const.FAIL_CREATE_CODE,
                        "The specified Blog already exists.");
                }

                // Map the DTO to an entity object
                var newBlog = blogCreateDto.MapToBlog(authorId, categoryId);

                // Add timestamp for creation
                newBlog.CreatedAt = DateTime.UtcNow;

                // Save the new entity to the database
                var result = await _unitOfWork.BlogRepository.CreateAsync(newBlog);

                if (result > 0)
                {
                    var responseDto = new BlogCreateDto
                    {
                        Title = blogCreateDto.Title,
                        Content = blogCreateDto.Content,
                        AuthorName = blogCreateDto.AuthorName,
                        CategoryName = blogCreateDto.CategoryName,
                        ImageBlog = blogCreateDto.ImageBlog,
                        Tags = blogCreateDto.Tags,
                        ReferenceSources = blogCreateDto.ReferenceSources
                    };

                    return new ServiceResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG,
                        responseDto);
                }
                else
                {
                    return new ServiceResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IServiceResult> DeleteById(int BlogId)
        {
            try
            {
                // Retrieve the Blog using the provided SpecializationId and DoctorId
                var blog = await _unitOfWork.BlogRepository.GetByIdBlogAsync(BlogId);

                // Check if the Blog exists
                if (blog == null)
                {
                    return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG,
                        new BlogDeleteDto());
                }
                else
                {
                    // Map to BlogDeleteDto for response
                    var deleteBlogDto = blog.MapToBlogDeleteDto();

                    var result = await _unitOfWork.BlogRepository.RemoveAsync(blog);

                    if (result)
                    {
                        return new ServiceResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG,
                            deleteBlogDto);
                    }
                    else
                    {
                        return new ServiceResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG,
                            deleteBlogDto);
                    }
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IServiceResult> GetAll()
        {
            var blogs = await _unitOfWork.BlogRepository.GetAllBlogAsync();
            if (blogs == null || !blogs.Any())
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG,
                    new List<BlogViewAllDto>());
            }
            else
            {
                var blogDtos = blogs
                    .Select(blog => blog.MapToBlogViewAllDto())
                    .ToList();

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG,
                    blogDtos);
            }
        }

        public async Task<IServiceResult> GetById(int BlogId)
        {
            var blog = await _unitOfWork.BlogRepository.GetByIdBlogAsync(BlogId);

            if (blog == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG,
                    new BlogViewDetailsDto());
            }
            else
            {
                var blogDto = blog.MapToBlogViewDetailsDto();

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG,
                    blogDto);
            }
        }

        public async Task<IServiceResult> GetAllByCategoryId(int categoryId)
        {
            var blogs = await _unitOfWork.BlogRepository.GetAllBlogByParentCategoryId(categoryId);

            if (blogs == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG,
                    new BlogViewDetailsDto());
            }
            else
            {
                var blogDtos = blogs
                    .Select(blog => blog.MapToBlogViewAllDto())
                    .ToList();

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG,
                    blogDtos);
            }
        }

        public async Task<IServiceResult> Update(BlogUpdateDto blogUpdateDto)
        {
            try
            {
                // Check if Blog exists
                var blog = await _unitOfWork.BlogRepository
                    .GetByIdAsync(blogUpdateDto.BlogId);

                if (blog == null)
                {
                    return new ServiceResult(Const.FAIL_UPDATE_CODE, "Blog not found.");
                }

                // Call the correct extension method
                blog.MapToUpdatedBlog(blogUpdateDto);

                // Update modification timestamp
                blog.UpdatedAt = DateTime.UtcNow;

                // Save changes to the database
                var result = await _unitOfWork.BlogRepository.UpdateAsync(blog);

                if (result > 0)
                {
                    return new ServiceResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, blog);
                }
                else
                {
                    return new ServiceResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IQueryable<BlogViewAllDto>> GetQueryable()
        {
            var blogs = await _unitOfWork.BlogRepository
                .GetAllAsync();

            return blogs
                .Select(blogs => blogs.MapToBlogViewAllDto())
                .AsQueryable();
        }
    }
}
