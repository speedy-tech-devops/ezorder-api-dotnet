using AutoMapper;
namespace EZOrderApi
{
    public class MapperConfigs : Profile
    {
        public MapperConfigs()
        {
            CreateMap<DataServices.Models.ShopUsers, DTO.RegisterModel>()
                .ReverseMap();
        }
    }
}
