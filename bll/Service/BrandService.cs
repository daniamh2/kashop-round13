using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using KASHOP.dal.DTO.Request;
using KASHOP.dal.DTO.Response;
using KASHOP.dal.Models;
using KASHOP.dal.Repository;
using Mapster;

namespace KASHOP.bll.Service
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IFileService _fileService;
        public BrandService(IBrandRepository brandRepository, IFileService fileService)
        {
            _fileService = fileService;
            _brandRepository = brandRepository;

        }
        public async Task<BrandResponse> CreateBrand(BrandRequest request)
        {
            var brand = request.Adapt<Brand>();
            if (request.Logo != null)
            {
                var imagePath = await _fileService.UploadAsync(request.Logo);
                brand.Logo = imagePath;
            }
            brand = await _brandRepository.CreateAsync(brand);
            return brand.Adapt<BrandResponse>();
        }
        public async Task<List<BrandResponse>> GetAllBrands()
        {
            var brands = await _brandRepository.GetAllAsync(
                new string[] {
                    nameof(Brand.Translations),
                    nameof(Brand.CreatedBy) }
                );
            return brands.Adapt<List<BrandResponse>>();
        }

        public async Task<BrandResponse> GetBrand(Expression<Func<Brand, bool>> filter)
        {
            var brand = await _brandRepository.GetOne(filter,
                new string[] {
                    nameof(Brand.Translations),
                    nameof(Brand.CreatedBy)
                });
            if (brand == null) return null;
            return brand.Adapt<BrandResponse>();

        }
        public async Task<bool> DeleteBrand(int id)
        {

            var brand = await _brandRepository.GetOne(c => c.Id == id);
            if (brand == null) return false;
            _fileService.Delete(brand.Logo);
            return await _brandRepository.DeleteAsync(brand);
        }

    }
}
