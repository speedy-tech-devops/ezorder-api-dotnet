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
    [DisplayName("notification_events")]
    [BsonIgnoreExtraElements]
    public class NotificationEvents
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        public NotificationName? Name { get; set; }
        [BsonElement("type")]
        public string? Type { get; set; }
        [BsonElement("request")]
        public string? Request { get; set; }
        [BsonElement("is_enable")]
        public bool? IsEnable { get; set; }
    }
    public class NotificationName
    {
        [BsonElement("th")]
        public string? TH { get; set; }
        [BsonElement("en")]
        public string? EN { get; set; }
    }
}
