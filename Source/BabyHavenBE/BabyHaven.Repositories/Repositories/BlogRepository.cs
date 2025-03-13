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
    public class BlogRepository : GenericRepository<Blog>
    {
        public BlogRepository() { }
        public BlogRepository(SWP391_ChildGrowthTrackingSystemContext context) => _context = context;

        public async Task<List<Blog>> GetAllBlogAsync()
        {
            var blogs = await _context.Blogs
                .Include(b => b.Author)
                .Include(b => b.Category)
                .ToListAsync();

            return blogs;
        }

        public async Task<Blog> GetByIdBlogAsync(int BlogId)
        {
            return await _context.Blogs
                .Include(b => b.Author)
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.BlogId == BlogId);
        }

        public async Task<Blog> GetByEmail(string email)
        {
            return await _context.Blogs
                .Include(b => b.Author)
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.Author.Email == email);
        }

        public async Task<Blog> GetByIdBlogAsync(Guid AuthorId, int CategoryId)
        {
            return await _context.Blogs
                .Include(b => b.Author)
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.AuthorId == AuthorId && b.CategoryId == CategoryId);
        }

        public async Task<List<Blog>> GetAllBlogByParentCategoryId(int parentCategoryId)
        {
            var categoryIds = await _context.BlogCategories
                .Where(c => c.ParentCategoryId == parentCategoryId)
                .Select(c => c.CategoryId)
                .ToListAsync();

            return await _context.Blogs
                .Include(b => b.Category)
                .Where(b => categoryIds.Contains(b.CategoryId))
                .ToListAsync();
        }

        public async Task<Blog> GetByTitle(string title, string email)
        {
            return await _context.Blogs
                .Include(b => b.Author)
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.Author.Email == email && b.Title == title);
        }
    }
}
