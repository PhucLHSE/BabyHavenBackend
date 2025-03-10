using BabyHaven.Common.DTOs.BlogDTOs;
using BabyHaven.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.IServices
{
    public interface IBlogService
    {
        Task<IServiceResult> GetAll();
        Task<IQueryable<BlogViewAllDto>> GetQueryable();
        Task<IServiceResult> GetById(int BlogId);
        Task<IServiceResult> GetAllByCategoryId(int categoryId);
        Task<IServiceResult> Create(BlogCreateDto blogCreateDto);
        Task<IServiceResult> Update(BlogUpdateDto blogUpdateDto);
        Task<IServiceResult> DeleteById(int BlogId);
    }
}
