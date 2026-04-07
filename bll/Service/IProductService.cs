using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using KASHOP.dal.DTO.Request;
using KASHOP.dal.DTO.Response;
using KASHOP.dal.Models;

namespace KASHOP.bll.Service
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetAllProducts();
        Task<ProductResponse> CreateProduct(ProductRequest request);
        Task<ProductResponse> GetProduct(Expression<Func<Product, bool>> filter);
        Task<bool> DeleteProduct(int id);
    }
}
