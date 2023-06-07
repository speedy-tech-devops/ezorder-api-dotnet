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
    [DisplayName("shop_roles")]
    [BsonIgnoreExtraElements]
    public class ShopRoles
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("code")]
        public string? Code { get; set; }
        [BsonElement("description")]
        public string? Description { get; set; }
        [BsonElement("name")]
        public string? Name { get; set; }
        [BsonElement("permissions")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string?> Permissions { get; set; }

    }
}
