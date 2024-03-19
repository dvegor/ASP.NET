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


        [HttpGet("get_products")]
        public IActionResult GetProducts()
        {
            var products = _productRepository.GetProducts();
            return Ok(products);
        }


        [HttpGet("get_products_in_CSV")]
        public IActionResult GetProducts([FromQuery] bool csv, bool url)
        {
            if (csv)
            {
                var products = _productRepository.GetProductsCsv();
                if (url)
                {
                    string filename = "products" + DateTime.Now.ToBinary().ToString() + ".csv";
                    System.IO.File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "Files", filename), products);
                    return Ok("https://" + Request.Host.ToString() + "/static/" + filename);
                }
                else
                {
                    return File(new System.Text.UTF8Encoding().GetBytes(products), "text/csv", "products.csv");
                }
            }
            else
            {
                var products = _productRepository.GetProducts();
                return Ok(products);
            }
        }


        [HttpPost("add_products")]
        public IActionResult AddProduct([FromBody] DtoProduct dtoProduct)
        {
            var result = _productRepository.AddProduct(dtoProduct);
            return Ok(result);
        }

        [HttpDelete("delete_products")]
        public IActionResult DeleteProduct([FromQuery] string name)
        {
            try
            {
                var result = _productRepository.DelProduct(name);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(409, "Продукта с таким именем не существует");
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
            [FromBody] DtoProduct product)
        {
            try
            {
                var result = _productRepository.UpdProduct(name, product);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(409, "Продукт не найден");
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

    }
}
