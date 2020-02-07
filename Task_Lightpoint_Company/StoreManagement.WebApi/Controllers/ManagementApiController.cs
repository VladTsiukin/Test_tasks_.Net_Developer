using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StoreManagement.Interface;

namespace StoreManagement.WebApi.Controllers
{
    [ApiController]
    [Route("api/management")]
    public class ManagementApiController : ControllerBase
    {
        private readonly ILogger<ManagementApiController> _logger;
        private readonly IStoreService _storeService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ManagementApiController(IStoreService storeService,
                                       IProductService productService,
                                       ILogger<ManagementApiController> logger,
                                       IMapper mapper)
        {
            _logger = logger;
            _productService = productService;
            _storeService = storeService;
            _mapper = mapper;
        }

        [HttpGet]
        public string Index()
        {
            return "WEB API ASP CORE 3: StoreManagement.WebApi";
        }

        [Route("stores")]
        [HttpGet]
        public async Task<dynamic> Get()
        {
            var stores = await _storeService.GetAll()
                .ConfigureAwait(false);

            if (stores != null)
            {
                return new
                {
                    error = false,
                    stores = stores
                };
            }

            return new
            {
                error = true,
                message = "stores is null"
            };
        }
    }
}
