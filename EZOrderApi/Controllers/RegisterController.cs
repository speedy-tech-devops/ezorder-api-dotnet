using DataService;
using DataServices;
using DataServices.Models;
using Microsoft.AspNetCore.Mvc;

namespace EZOrderApi.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly IMongoDBServices _mongoDBServices;
        public RegisterController(IMongoDBServices mongo)
        {
            _mongoDBServices = mongo;
        }
        [HttpGet("")]
        public async Task<IActionResult> TEST()
        {
            var xx = await _mongoDBServices.GetAllDocuments<ShopUser>("dev-speedy", "shop_users");
            return await Task.FromResult(StatusCode(400));
        }
    }
}
