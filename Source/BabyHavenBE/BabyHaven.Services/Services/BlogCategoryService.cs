using BabyHaven.Common;
using BabyHaven.Common.DTOs.BlogCategoryDTOs;
using BabyHaven.Repositories;
using BabyHaven.Repositories.Models;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.Services
{
    public class BlogCategoryService: IBlogCategoryService
    {
        private readonly UnitOfWork _unitOfWork;
        public BlogCategoryService()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IServiceResult> GetAll()
        {
            var blogCategories = await _unitOfWork.BlogCategoryRepository.GetAllAsync();

            if (blogCategories == null || !blogCategories.Any())
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG,
                    new List<BlogCategoryViewAllDto>());
            }
            else
            {
                var blogCategoryDtos = blogCategories
                    .Select(blogCategories => blogCategories.MapToBlogCategoryAPIResponseDto())
                    .ToList();

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG,
                    blogCategoryDtos);
            }
        }

        public async Task<IQueryable<BlogCategoryAPIResponseDto>> GetQueryable()
        {
            var blogCategories = await _unitOfWork.BlogCategoryRepository
                .GetAllAsync();

            return blogCategories
                .Select(blogCategories => blogCategories.MapToBlogCategoryAPIResponseDto())
                .AsQueryable();
        }

        public async Task<IServiceResult> GetById(int CategoryId)
        {
            var blogCategory = await _unitOfWork.BlogCategoryRepository.GetByIdAsync(CategoryId);

            if (blogCategory == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG,
                    new BlogCategoryViewDetailsDto());
            }
            else
            {
                var blogCategoryDto = blogCategory.MapToBlogCategoryViewDetailsDto();

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG,
                    blogCategoryDto);
            }
        }

        public async Task<IServiceResult> GetChildCategories(int CategoryId)
        {
            var blogCategory = await _unitOfWork.BlogCategoryRepository.GetListByParentCategoryId(CategoryId);

            if (blogCategory == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            else
            {
                var cateDtos = blogCategory
                    .Select(bc => bc.MapToBlogCategoryViewAllDto())
                    .ToList();

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG,
                    blogCategory);
            }
        }

        public async Task<IServiceResult> Create(BlogCategoryCreateDto categoryDto)
        {
            try
            {
                // Check if the blogcategory exists in the database
                var blogCategory = await _unitOfWork.BlogCategoryRepository.GetByCategoryNameAsync(categoryDto.CategoryName);

                if (blogCategory != null)
                {
                    return new ServiceResult(Const.FAIL_CREATE_CODE, "BlogCategory with the same name already exists.");
                }

                var blogParentId = await _unitOfWork.BlogCategoryRepository.GetByParentCategoryId(categoryDto.ParentCategoryId);

                if (categoryDto.ParentCategoryId != null && blogParentId == null)
                {
                    return new ServiceResult(Const.FAIL_CREATE_CODE, "Parent Category not found.");
                }

                // Map DTO to Entity
                var newBlogCategory = categoryDto.MapToEntity();

                // Save data to database
                var result = await _unitOfWork.BlogCategoryRepository.CreateAsync(newBlogCategory);

                if (result > 0)
                {
                    return new ServiceResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG,
                        newBlogCategory);
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

        public async Task<IServiceResult> Update(BlogCategoryUpdateDto categoryDto)
        {
            try
            {
                // Check if the blogCategory exists in the database
                var blogCategory = await _unitOfWork.BlogCategoryRepository.GetByIdAsync(categoryDto.CategoryId);

                if (blogCategory == null)
                {
                    return new ServiceResult(Const.FAIL_UPDATE_CODE, "BlogCategory not found.");
                }

                //Map DTO to Entity
                categoryDto.MapToBlogCategoryUpdateDto(blogCategory);

                // Update time information
                blogCategory.UpdatedAt = DateTime.UtcNow;

                // Save data to database
                var result = await _unitOfWork.BlogCategoryRepository.UpdateAsync(blogCategory);

                if (result > 0)
                {
                    return new ServiceResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG,
                        blogCategory);
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

        public async Task<IServiceResult> DeleteById(int CategoryId)
        {
            try
            {
                var blogCategory = await _unitOfWork.BlogCategoryRepository.GetByIdAsync(CategoryId);

                if (blogCategory == null)
                {
                    return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG,
                        new BlogCategoryDeleteDto());
                }
                else
                {
                    var deleteBlogCategoryDto = blogCategory.MapToBlogCategoryDeleteDto();

                    var result = await _unitOfWork.BlogCategoryRepository.RemoveAsync(blogCategory);

                    if (result)
                    {
                        return new ServiceResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG,
                            deleteBlogCategoryDto);
                    }
                    else
                    {
                        return new ServiceResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG,
                            deleteBlogCategoryDto);
                    }
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }
    }
}
