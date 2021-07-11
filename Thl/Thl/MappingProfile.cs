using AutoMapper;
using Thl.EFCore.Models;
using Thl.Models;

namespace Thl
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}