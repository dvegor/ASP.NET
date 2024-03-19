using Market.DTO;
using System.Text;

namespace Market.Services
{
    public class GetCsv
    {
        public static string GetProducts(IEnumerable<DtoProduct> types)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var t in types)
            {
                sb.Append(t.Id + ";" + t.Name + ";" + t.Price + ";" + t.Description + ";" + t.ProductGroupId + "\n");
            }
            return sb.ToString();
        }
    }
}
