using AutoMapper;
using Market.Abstraction;
using Market.DTO;
using Market.Models;

namespace Market.Repo
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper _mapper;
        public ProductRepository(IMapper mapper)
        {
            _mapper = mapper;
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
                }
                return entityProduct.Id;
            }
        }
        public IEnumerable<DtoProduct> GetProducts()
        {
            using (var context = new ProductContext())
            {
                var productsList = context.Products.Select(x => _mapper.Map<DtoProduct>(x)).ToList();
                return productsList;
            }
        }
    }
}