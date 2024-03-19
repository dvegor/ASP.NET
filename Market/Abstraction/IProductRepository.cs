using Market.DTO;
using Market.Models;

namespace Market.Abstraction
{
    public interface IProductRepository
    {
        public int AddProduct(DtoProduct product);
        public IEnumerable<DtoProduct> GetProducts();
        public string GetProductsCsv();
        public bool DelProduct(string name);
        public bool UpdProduct(string name, DtoProduct product);
    }
}
