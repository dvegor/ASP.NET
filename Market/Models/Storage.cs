namespace Market.Models
{
    public class Storage : BaseModel
    {
        public virtual List<Product> Products { get; set; } = new List<Product>();
        public int Count { get; set; }
    }
}
