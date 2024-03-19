using Market.Abstraction;
using Market.DTO;
using Market.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Market.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductGroupController : ControllerBase
    {

        private readonly IProductGroupRepository _productRepository;
        public ProductGroupController(IProductGroupRepository productRepository)
        {
            _productRepository = productRepository;
        }


        [HttpGet("get_groups")]
        public IActionResult GetProductGroups()
        {
            var productGroups = _productRepository.GetProductGroups();
            return Ok(productGroups);
        }
        [HttpPost("add_groups")]
        public IActionResult AddGroup([FromBody] DtoProductGroup dtoProductGroup)
        {
            var result = _productRepository.AddGroup(dtoProductGroup);
            return Ok(result);
        }

        [HttpDelete("delete_groups")]
        public IActionResult DeleteCategories([FromQuery] string name)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    if (context.ProductGroups.Any(x => x.Name.ToLower().Equals(name.ToLower())))
                    {
                        context.ProductGroups.Where(x => x.Name.ToLower()
                        .Equals(name.ToLower()))
                            .ExecuteDelete();
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
