using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EZOrderApi.DTO
{
    public class ResponseBaseIsValidModel : ResponseBaseModel
    {
        public Object? InvalidField { get; set;}
    }
    public class ResponseBaseModel
    {
        public int Status { get; set; } = 200;
        public string? StatusText { get; set; } = "Success";
        public string? RequestID { get; set; } = Guid.NewGuid().ToString();
    }
    public class ResponseBasePageModel
    {
        public int Status { get; set; } = 200;
        public string? StatusText { get; set; } = "Success";
        public string? RequestID { get; set; } = Guid.NewGuid().ToString();
        public int TotalPage { get; set; }
        public int TotalRecord { get; set; }
    }
}
