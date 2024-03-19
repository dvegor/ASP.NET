using Market.DTO;
using Market.Models;

namespace Market.Abstraction
{
    public interface IProductGroupRepository
    {
        public int AddGroup(DtoProductGroup group);
        public IEnumerable<DtoProductGroup> GetProductGroups();
    }
}
