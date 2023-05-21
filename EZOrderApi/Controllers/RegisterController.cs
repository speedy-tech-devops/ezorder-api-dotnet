using DataService;
using Microsoft.AspNetCore.Mvc;

namespace EZOrderApi.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly MongoDBServices _mongoDBServices;
        public RegisterController(MongoDBServices mongoDBServices)
        {
            _mongoDBServices = mongoDBServices;
        }
        [HttpGet("")]
        public async Task<IActionResult> TEST()
        {
            _ = _mongoDBServices.CollectionExists("");
            return await Task.FromResult(StatusCode(400));
        }
    }
}
