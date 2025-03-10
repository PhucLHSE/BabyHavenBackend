using BabyHaven.Common.DTOs.RoleDTOs;
using BabyHaven.Common;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BabyHaven.Common.DTOs.BlogCategoryDTOs;
using Microsoft.AspNetCore.OData.Query;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BabyHaven.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogCategoriesController : ControllerBase
    {
        private readonly IBlogCategoryService _blogCategoryService;

        public BlogCategoriesController(IBlogCategoryService blogCategoryService)
            => _blogCategoryService = blogCategoryService;


        // GET: api/<BlogCategoriesController>
        [HttpGet]
        public async Task<IServiceResult> Get()
        {
            return await _blogCategoryService.GetAll();
        }

        [HttpGet("odata")]
        [EnableQuery]
        public async Task<IQueryable<BlogCategoryAPIResponseDto>> GetForOData()
        {
            return await _blogCategoryService.GetQueryable();
        }

        // GET api/<BlogCategoriesController>/5
        [HttpGet("{id}")]
        public async Task<IServiceResult> Get(int id)
        {
            return await _blogCategoryService.GetById(id);
        }

        [HttpGet("parent-categories/{id}")]
        public async Task<IServiceResult> GetChildCategory(int id)
        {
            return await _blogCategoryService.GetChildCategories(id);
        }

        // POST api/<BlogCategoriesController>
        [HttpPost]
        public async Task<IServiceResult> Post(BlogCategoryCreateDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return new ServiceResult(Const.ERROR_VALIDATION_CODE, "Validation failed", ModelState);
            }

            return await _blogCategoryService.Create(categoryDto);
        }

        // PUT api/<BlogCategoriesController>/5
        [HttpPut("{id}")]
        public async Task<IServiceResult> Put(BlogCategoryUpdateDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return new ServiceResult(Const.ERROR_VALIDATION_CODE, "Validation failed", ModelState);
            }

            return await _blogCategoryService.Update(categoryDto);
        }

        // DELETE api/<BlogCategoriesController>/5
        [HttpDelete("{id}")]
        public async Task<IServiceResult> Delete(int id)
        {
            return await _blogCategoryService.DeleteById(id);
        }
    }
}
