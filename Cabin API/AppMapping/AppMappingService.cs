using AutoMapper;
using Cabin_API.Dtos;
using Cabin_API.Models;

namespace Cabin_API.AppMapping
{
    public class AppMappingService : Profile
    {
        public AppMappingService()
        {
            CreateMap<Cabin, CabinDto>().ReverseMap();
            CreateMap<Price, PriceDto>().ReverseMap();
        }
    }
}
