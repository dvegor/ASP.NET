using AutoMapper;
using Market.Abstraction;
using Market.DTO;
using Market.Models;
using Market.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Market.Repo
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly ProductContext _productContext;
        public ProductRepository(IMapper mapper, IMemoryCache cache, ProductContext productContext)
        {
            _mapper = mapper;
            _cache = cache;
            _productContext = productContext;
        }

        public int AddProduct(DtoProduct product)
        {
            {
                var entityProduct = _productContext.Products.FirstOrDefault(x => x.Name.ToLower() == product.Name.ToLower());
                if (entityProduct == null)
                {
                    entityProduct = _mapper.Map<Product>(product);
                    _productContext.Products.Add(entityProduct);
                    _productContext.SaveChanges();
                    _cache.Remove("products");
                }
                return entityProduct.Id;
            }
        }

        public IEnumerable<DtoProduct> GetProducts()
        {
            if (_cache.TryGetValue("products", out List<DtoProduct> products))
            {
                return products;
            }
            var productsList = _productContext.Products.Select(x => _mapper.Map<DtoProduct>(x)).ToList();
            _cache.Set("products", productsList, TimeSpan.FromMinutes(30));
            return productsList;

        }

        public string GetProductsCsv()
        {
            var content = "";
            if (_cache.TryGetValue("products", out List<DtoProduct> products))
            {
                content = GetCsv.GetProducts(products);
            }
            else
            {
                var productList = _productContext.Products.Select(x => _mapper.Map<DtoProduct>(x)).ToList();
                _cache.Set("products", productList, TimeSpan.FromMinutes(30));
                content = GetCsv.GetProducts(productList);
            }
            return content;

        }


        public bool DelProduct(string name)
        {
            if (!_productContext.Products.Any(x => x.Name.ToLower().Equals(name.ToLower())))
            {
                return false;
            }
            _productContext.Products.Where(x => x.Name.ToLower().Equals(name.ToLower())).ExecuteDelete();
            _cache.Remove("products");
            return true;
        }

        public bool UpdProduct(string name, DtoProduct product)
        {
            if (_productContext.Products.Any(x => x.Name.ToLower().Equals(name.ToLower())))
            {
                _productContext.Products.Where(x => x.Name.ToLower().Equals(name.ToLower()))
                .ExecuteUpdate(setters => setters
                .SetProperty(x => x.Description, product.Description)
                .SetProperty(x => x.Price, product.Price)
                .SetProperty(x => x.ProductGroupId, product.ProductGroupId));
                _cache.Remove("products");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}