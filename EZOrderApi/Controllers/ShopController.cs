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
    }
}
