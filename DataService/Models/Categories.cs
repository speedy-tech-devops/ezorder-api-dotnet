using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonServices;

namespace DataServices.Models
{
    [DisplayName("categories")]
    [BsonIgnoreExtraElements]
    public class Categories
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("description")]
        public CategoryDescription? Description { get; set; }
        [BsonElement("name")]
        public CategoryName? Name { get; set; }
        [BsonElement("status")]
        public string? Status { get; set; }

        [BsonElement("branch")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Branch { get; set; }
        [BsonElement("display_order")]
        public int? DisplayOrder { get; set; }
        [BsonElement("is_delete")]
        public bool? IsDelete { get; set; }
        [BsonElement("is_enable")]
        public bool? IsEnable { get; set; }
        [BsonElement("shop")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Shop { get; set; }
        [BsonElement("is_recommend_category")]
        public bool? IsRecommendCategory { get; set; }
        [BsonElement("updated_at")]
        [BsonSerializer(typeof(MongoSerializer))]
        public DateTime UpdatedAt { get; set; }
        [BsonElement("updated_by")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UpdatedBy { get; set; }

    }
    public class CategoryDescription
    {
        [BsonElement("th")]
        public string? TH { get; set; }
        [BsonElement("en")]
        public string? EN { get; set; }
    }
    public class CategoryName
    {
        [BsonElement("th")]
        public string? TH { get; set; }
        [BsonElement("en")]
        public string? EN { get; set; }
    }
}
