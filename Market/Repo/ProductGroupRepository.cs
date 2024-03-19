using AutoMapper;
using Market.Abstraction;
using Market.DTO;
using Market.Models;

namespace Market.Repo
{
    public class ProducGroupRepository : IProductGroupRepository
    {
        private readonly IMapper _mapper;
        public ProducGroupRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public int AddGroup(DtoProductGroup group)
        {

            using (var context = new ProductContext())
            {
                var entityGroup = context.ProductGroups.FirstOrDefault(x => x.Name.ToLower() == group.Name.ToLower());
                if (entityGroup == null)
                {
                    entityGroup = _mapper.Map<ProductGroup>(group);
                    context.ProductGroups.Add(entityGroup);
                    context.SaveChanges();
                }
                return entityGroup.Id;
            }
        }

        public IEnumerable<DtoProductGroup> GetProductGroups()
        {
            using (var context = new ProductContext())
            {
                var groupsList = context.ProductGroups.Select(x => _mapper.Map<DtoProductGroup>(x)).ToList();
                return groupsList;
            }
        }


    }
}

