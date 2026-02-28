using KASHOP.dal.Data;
using KASHOP.dal.DTO.Request;
using KASHOP.dal.Models;
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
        private readonly ApplicationDbContext _context ;//NO NEW BEACAUSE IT ALLOCAE HEAP PRIVATE AND READONLY TO ASSURE NOT CHANGE
        private readonly IStringLocalizer<SharedResource> _localizer;
        public CategoriesController(ApplicationDbContext context,IStringLocalizer<SharedResource> localizer)//CLr CREATE OBJ

        {
            _context = context;
            _localizer = localizer;
        }
        [HttpGet]
        public IActionResult Get() {
            var categories = _context.Categories.Include(c => c.Translations).ToList();
            var response = categories.Adapt<List<CreateCategory>>();

            return Ok(new
            {
                data= response,
                message = _localizer["success"].Value
            });
        }
        [HttpPost]
        public IActionResult Create(CategoryRequest request)
        {
            var category = request.Adapt<Category>();
            _context.Add(category);

            _context.SaveChanges();
            return Ok(new
            {
                message=_localizer["success"].Value
            });
        }
    }

}
