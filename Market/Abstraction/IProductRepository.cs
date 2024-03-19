using Market.DTO;
using Market.Models;

namespace Market.Abstraction
{
    public interface IProductRepository
    {
        public int AddProduct(DtoProduct product);
        public IEnumerable<DtoProduct> GetProducts();
    }
}
