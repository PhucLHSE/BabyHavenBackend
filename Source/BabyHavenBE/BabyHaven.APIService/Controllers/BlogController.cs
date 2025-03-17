using BabyHaven.Common;
using BabyHaven.Common.DTOs.BlogDTOs;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace BabyHaven.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
            => _blogService = blogService;

        // GET: api/<BlogController>
        [HttpGet]
        public async Task<IServiceResult> Get()
        {
            return await _blogService.GetAll();
        }

        [HttpGet("odata")]
        [EnableQuery]
        public async Task<IQueryable<BlogViewAllDto>> GetForOData()
        {
            return await _blogService.GetQueryable();
        }

        // GET api/<BlogController>/5/3
        [HttpGet("{blogId}")]
        public async Task<IServiceResult> Get(int BlogId)
        {
            return await _blogService.GetById(BlogId);
        }

        [HttpGet("blogs/{categoryId}")]
        public async Task<IServiceResult> GetByCategory(int categoryId)
        {
            return await _blogService.GetAllByCategoryId(categoryId);
        }

        // POST api/<BlogController>
        [HttpPost]
        public async Task<IServiceResult> Post(BlogCreateDto blogCreateDto)
        {
            if (!ModelState.IsValid)
            {

                return new ServiceResult(Const.ERROR_VALIDATION_CODE, 
                    "Validation failed", 
                    ModelState);
            }

            return await _blogService.Create(blogCreateDto);
        }

        // PUT api/<BlogController>/5
        [HttpPut("{blogId}")]
        public async Task<IServiceResult> Put(BlogUpdateDto blogUpdateDto)
        {
            if (!ModelState.IsValid)
            {

                return new ServiceResult(Const.ERROR_VALIDATION_CODE, 
                    "Validation failed",
                    ModelState);
            }

            return await _blogService.Update(blogUpdateDto);
        }

        // DELETE api/<BlogController>/5
        [HttpDelete("{blogId}")]
        public async Task<IServiceResult> Delete(int BlogId)
        {
            return await _blogService.DeleteById(BlogId);
        }

        private bool BlogExists(int BlogId)
        {
            return _blogService.GetById(BlogId) != null;
        }
    }
}
