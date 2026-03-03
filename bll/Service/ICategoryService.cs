using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KASHOP.dal.DTO.Response ;
using KASHOP.dal.DTO.Request;
using KASHOP.dal.Models;

namespace KASHOP.bll.Service
{
    public interface ICategoryService
    {
        Task <List<CategoryResponse>> GetAllCategories();
        Task <CategoryResponse> CreateCategory(CategoryRequest request);
    }
}
