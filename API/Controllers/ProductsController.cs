using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        public readonly IProductRepository _repo;

        public ProductsController(IProductRepository repo)
        {
            _repo = repo;
            
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            return Ok(await _repo.GetProductsAsync());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return Ok(await _repo.GetProductByIdAsync(id));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<Product>>> GetProductBrands()
        {
            return Ok(await _repo.GetProductBrandsAsync());
        }


        [HttpGet("brands/{id}")]
        public async Task<ActionResult<Product>> GetProductBrand(int id)
        {
            return Ok(await _repo.GetProductBrandByIdAsync(id));
        }     


        [HttpGet("types")]
        public async Task<ActionResult<List<Product>>> GetProductTypes()
        {
            return Ok(await _repo.GetProductTypesAsync());
        }


        [HttpGet("types/{id}")]
        public async Task<ActionResult<Product>> GetProductTypes(int id)
        {
            return Ok(await _repo.GetProductTypeByIdAsync(id));
        }            
    }
}