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
        public int AddGroup(DtoProductGroup group)
        {

            using (var context = new ProductContext())
            {
                var entityGroup = context.ProductGroups.FirstOrDefault(x => x.Name.ToLower() == group.Name.ToLower());
                if (entityGroup == null)
                {
                    entityGroup = _mapper.Map<ProductGroup>(group);
                    context.ProductGroups.Add(entityGroup);
                    context.SaveChanges();
                }
                return entityGroup.Id;
            }
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

        public IEnumerable<DtoProductGroup> GetProductGroups()
        {
            using (var context = new ProductContext())
            {
                var groupsList = context.ProductGroups.Select(x=> _mapper.Map<DtoProductGroup>(x)).ToList();
                return groupsList;
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
