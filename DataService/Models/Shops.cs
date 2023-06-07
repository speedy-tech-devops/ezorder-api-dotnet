using CommonServices;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Models
{
    [DisplayName("shops")]
    [BsonIgnoreExtraElements]
    public class Shops
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("description")]
        public ShopDescription? Description { get; set; }
        [BsonElement("name")]
        public ShopName? Name { get; set; }
        [BsonElement("addresses")]
        public List<ShopAddress> Addresses { get; set; } = new List<ShopAddress>();
        [BsonElement("is_delete")]
        public bool? IsDelete { get; set; }
        [BsonElement("is_enable")]
        public bool? IsEnable { get; set; }
        [BsonElement("package_type")]
        public string? PackageType { get; set; }
        [BsonElement("shop_type")]
        public string? ShopType { get; set; }
        [BsonElement("address")]
        public string? Address { get; set; }
        [BsonElement("phone_number")]
        public string? PhoneNumber { get; set; }
        [BsonElement("social_configs")]
        public ShopSocial? SocialConfigs { get; set; }
        [BsonElement("updated_at")]
        [BsonSerializer(typeof(MongoSerializer))]
        public DateTime UpdatedAt { get; set; }
        [BsonElement("updated_by")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UpdatedBy { get; set; }
        [BsonElement("is_approved")]
        public bool? IsApproved { get; set; }
        [BsonElement("approved_at")]
        [BsonSerializer(typeof(MongoSerializer))]
        public DateTime ApprovedAt { get; set; }
        [BsonElement("approved_by")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ApprovedBy { get; set; }
        [BsonElement("referral_code_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ReferralCodeId { get; set; }
    }
    public class ShopDescription
    {
        [BsonElement("th")]
        public string? TH { get; set; }
        [BsonElement("en")]
        public string? EN { get; set; }
    }
    public class ShopName
    {
        [BsonElement("th")]
        public string? TH { get; set; }
        [BsonElement("en")]
        public string? EN { get; set; }
    }
    public class ShopAddress
    {
        [BsonElement("coutry")]
        public string? Coutry { get; set; }
        [BsonElement("city")]
        public string? City { get; set; }
        [BsonElement("state")]
        public string? State { get; set; }
        [BsonElement("zip_code")]
        public string? ZipCode { get; set; }
        [BsonElement("street_address")]
        public string? StreetAddress { get; set; }
        [BsonElement("is_default")]
        public bool? IsDefault { get; set; }
    }
    public class ShopSocial
    {
        [BsonElement("facebook_url")]
        public string? FacebookUrl { get; set; }
        [BsonElement("ig_url")]
        public string? IGUrl { get; set; }
        [BsonElement("website_url")]
        public string? WebsiteUrl { get; set; }
    }
}
