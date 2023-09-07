using CommonServices;
using DataServices.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EZOrderApi.DTO
{
    public class ShopWaitApproveResponse : ResponseBaseModel
    {
        public List<ShopWaitApproveItemResponse> Shops { get; set; } = new List<ShopWaitApproveItemResponse>();
    }
    public class ShopWaitApproveItemResponse
    {
        public string? Id { get; set; }
        public ShopDescription? Description { get; set; }
        public ShopName? Name { get; set; }
        public List<ShopAddress> Addresses { get; set; } = new List<ShopAddress>();
        public string? PackageType { get; set; }
        public string? ShopType { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public string? ReferralCode { get; set; }
    }
    public class ShopListResponse : ResponseBasePageModel
    {
        public List<Shop> Shops { get; set; }
    }
    public class Shop
    {
        public string? Id { get; set; }
        public ShopName? Name { get; set; }
        public string? ShopType { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ReferralCode { get; set; }
        public DateTime ExpirdAt { get; set; }
    }
}
