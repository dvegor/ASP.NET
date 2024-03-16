using Market.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Market.Controllers
{
        [ApiController]
        [Route("[controller]")]
        public class ProductGroupController : ControllerBase
        {
            [HttpGet("getProductGroups")]
            public IActionResult GetProductGroup()
            {
                try
                {
                    var context = new ProductContext();
                    IQueryable<ProductGroup> groups = context.ProductGroups.Select(x => new ProductGroup
                    {
                        Id = x.Id,
                        Name = x.Name,
                    });
                    return Ok(groups);

                }
                catch
                {
                    return StatusCode(500);
                }
            }
            [HttpPost("postProductGroups")]
            public IActionResult PostCategories(
                [FromQuery] string name)
            {
                try
                {
                    var context = new ProductContext();
                    if (!context.ProductGroups.Any(x => x.Name.ToLower().Equals(name.ToLower())))
                    {
                        context.Add(new ProductGroup()
                        {
                            Name = name
                        });
                        context.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(409);
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex);
                }
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
