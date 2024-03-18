using AutoMapper;
using Market.DTO;
using Market.Models;

namespace Market.Repo
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, DtoProduct>(MemberList.Destination).ReverseMap();
            CreateMap<ProductGroup, DtoProductGroup>(MemberList.Destination).ReverseMap();
            CreateMap<Storage, DtoStorage>(MemberList.Destination).ReverseMap();
        }
    }
}
