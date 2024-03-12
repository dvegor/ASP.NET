namespace Market.Models
{
    public class Product : BaseModel
    {
        public int Price { get; set; }
        public int ProductId { get; set; }
        public virtual ProductGroup? ProductGroup { get; set; }
        public virtual List<Storage> Storages { get; set; } = new List<Storage>();
    }
}
