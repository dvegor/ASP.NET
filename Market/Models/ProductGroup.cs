namespace Market.Models
{
    public class ProductGroup : BaseModel
    {
        public virtual List<Product> Products { get; set; } = new List<Product>();

    }
}
