using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace EZOrderApi.DTO
{
    public class SaleReferralModel
    {
        [Required(ErrorMessage = "{0} ไม่สามารถเป็นค่าว่าง")]
        public string? Code { get; set; } = null;
        [Required(ErrorMessage = "{0} ไม่สามารถเป็นค่าว่าง")]
        public string? ReferralName { get; set; } = null;
        [Required(ErrorMessage = "{0} ไม่สามารถเป็นค่าว่าง")]
        public bool? IsEnable { get; set; }
    }
    public class SaleReferralResponse : ResponseBaseModel
    {
        public List<SaleReferralItemResponse> Referral { get; set; } = new List<SaleReferralItemResponse>();
    }
    public class SaleReferralItemResponse 
    {
        public string? Id { get; set; }
        public string? Code { get; set; }
        public string? ReferralType { get; set; }
        public string? Description { get; set; }
        public bool? IsEnable { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
    }
}
