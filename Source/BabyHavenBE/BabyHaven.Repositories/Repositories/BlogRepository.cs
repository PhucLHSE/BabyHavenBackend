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
    public class BlogRepository:GenericRepository<Blog>
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
            var blog = await _context.Blogs
                        .Include(b => b.Author)
                        .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.BlogId == BlogId);

            return blog;
        }
        public async Task<Blog> GetByIdBlogAsync(Guid AuthorId, int CategoryId)
        {
            var blog = await _context.Blogs
                .Include(b => b.Author)
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.AuthorId == AuthorId && b.CategoryId == CategoryId);

            return blog;
        }
    }
}
