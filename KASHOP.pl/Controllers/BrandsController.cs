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
    public class BrandsController : Controller
    {

            private readonly IStringLocalizer<SharedResource> _localizer;
            private readonly IBrandService _brandService;
            public BrandsController(IBrandService brandService, IStringLocalizer<SharedResource> localizer)
            {
                _brandService = brandService;
                _localizer = localizer;
            }
            [HttpGet]

            public async Task<IActionResult> Index()
            {
                var response = await _brandService.GetAllBrands();

                return Ok(new
                {
                    data = response,
                    message = _localizer["success"].Value
                });
            }
            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(int id)
            {
                var brand = await _brandService.GetBrand(c => c.Id == id);
                if (brand == null) return NotFound();
                return Ok(new
                {
                    data = brand,
                    message = _localizer["success"].Value

                });
            }

            [HttpPost]
            [Authorize]

            public async Task<IActionResult> Create([FromForm] BrandRequest request)
            {
                var response = await _brandService.CreateBrand(request);
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
                var deleted = await _brandService.DeleteBrand(id);
                if (!deleted)
                {
                    return NotFound(new { message = _localizer["not found"].Value });
                }
                return Ok(new { message = _localizer["success"].Value });

            }
        }
}
