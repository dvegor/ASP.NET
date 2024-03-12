namespace Market.Models
{
    public class Product : BaseModel
    {
        public int Cost { get; set; }
        public int ProductId { get; set; }
        public virtual ProductGroup? ProductGroup { get; set; }
        public virtual List<ProductsToStorages> ProductsToStorages { get; set; } = new List<ProductsToStorages>();
    }
}
