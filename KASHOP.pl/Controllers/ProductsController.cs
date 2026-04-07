using System.Threading.Tasks;
using Azure;
using KASHOP.bll.Service;
using KASHOP.dal.DTO.Request;
using KASHOP.pl.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace KASHOP.pl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {

        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IProductService _productService;
        public ProductsController(IProductService productService, IStringLocalizer<SharedResource> localizer)
        {
            _productService = productService;
            _localizer = localizer;
        }
        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var response = await _productService.GetAllProducts();

            return Ok(new
            {
                data = response,
                message = _localizer["success"].Value
            });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetProduct(c => c.Id == id);
            if (product == null) return NotFound();
            return Ok(new
            {
                data=product,
                message = _localizer["success"].Value

            });
        }

        [HttpPost]
        [Authorize]

        public async Task<IActionResult> Create([FromForm] ProductRequest request)
        {
            var response = await _productService.CreateProduct(request);
            return Ok(new
            {
                data = response,
                message = _localizer["success"].Value
            });
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _productService.DeleteProduct(id);
            if (!deleted)
            {
                return NotFound(new { message = _localizer["not found"].Value });
            }
            return Ok(new { message = _localizer["success"].Value });

        }
    }
}
