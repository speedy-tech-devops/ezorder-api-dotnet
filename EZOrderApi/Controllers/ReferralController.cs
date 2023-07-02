using AutoMapper;
using DataServices.Repository;
using EZOrderApi.DTO;
using EZOrderApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EZOrderApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReferralController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly MongoDBServices _mongoDBService;
        private readonly IConfiguration _configuration;
        public ReferralController(IMapper mapper, MongoDBServices mongoDBService, IConfiguration configuration)
        {
            _mapper = mapper;
            _mongoDBService = mongoDBService;
            _configuration = configuration;
        }
        [HttpPost("CreateSaleReferral")]
        public async Task<IActionResult> CreateSaleReferral(SaleReferralModel request)
        {
            ResponseBaseModel response = new ResponseBaseModel();
            ReferralService referralService = new ReferralService(_mapper, _mongoDBService, _configuration);
            response = await referralService.CreateSaleReferral(request, response);
            return await Task.FromResult(StatusCode(response.Status, response));
        }
        [HttpGet("GetSaleReferral")]
        public async Task<IActionResult> GetSaleReferral()
        {
            SaleReferralResponse response = new SaleReferralResponse();
            ReferralService referralService = new ReferralService(_mapper, _mongoDBService, _configuration);
            response = await referralService.GetSaleReferral(response);
            return await Task.FromResult(StatusCode(response.Status, response));
        }
    }
}
