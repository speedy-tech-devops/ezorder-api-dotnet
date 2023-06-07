using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using EZOrderApi.DTO;

namespace EZOrderApi.Class
{
    public class ValidationFilterAttribute : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(new ResponseBaseIsValidModel()
                {
                    Status = (int)System.Net.HttpStatusCode.BadRequest,
                    StatusText = "กรุณากรอกข้อมูลให้ครบถ้วน",
                    RequestID = context.HttpContext.Request.Headers["X-Order-RequestID"].ToString(),
                    InvalidField = new UnprocessableEntityObjectResult(context.ModelState).Value
                });
            }
        }
        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
