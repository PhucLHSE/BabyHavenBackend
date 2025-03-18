using BabyHaven.Repositories.Base;
using BabyHaven.Repositories.DBContext;
using BabyHaven.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Repositories.Repositories
{
    public class BlogCategoryRepository : GenericRepository<BlogCategory>
    {
        public BlogCategoryRepository()
        {

        }

        public BlogCategoryRepository(SWP391_ChildGrowthTrackingSystemContext context) 
            => _context = context;

        public async Task<BlogCategory?> GetByCategoryNameAsync(string categoryName)
        {
            return await _context.BlogCategories
                .FirstOrDefaultAsync(bc => bc.CategoryName == categoryName);
        }

        public async Task<BlogCategory?> GetByParentCategoryId(int? parentCategoryId)
        {
            return await _context.BlogCategories
                .FirstOrDefaultAsync(bc => bc.CategoryId == parentCategoryId);
        }

        public async Task<List<BlogCategory?>> GetListByParentCategoryId(int? parentCategoryId)
        {
            return await _context.BlogCategories
                .Include(bc => bc.Blogs)
                .Where(bc => bc.ParentCategoryId == parentCategoryId)
                .ToListAsync();
        }

        public async Task<Dictionary<string, int>> GetAllCategoryNameToIdMappingAsync()
        {
            return await _context.BlogCategories
                .ToDictionaryAsync(bc => bc.CategoryName, bc => bc.CategoryId);
        }

        public async Task<BlogCategory> GetByIdBlogCategory(int categoryId)
        {
            return await _context.BlogCategories
                .Where(c => c.CategoryId == categoryId)
                .Include(c => c.Blogs)
                    .ThenInclude(b => b.Author)
                .FirstOrDefaultAsync();
        }
    }
}
