using CommonServices;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Models
{
    [DisplayName("shop_users")]
    [BsonIgnoreExtraElements]
    public class ShopUsers
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("email")]
        public string? Email { get; set; }
        [BsonElement("password")]
        public string? Password { get; set; }
        [BsonElement("is_delete")]
        public bool IsDelete { get; set; }
        [BsonElement("roles")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string?> Roles { get; set; }
        [BsonElement("is_owner_account")]
        public bool IsOwnerAccount { get; set; }
        [BsonElement("first_name")]
        public string? FirstName { get; set; }
        [BsonElement("last_name")]
        public string? LastName { get; set; }
        [BsonElement("access_token")]
        public string? AccessToken { get; set; }
        [BsonElement("__v")]
        public int version { get; set; }
        [BsonElement("updatedAt")]
        [BsonSerializer(typeof(MongoSerializer))]
        public DateTime UpdatedAt { get; set; }
        [BsonElement("assign_branches")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string?> AssignBranches { get; set; }
        [BsonElement("is_enable")]
        public bool IsEnable { get; set; }
        [BsonElement("owner_shop")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? OwnerShop { get; set; }
        [BsonElement("refresh_token")]
        public string? RefreshToken { get; set; }
        [BsonElement("app_access_token")]
        public string? AppAccessToken { get; set; }
        [BsonElement("app_refresh_token")]
        public string? AppRefreshToken { get; set; }
    }
}
