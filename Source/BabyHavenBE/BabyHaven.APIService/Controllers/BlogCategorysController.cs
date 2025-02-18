using BabyHaven.Common.DTOs.RoleDTOs;
using BabyHaven.Common;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BabyHaven.Common.DTOs.BlogCategoryDTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BabyHaven.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogCategorysController : ControllerBase
    {
        private readonly IBlogCategoryService _blogCategoryService;

        public BlogCategorysController(IBlogCategoryService blogCategoryService)
            => _blogCategoryService = blogCategoryService;


        // GET: api/<BlogCategorysController>
        [HttpGet]
        public async Task<IServiceResult> Get()
        {
            return await _blogCategoryService.GetAll();
        }

        // GET api/<BlogCategorysController>/5
        [HttpGet("{id}")]
        public async Task<IServiceResult> Get(int id)
        {
            return await _blogCategoryService.GetById(id);
        }

        // POST api/<BlogCategorysController>
        [HttpPost]
        public async Task<IServiceResult> Post(BlogCategoryCreateDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return new ServiceResult(Const.ERROR_VALIDATION_CODE, "Validation failed", ModelState);
            }

            return await _blogCategoryService.Create(categoryDto);
        }

        // PUT api/<BlogCategorysController>/5
        [HttpPut("{id}")]
        public async Task<IServiceResult> Put(BlogCategoryUpdateDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return new ServiceResult(Const.ERROR_VALIDATION_CODE, "Validation failed", ModelState);
            }

            return await _blogCategoryService.Update(categoryDto);
        }

        // DELETE api/<BlogCategorysController>/5
        [HttpDelete("{id}")]
        public async Task<IServiceResult> Delete(int id)
        {
            return await _blogCategoryService.DeleteById(id);
        }

        private bool BlogCategoryExists(int id)
        {
            return _blogCategoryService.GetById(id) != null;
        }
    }
}
