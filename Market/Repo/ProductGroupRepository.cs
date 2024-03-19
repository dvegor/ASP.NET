using AutoMapper;
using Market.Abstraction;
using Market.DTO;
using Market.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Market.Repo
{
    public class ProducGroupRepository : IProductGroupRepository
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        public ProducGroupRepository(IMapper mapper, IMemoryCache cache)
        {
            _mapper = mapper;
            _cache = cache;
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
                    _cache.Remove("groups");
                }
                return entityGroup.Id;
            }
        }

        public IEnumerable<DtoProductGroup> GetProductGroups()
        {
            if(_cache.TryGetValue("groups", out List<DtoProductGroup> groups))
            {
                return groups;
            }

            using (var context = new ProductContext())
            {
                var groupsList = context.ProductGroups.Select(x => _mapper.Map<DtoProductGroup>(x)).ToList();
                _cache.Set("groups", groupsList, TimeSpan.FromMinutes(30));
                return groupsList;
            }
        }


    }
}

