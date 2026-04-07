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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileService _fileService;
        public ProductService(IProductRepository productRepository, IFileService fileService)
        {
            _productRepository = productRepository;
            _fileService = fileService;
        }

        public async Task<ProductResponse> CreateProduct(ProductRequest request)
        {
            var product = request.Adapt<Product>();
            if (request.MainImage != null)
            {
                var imagePath = await _fileService.UploadAsync(request.MainImage);
                product.MainImage = imagePath;
            }
            product= await _productRepository.CreateAsync(product);
            return product.Adapt<ProductResponse>();

        }
        public async Task<List<ProductResponse>> GetAllProducts()
        {
            var products = await _productRepository.GetAllAsync(
                new string[] {
                    nameof(Product.Translations),
                    nameof(Product.CreatedBy) }
                );
            return products.Adapt<List<ProductResponse>>();
        }

        public async Task<ProductResponse> GetProduct(Expression<Func<Product, bool>> filter)
        {
            var product = await _productRepository.GetOne(filter,
                new string[] {
                    nameof(Product.Translations),
                    nameof(Product.CreatedBy)
                });
            if (product == null) return null;
            return product.Adapt<ProductResponse>();

        }

        public async Task<bool> DeleteProduct(int id)
        {

            var product = await _productRepository.GetOne(c => c.Id == id);
            if (product == null) return false;
            _fileService.Delete(product.MainImage);
            return await _productRepository.DeleteAsync(product);
        }
    }
}
