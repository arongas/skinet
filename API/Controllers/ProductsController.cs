using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        public readonly IGenericRepository<Product> _productsRepo;
        public readonly IGenericRepository<ProductBrand> _productBrandsRepo;
        public readonly IGenericRepository<ProductType> _productTypesRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepo,
                                  IGenericRepository<ProductBrand> productBrandsRepo,
                                  IGenericRepository<ProductType> productTypesRepo,
                                  IMapper mapper)
        {
            _productsRepo = productsRepo;
            _productBrandsRepo = productBrandsRepo;
            _productTypesRepo = productTypesRepo;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetProducts()
        {
            var products = await _productsRepo.ListAsync(new ProductsWithTypesAndBrandsSpecification());
            return Ok(_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductDto>>(products));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _productsRepo.GetAsync(new ProductsWithTypesAndBrandsSpecification(id));
            if (product==null) return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Product,ProductDto>(product));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<Product>>> GetProductBrands()
        {
            return Ok(await _productBrandsRepo.ListAllAsync());
        }


        [HttpGet("brands/{id}")]
        public async Task<ActionResult<Product>> GetProductBrand(int id)
        {
            return Ok(await _productBrandsRepo.GetByIdAsync(id));
        }


        [HttpGet("types")]
        public async Task<ActionResult<List<Product>>> GetProductTypes()
        {
            return Ok(await _productTypesRepo.ListAllAsync());
        }


        [HttpGet("types/{id}")]
        public async Task<ActionResult<Product>> GetProductTypes(int id)
        {
            return Ok(await _productTypesRepo.GetByIdAsync(id));
        }
    }
}