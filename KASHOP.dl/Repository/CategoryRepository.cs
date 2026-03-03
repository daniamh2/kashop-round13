using KASHOP.dal.Data;
using KASHOP.dal.Models;
using Microsoft.EntityFrameworkCore;

namespace KASHOP.dal.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
