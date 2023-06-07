using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Models
{
    [DisplayName("referral_code")]
    [BsonIgnoreExtraElements]
    public class ReferralCode
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("code")]
        public string? Code { get; set; }
        [BsonElement("referral_type")]
        public string? ReferralType { get; set; }
        [BsonElement("description")]
        public string? Description { get; set; }
        [BsonElement("is_enable")]
        public bool? IsEnable { get; set; }
        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; }
        [BsonElement("created_by")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CreatedBy { get; set; }

        [BsonElement("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [BsonElement("updated_by")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UpdatedBy { get; set; }
        [BsonElement("is_delete")]
        public bool? IsDelete { get; set; }
        [BsonElement("deleted_at")]
        public DateTime DeletedAt { get; set; }
        [BsonElement("deleted_by")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? DeletedBy { get; set; }
    }
}
