using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StoreManagement.Core.Dto;
using StoreManagement.Interface;
using StoreManagement.Web.Models.Product;
using StoreManagement.Web.Models.Store;


namespace StoreManagement.Web.Controllers
{
    public class ManagementController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStoreService _storeService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ManagementController(IStoreService storeService,
                                    IProductService productService,
                                    ILogger<HomeController> logger,
                                    IMapper mapper)
        {
            _logger = logger;
            _productService = productService;
            _storeService = storeService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all store
        /// </summary>
        /// <returns></returns>
        [Route("management/indexasync")]
        public async Task<ActionResult> IndexAsync()
        {
            var stores = await _storeService.GetAll()
                .ConfigureAwait(false);

            if (stores != null)
            {
                var storesModel = _mapper.Map<List<StoreDto>, List<StoreVM>>(stores);
                return View("IndexAsync", storesModel);
            }
            return View("IndexAsync", null);
        }

        /// <summary>
        /// Get the products of the store
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="storeId"></param>
        /// <returns></returns>
        [Route("management/productsofstoreasync")]
        public async Task<ActionResult> ProductsOfStoreAsync(List<int> ids, int storeId)
        {
            var products = await _productService.GetAllByStoreId(storeId)
                                              .ConfigureAwait(false);

            if (products != null)
            {
                var storesModel = _mapper.Map<List<ProductDto>, List<ProductVM>>(products);
                return View("ProductsOfStoreAsync", new ProductsVM(storesModel, storeId));
            }
            return View("ProductsOfStoreAsync", null);
        }

        /// <summary>
        /// Add new product to the store
        /// </summary>
        /// <param name="productVM"></param>
        /// <returns></returns>
        [Route("management/createproduct")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateProduct(CreateProductVM productVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { error = true, message = "model is not valid" });
                }

                if (productVM == null)
                {
                    return Json(new { error = true, message = "model is null" });
                }

                // TODO: add transaction
                var product = await _productService.AddAsync(_mapper.Map<ProductDto>(productVM), productVM.StoreId)
                                        .ConfigureAwait(false);

                if (product == null)
                {
                    return Json(new { error = true, message = "product is null" });
                }

                return Json(new { error = false, 
                    name = product.Name, 
                    description = product.Description,
                    productId = product.ProductId
                });
            }
            catch(Exception ex)
            {
                _logger.LogError("CreateProduct fail", ex);
                return Json(new { error = true, message = $"{ex?.ToString()}" });
            }
        }

        /// <summary>
        /// Edit product in the store
        /// </summary>
        /// <param name="productVM"></param>
        /// <returns></returns>
        [Route("management/editproduct")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditProduct(EditProductVM productVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { error = true, message = "model is not valid" });
                }

                if (productVM == null)
                {
                    return Json(new { error = true, message = "model is null" });
                }
                var result = await _productService.UpdateAsync(_mapper.Map<ProductDto>(productVM))
                    .ConfigureAwait(false);

                if (!result)
                {
                    return Json(new { error = true, message = "product is not modified" });
                }

                return Json(new
                {
                    error = false
                });
            }
            catch(Exception ex)
            {
                _logger.LogError("EditProduct fail", ex);
                return Json(new { error = true, message = $"{ex?.ToString()}" });
            }
        }

        /// <summary>
        /// Delete product from the store
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="storeId"></param>
        /// <returns></returns>
        [Route("management/deleteproduct")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int productId, int storeId)
        {
            try
            {
                if (productId > 0 && storeId > 0)
                {
                    var result = await _productService.DeleteAsync(new ProductDto { ProductId = productId })
                        .ConfigureAwait(false);

                    if (!result)
                    {
                        return Json(new { error = true, message = "product is not modified" });
                    }

                    return Json(new
                    {
                        error = false
                    });
                }
                return Json(new { error = true, message = $"productId = {productId}, storeId = {storeId} is null" });
            }
            catch (Exception ex)
            {
                _logger.LogError("EditProduct fail", ex);
                return Json(new { error = true, message = $"{ex?.ToString()}" });
            }
        }
    }
}