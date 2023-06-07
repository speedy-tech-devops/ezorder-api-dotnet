using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DataServices.Models
{
    [DisplayName("request_response_log")]
    [BsonIgnoreExtraElements]
    public class RequestResponseLogModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("url")]
        public string? Url { get; set; }
        [BsonElement("part")]
        public string? Part { get; set; }
        [BsonElement("method")]
        public string? method { get; set; }
        [BsonElement("status_code")]
        public int? StatusCode { get; set; }
        [BsonElement("client_ip")]
        public string? ClientIp { get; set; }
        [BsonElement("request_header")]
        public string? RequestHeader { get; set; }
        [BsonElement("requset_body")]
        public string? RequsetBody { get; set; }
        [BsonElement("exception")]
        public string? Exception { get; set; }
        [BsonElement("response_header")]
        public string? ResponseHeader { get; set; }
        [BsonElement("response_body")]
        public string? ResponseBody { get; set; }
        [BsonElement("create_date")]
        public DateTime? CreateDate { get; set; }
        [BsonElement("update_date")]
        public DateTime? UpdateDate { get; set; }
    }
}
