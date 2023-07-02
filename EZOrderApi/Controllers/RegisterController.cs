using AutoMapper;
using DataServices;
using DataServices.Models;
using DataServices.Repository;
using EZOrderApi.DTO;
using EZOrderApi.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Xml.Linq;

namespace EZOrderApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly MongoDBServices _mongoDBService;
        private readonly IConfiguration _configuration;
        public RegisterController(IMapper mapper, MongoDBServices mongoDBService, IConfiguration configuration)
        {
            _mapper = mapper;
            _mongoDBService = mongoDBService;
            _configuration = configuration;
        }
        //[HttpGet("GetByName")]
        //public async Task<IEnumerable<ShopUsers>> GetByName(string name)
        //{
        //    var filter = Builders<ShopUsers>.Filter.Eq("Name", name);
        //    return await _mongoDBService.GetFilteredDocumentsAsync<ShopUsers>(filter);
        //}
        //[HttpPatch("UpdateNameAndSalary")]
        //public async Task UpdateDetails(ShopUser partechData)
        //{
        //    var filter = Builders<ShopUser>.Filter.Eq("id", partechData._id);
        //    var update = Builders<ShopUser>.Update.Set(x => x._id, partechData._id);
        //    await _mongoDBServices.UpdateDocument<ShopUser>(filter, update);
        //}
        //[HttpPost]
        //public async Task CreateDetails(ShopUser partechData)
        //{
        //    await _mongoDBServices.CreateDocument<ShopUser>(partechData);
        //}
        //[HttpDelete]
        //public async Task DeleteDetails(int id)
        //{
        //    var filter = Builders<ShopUser>.Filter.Eq("EmployeeId", id);
        //    await _mongoDBServices.DeleteDocument<ShopUser>(filter);
        //}
        [HttpPost]
        public async Task<IActionResult> CreateShop(RegisterModel request)
        {
            ResponseBaseModel response = new ResponseBaseModel();
            List<string> packageType = new List<string>() { "BASIC", "PLUS", "GOLD", "PLATINUM" };
            List<string> shopType = new List<string>() { "RESTAURANT", "HOTEL" };
            ShopService shopService = new ShopService(_mapper, _mongoDBService, _configuration);
            if (!packageType.Contains(request.PackageType))
            {
                response.Status = 400;
                response.StatusText = $"โปรดระบุ PackageType ให้ตรง ({String.Join(',', packageType)})";
            }
            if (!shopType.Contains(request.ShopType))
            {
                response.Status = 400;
                response.StatusText = $"โปรดระบุ ShopType ให้ตรง ({String.Join(',', shopType)})";
            }
            else
            {
                await shopService.Register(request);
            }
            return await Task.FromResult(StatusCode(response.Status, response));
        }

    }
}
