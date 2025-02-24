using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common;
using BabyHaven.Common.DTOs.BlogDTOs;
using BabyHaven.Repositories;
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
            var cate = await _unitOfWork.BlogCategoryRepository.GetByCategoryNameAsync(blogCreateDto.CategoryName);

            if (cate == null)
            {
                return new ServiceResult(Const.FAIL_READ_CODE, "Category name not found");
            }

            var blog = blogCreateDto.ToEntity(cate.CategoryId);
            await _unitOfWork.BlogRepository.CreateAsync(blog);

            return new ServiceResult(Const.SUCCESS_CREATE_CODE, "Blog created successfully");
        }

        public async Task<IServiceResult> DeleteById(int blogId)
        {
            var blog = await _unitOfWork.BlogRepository.GetByIdAsync(blogId);
            if (blog == null)
            {
                return new ServiceResult(Const.FAIL_READ_CODE, "Blog not found");
            }

            await _unitOfWork.BlogRepository.RemoveAsync(blog);

            return new ServiceResult(Const.SUCCESS_DELETE_CODE, "Blog deleted successfully");
        }

        public async Task<IServiceResult> GetAll()
        {
            var blogs = await _unitOfWork.BlogRepository.GetAllAsync();
            var blogDtos = blogs.Select(blog => blog);

            return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, blogDtos);
        }

        public async Task<IServiceResult> GetById(int blogId)
        {
            var blog = await _unitOfWork.BlogRepository.GetByIdAsync(blogId);
            if (blog == null)
            {
                return new ServiceResult(Const.FAIL_READ_CODE, "Blog not found");
            }

            return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, blog);
        }

        public async Task<IServiceResult> Update(BlogUpdateDto blogUpdateDto)
        {
            var blog = await _unitOfWork.BlogRepository.GetByIdAsync(blogUpdateDto.BlogId);
            if (blog == null)
            {
                return new ServiceResult(Const.FAIL_READ_CODE, "Blog not found");
            }

            var cate = await _unitOfWork.BlogCategoryRepository.GetByCategoryNameAsync(blogUpdateDto.CategoryName);
            if (cate == null)
            {
                return new ServiceResult(Const.FAIL_READ_CODE, "Category name not found");
            }

            blogUpdateDto.ToEntity(blog, cate.CategoryId);
            await _unitOfWork.BlogRepository.UpdateAsync(blog);

            return new ServiceResult(Const.SUCCESS_UPDATE_CODE, "Blog updated successfully", blog);
        }
    }
}
