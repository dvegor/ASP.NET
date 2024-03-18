using Market.DTO;
using Market.Models;

namespace Market.Abstraction
{
    public interface IProductRepository
    {
        public int AddGroup(DtoProductGroup group);
        public IEnumerable<DtoProductGroup> GetProductGroups();
        public int AddProduct(DtoProduct product);
        public IEnumerable<DtoProduct> GetProducts();
    }
}
