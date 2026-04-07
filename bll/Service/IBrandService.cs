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
    public interface IBrandService
    {
            Task<List<BrandResponse>> GetAllBrands();
            Task<BrandResponse> CreateBrand(BrandRequest request);
            Task<BrandResponse> GetBrand(Expression<Func<Brand, bool>> filter);
            Task<bool> DeleteBrand(int id);
    }

}

