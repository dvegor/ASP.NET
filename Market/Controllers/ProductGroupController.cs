﻿using Market.Abstraction;
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

        private readonly IProductRepository _productRepository;
        public ProductGroupController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        [HttpGet("getProductGroups")]
        public IActionResult GetProductGroups()
        {
            var productGroups = _productRepository.GetProductGroups();
            return Ok(productGroups);
        }
        [HttpPost("addProductGroups")]
        public IActionResult AddGroup([FromBody] DtoProductGroup dtoProductGroup)
        {
            var result = _productRepository.AddGroup(dtoProductGroup);
            return Ok(result);
        }

        [HttpDelete("deleteProductGroups")]
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
