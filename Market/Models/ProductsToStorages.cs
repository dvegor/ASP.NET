namespace Market.Models
{
    public class ProductsToStorages
    {
        public int? ProductId { get; set; }
        public int? StorageId { get; set; }
        public virtual Product? Product { get; set; }
        public virtual Storage? Storage { get; set; }
    }
}
