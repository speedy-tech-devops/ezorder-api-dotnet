using AutoMapper;
using DataServices.Repository;
using EZOrderApi.DTO;
using EZOrderApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EZOrderApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShopController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly MongoDBServices _mongoDBService;
        private readonly IConfiguration _configuration;
        public ShopController(IMapper mapper, MongoDBServices mongoDBService, IConfiguration configuration)
        {
            _mapper = mapper;
            _mongoDBService = mongoDBService;
            _configuration = configuration;
        }
        [HttpGet("WaitApprove")]
        public async Task<IActionResult> GetShopUserWaitApprove()
        {
            ShopWaitApproveResponse response = new ShopWaitApproveResponse();
            ShopService shopService = new ShopService(_mapper, _mongoDBService, _configuration);
            response = await shopService.GetShopWaitApprove(response);
            return await Task.FromResult(StatusCode(response.Status, response));
        }
        [HttpPut("ApproveShop/{id}")]
        public async Task<IActionResult> ApproveShop(string id)
        {
            ResponseBaseModel response = new ResponseBaseModel();
            ShopService shopService = new ShopService(_mapper, _mongoDBService, _configuration);
            response = await shopService.ApproveShop(id, response);
            return await Task.FromResult(StatusCode(response.Status, response));
        }
        [HttpGet("GetShop")]
        public async Task<IActionResult> GetShopUse(string searchText, int? pageSize, int? pageIndex, string orderBy, string sortBy)
        {
            ShopListResponse response = new ShopListResponse();
            ShopService shopService = new ShopService(_mapper, _mongoDBService, _configuration);
            response = await shopService.GetShop(response, searchText, pageIndex, pageSize, orderBy , sortBy);
            return await Task.FromResult(StatusCode(response.Status, response));
        }
        [HttpPost("ShopCategories")]
        public async Task<IActionResult> CreateShopCategories(ShopCategoriesModel request)
        {
            ResponseBaseModel response = new ResponseBaseModel();
            ShopService shopService = new ShopService(_mapper, _mongoDBService, _configuration);
            response = await shopService.CreateShopCategories(request, response);
            return await Task.FromResult(StatusCode(response.Status, response));
        }
        [HttpGet("ShopCategories")]
        public async Task<IActionResult> GetShopCategories()
        {
            ShopCategoriesResponse response = new ShopCategoriesResponse();
            ShopService shopService = new ShopService(_mapper, _mongoDBService, _configuration);
            response = await shopService.GetShopCategories(response);
            return await Task.FromResult(StatusCode(response.Status, response));
        }
        [HttpDelete("ShopCategories/{id}")]
        public async Task<IActionResult> DeleteShopCategories(string id)
        {
            ResponseBaseModel response = new ResponseBaseModel();
            ShopService shopService = new ShopService(_mapper, _mongoDBService, _configuration);
            response = await shopService.DeleteShopCategories(id, response);
            return await Task.FromResult(StatusCode(response.Status, response));
        }
    }
}
