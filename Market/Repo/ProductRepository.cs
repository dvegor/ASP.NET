using AutoMapper;
using Market.Abstraction;
using Market.DTO;
using Market.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Market.Repo
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        public ProductRepository(IMapper mapper, IMemoryCache cache)
        {
            _mapper = mapper;
            _cache = cache;
        }

        public int AddProduct(DtoProduct product)
        {
            using (var context = new ProductContext())
            {
                var entityProduct = context.Products.FirstOrDefault(x => x.Name.ToLower() == product.Name.ToLower());
                if (entityProduct == null)
                {
                    entityProduct = _mapper.Map<Product>(product);
                    context.Products.Add(entityProduct);
                    context.SaveChanges();
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

            using (var context = new ProductContext())
            {
                var productsList = context.Products.Select(x => _mapper.Map<DtoProduct>(x)).ToList();
                _cache.Set("products", productsList, TimeSpan.FromMinutes(30));
                return productsList;
            }
        }
    }
}