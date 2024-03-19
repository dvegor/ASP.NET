using Market.Abstraction;
using Market.DTO;
using Market.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Market.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        [HttpGet("getProducts")]
        public IActionResult GetProducts()
        {
            var products = _productRepository.GetProducts();
            return Ok(products);
        }

        [HttpPost("addProducts")]
        public IActionResult AddProduct([FromBody] DtoProduct dtoProduct)
        {
            var result = _productRepository.AddProduct(dtoProduct);
            return Ok(result);
        }

        [HttpDelete("deleteProducts")]
        public IActionResult DeleteProduct([FromQuery] string name)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    if (context.Products.Any(x => x.Name.ToLower().Equals(name.ToLower())))
                    {
                        context.Products.Where(x => x.Name.ToLower().Equals(name.ToLower())).ExecuteDelete();
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(409);
                    }
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPatch("updateProducts")]
        public IActionResult UpdateProducts(
            [FromQuery] string name,
            [FromBody] DtoProduct dtoUpdateProducts)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    if (context.Products.Any(x => x.Name.ToLower().Equals(name.ToLower())))
                    {
                        context.Products.Where(x => x.Name.ToLower().Equals(name.ToLower()))
                        .ExecuteUpdate(setters => setters
                        .SetProperty(x => x.Description, dtoUpdateProducts.Description)
                        .SetProperty(x => x.Price, dtoUpdateProducts.Price));
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(409);
                    }
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

    }
}
