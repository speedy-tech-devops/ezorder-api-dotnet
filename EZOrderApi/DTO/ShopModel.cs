using CommonServices;
using DataServices.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

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
    public class ShopCategoriesModel
    {
        [Required(ErrorMessage = "{0} ไม่สามารถเป็นค่าว่าง")]
        public string? NameTh { get; set; } = null;
        [Required(ErrorMessage = "{0} ไม่สามารถเป็นค่าว่าง")]
        public string? NameEn { get; set; } = null;
    }
    public class ShopCategoriesResponse : ResponseBaseModel
    {
        public List<ShopCategoriesItemResponse> Referral { get; set; } = new List<ShopCategoriesItemResponse>();
    }
    public class ShopCategoriesItemResponse
    {
        public string? Id { get; set; }
        public ShopCategoriesName? Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public bool? IsDelete { get; set; }
    }
}
