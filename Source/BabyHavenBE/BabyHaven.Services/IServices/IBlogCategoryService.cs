using BabyHaven.Common.DTOs.BlogCategoryDTOs;
using BabyHaven.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.IServices
{
    public interface IBlogCategoryService
    {
        Task<IServiceResult> GetAll();

        Task<IQueryable<BlogCategoryAPIResponseDto>> GetQueryable();

        Task<IServiceResult> GetById(int CategoryId);

        Task<IServiceResult> GetChildCategories(int CategoryId);

        Task<IServiceResult> Create(BlogCategoryCreateDto categoryDto);

        Task<IServiceResult> Update(BlogCategoryUpdateDto categoryDto);

        Task<IServiceResult> DeleteById(int CategoryId);
    }
}
