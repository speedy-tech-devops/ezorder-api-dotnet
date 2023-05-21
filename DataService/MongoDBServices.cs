using MongoDB.Bson;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataServices.Models;

namespace DataService
{
    public class MongoDBServices
    {
        private static readonly CultureInfo en = new CultureInfo("en-US");
        private readonly IMongoDatabase _mongoDatabase;
        public MongoDBServices(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionString);
            _mongoDatabase = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        }
        public async Task<bool> CollectionExists(string collectionName)
        {
            var filter = new BsonDocument("name", collectionName);
            var options = new ListCollectionNamesOptions { Filter = filter };

            return await _mongoDatabase.ListCollectionNames(options).AnyAsync();
        }
    }
}
