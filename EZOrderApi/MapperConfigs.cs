using AutoMapper;
using EZOrderApi.DTO;

namespace EZOrderApi
{
    public class MapperConfigs : Profile
    {
        public MapperConfigs()
        {
            CreateMap<DataServices.Models.ShopUsers, DTO.RegisterModel>()
                .ReverseMap();
            CreateMap<DataServices.Models.ReferralCode, DTO.SaleReferralItemResponse>()
                .ReverseMap();
            CreateMap<DataServices.Models.Shops, DTO.ShopWaitApproveItemResponse>()
               .ReverseMap();
            CreateMap<DataServices.Models.Shops, DTO.Shop>()
               .ReverseMap();
            CreateMap<DataServices.Models.ShopCategories, DTO.ShopCategoriesItemResponse>()
               .ReverseMap();
        }
    }
}
