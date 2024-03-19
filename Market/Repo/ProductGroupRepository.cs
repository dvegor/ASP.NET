using AutoMapper;
using Market.Abstraction;
using Market.DTO;
using Market.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Market.Repo
{
    public class ProducGroupRepository : IProductGroupRepository
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly ProductContext _productContext;
        public ProducGroupRepository(IMapper mapper, IMemoryCache cache, ProductContext productContext)
        {
            _mapper = mapper;
            _cache = cache;
            _productContext = productContext;
        }
        public int AddGroup(DtoProductGroup group)
        {
            var entityGroup = _productContext.ProductGroups.FirstOrDefault(x => x.Name.ToLower() == group.Name.ToLower());
            if (entityGroup == null)
            {
                entityGroup = _mapper.Map<ProductGroup>(group);
                _productContext.ProductGroups.Add(entityGroup);
                _productContext.SaveChanges();
                _cache.Remove("groups");
            }
            return entityGroup.Id;
        }

        public IEnumerable<DtoProductGroup> GetProductGroups()
        {
            if (_cache.TryGetValue("groups", out List<DtoProductGroup> groups))
            {
                return groups;
            }
            var groupsList = _productContext.ProductGroups.Select(x => _mapper.Map<DtoProductGroup>(x)).ToList();
            _cache.Set("groups", groupsList, TimeSpan.FromMinutes(30));
            return groupsList;
        }
        public bool DelGroup(string name)
        {
            if (_productContext.ProductGroups.Any(x => x.Name.ToLower().Equals(name.ToLower())))
            {
                _productContext.ProductGroups.Where(x => x.Name.ToLower()
                .Equals(name.ToLower())).ExecuteDelete();
                _cache.Remove("categories");
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}

