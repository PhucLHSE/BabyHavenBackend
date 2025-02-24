using BabyHaven.Common;
using BabyHaven.Common.DTOs.BlogDTOs;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using Microsoft.AspNetCore.Mvc;

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

        // GET api/<BlogController>/5
        [HttpGet("{id:int}")]
        public async Task<IServiceResult> Get(int id)
        {
            return await _blogService.GetById(id);
        }

        // POST api/<BlogController>
        [HttpPost]
        public async Task<IServiceResult> Post(BlogCreateDto blogCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return new ServiceResult(Const.ERROR_VALIDATION_CODE, "Validation failed", ModelState);
            }

            return await _blogService.Create(blogCreateDto);
        }

        // PUT api/<BlogController>/5
        [HttpPut("{id:int}")]
        public async Task<IServiceResult> Put(int id, BlogUpdateDto blogUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return new ServiceResult(Const.ERROR_VALIDATION_CODE, "Validation failed", ModelState);
            }

            if (id != blogUpdateDto.BlogId)
            {
                return new ServiceResult(Const.ERROR_VALIDATION_CODE, "Blog ID mismatch");
            }

            return await _blogService.Update(blogUpdateDto);
        }

        // DELETE api/<BlogController>/5
        [HttpDelete("{id:int}")]
        public async Task<IServiceResult> Delete(int id)
        {
            return await _blogService.DeleteById(id);
        }

        private bool BlogExists(int id)
        {
            return _blogService.GetById(id) != null;
        }
    }
}
