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
        [HttpGet("getProducts")]
        public IActionResult GetProducts()
        {
            try
            {
                using (var context = new ProductContext())
                {
                    IQueryable<Product> products = context.Products.Select(x => new Product
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        ProductGroup = x.ProductGroup,
                        Price = x.Price,
                        ProductGroupId = x.ProductGroupId,
                        Storages = x.Storages
                    });
                    return Ok(products);
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpPost("postProducts")]
        public IActionResult PostProducts([FromQuery] string name, string description, int price, int productId)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    if (!context.Products.Any(x => x.Name.ToLower().Equals(name.ToLower())))
                    {
                        context.Add(new Product()
                        {
                            Name = name,
                            Description = description,
                            Price = price,
                            ProductId = productId
                        });
                        context.SaveChanges();
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
            [FromBody] DtoProducts dtoUpdateProducts)
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
                        .SetProperty(x => x.Price, dtoUpdateProducts.Price)
                        .SetProperty(x => x.ProductGroupId, dtoUpdateProducts.ProductGroupId));
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
