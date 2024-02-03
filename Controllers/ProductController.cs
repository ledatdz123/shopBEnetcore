using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shopEcomerceExBE.Service.CacheService;
using shopEcomerceExBE.Service.ProductService;

namespace shopEcomerceExBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICacheService _cacheService;
        public ProductController(IProductService productService, ICacheService cacheService)
        {
            _productService = productService;
            _cacheService = cacheService;
        }
        [HttpGet]
        [Route("get_product")]
        public async Task<IActionResult> Test()
        {
            var product=_productService.GetProduct();
            return Ok(product);
        }
        [HttpGet]
        [Route("get_all_product")]
        public async Task<IActionResult> GetAllProduct()
        {
            var product = _productService.GetAllProduct();
            return Ok(product);
        }
        [HttpGet]
        [Route("get_productid")]
        public async Task<IActionResult> TestProduct(int productid)
        {
            var product = _productService.Test(productid);
            return Ok(product);
        }
        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> SearchProduct(string? productname, string? description)
        {
            var product = _productService.SearchProduct(productname, description);
            return Ok(product);
        }
    }
}
