using KASHOP.bll.Service;
using KASHOP.dal.Data;
using KASHOP.dal.DTO.Request;
using KASHOP.dal.Models;
using KASHOP.dal.Repository;
using KASHOP.pl.Resources;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace KASHOP.pl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService, IStringLocalizer<SharedResource> localizer)//CLr CREATE OBJ

        {
            _localizer = localizer;
            _categoryService = categoryService;
        }
        [HttpGet]
        public  async Task<IActionResult> Index() {
            var response =await _categoryService.GetAllCategories();

            return Ok(new
            {
                data= response,
                message = _localizer["success"].Value
            });
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryRequest request)
        {
            var response = await _categoryService.CreateCategory(request);


            return Ok(new
            {
                data = response,
                message = _localizer["success"].Value
            });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _categoryService.GetCategory(c => c.Id == id));
        }
    }

}
