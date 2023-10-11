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
    [DisplayName("shop_categories")]
    [BsonIgnoreExtraElements]
    public class ShopCategories
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("name")]
        public ShopCategoriesName? Name { get; set; }

        [BsonElement("created_at")]
        [BsonSerializer(typeof(MongoSerializer))]
        public DateTime CreatedAt { get; set; }
        [BsonElement("created_by")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CreatedBy { get; set; }
        [BsonElement("updated_at")]
        [BsonSerializer(typeof(MongoSerializer))]
        public DateTime UpdatedAt { get; set; }
        [BsonElement("updated_by")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UpdatedBy { get; set; }
        [BsonElement("is_delete")]
        public bool? IsDelete { get; set; }
    }
    public class ShopCategoriesName
    {
        [BsonElement("th")]
        public string? TH { get; set; }
        [BsonElement("en")]
        public string? EN { get; set; }
    }
}
