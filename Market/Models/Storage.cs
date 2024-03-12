namespace Market.Models
{
    public class Storage : BaseModel
    {
        public virtual List<ProductsToStorages> ProductsToStorages { get; set; } = new List<ProductsToStorages>();

    }
}
