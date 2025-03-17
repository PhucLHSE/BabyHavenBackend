using BabyHaven.Common;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using BabyHaven.Common.DTOs.RoleDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OData.Query;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BabyHaven.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
            => _roleService = roleService;


        // GET: api/<RolesController>
        [HttpGet]
        [Authorize(Roles = "3")]
        public async Task<IServiceResult> Get()
        {
            return await _roleService.GetAll();
        }

        [HttpGet("odata")]
        [EnableQuery]
        public async Task<IQueryable<RoleViewAllDto>> GetForOData()
        {
            return await _roleService.GetQueryable();
        }

        // GET api/<RolesController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "3")]
        public async Task<IServiceResult> Get(int id)
        {
            return await _roleService.GetById(id);
        }

        // POST api/<RolesController>
        [HttpPost]
        [Authorize(Roles = "3")]
        public async Task<IServiceResult> Post(RoleCreateDto roleCreateDto)
        {
            if (!ModelState.IsValid)
            {

                return new ServiceResult(Const.ERROR_VALIDATION_CODE,
                    "Validation failed",
                    ModelState);
            }

            return await _roleService.Create(roleCreateDto);
        }

        // PUT api/<RolesController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "3")]
        public async Task<IServiceResult> Put(RoleUpdateDto roleUpdateDto)
        {
            if (!ModelState.IsValid)
            {

                return new ServiceResult(Const.ERROR_VALIDATION_CODE,
                    "Validation failed",
                    ModelState);
            }

            return await _roleService.Update(roleUpdateDto);
        }

        // DELETE api/<RolesController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "3")]
        public async Task<IServiceResult> Delete(int id)
        {
            return await _roleService.DeleteById(id);
        }

        private bool RoleExists(int id)
        {
            return _roleService.GetById(id) != null;
        }
    }
}
