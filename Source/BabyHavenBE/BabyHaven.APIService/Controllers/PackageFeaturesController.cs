﻿using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BabyHaven.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageFeaturesController : ControllerBase
    {
        private readonly IPackageFeatureService _packageFeatureService;

        public PackageFeaturesController(IPackageFeatureService packageFeatureService)
            => _packageFeatureService = packageFeatureService;


        // GET: api/<PackageFeaturesController>
        [HttpGet]
        public async Task<IServiceResult> Get()
        {
            return await _packageFeatureService.GetAll();
        }

        // GET api/<FeaturesController>/5/3
        [HttpGet("{PackageId}/{FeatureId}")]
        public async Task<IServiceResult> Get(int PackageId, int FeatureId)
        {
            return await _packageFeatureService.GetById(PackageId, FeatureId);
        }
    }
}
