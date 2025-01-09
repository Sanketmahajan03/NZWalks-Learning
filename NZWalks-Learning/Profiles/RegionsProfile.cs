using AutoMapper;
using NZWalks_Learning.Model.Domain;
using NZWalks_Learning.Model.DTO;

namespace NZWalks_Learning.Profiles
{
    public class RegionsProfile : Profile
    {
        public RegionsProfile()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
        }
    }
}
