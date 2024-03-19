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
        public IActionResult DelGroup([FromQuery] string name)
        {
            try
            {
                var result = _productRepository.DelGroup(name);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(409, "Группы с таким именем не существует");
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
