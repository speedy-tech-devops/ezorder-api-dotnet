using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CommonServices
{
    public class MongoSerializer : DateTimeSerializer, IBsonSerializer
    {
        public Type ValueType => typeof(DateTime);

        public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var type = context.Reader.GetCurrentBsonType();
            if (type == BsonType.String)
            {
                var s = context.Reader.ReadString();
                string format = "yyyy-MM-dd HH:mm:ss.fff";

                DateTime dateTime;
                if (DateTime.TryParseExact(s, format, null, System.Globalization.DateTimeStyles.None, out dateTime))
                {
                    return dateTime;
                }
                else
                {
                    return null;
                }
            }
            else if (type == BsonType.DateTime)
            {
                var xx = context.Reader.ReadDateTime();
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(xx);
                return dateTimeOffset.UtcDateTime;
            }
            else
            {
                return null;
            }
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            if (value is null)
                context.Writer.WriteNull();
            else
                base.Serialize(context, args, (DateTime)value);
        }
    }
}
