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
using DataServices;
using Microsoft.Extensions.Configuration;

namespace DataService
{
    public class MongoDBServices : IMongoDBServices
    {
        private readonly IConfiguration _configuration;
        public MongoDBServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IMongoDatabase GetMongoDbInstance()
        {
            var mongoDBSettings = _configuration.GetSection("MongoDBSettings")
                                                    .Get<MongoDBSettings>();
            var client = new MongoClient(mongoDBSettings?.ConnectionString);
            var db = client.GetDatabase(mongoDBSettings?.DatabaseName);
            return db;
        }
        private IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return GetMongoDbInstance().GetCollection<T>(collectionName);
        }
        public async Task CreateDocument<T>(string collectionName, T document)
        {
            await GetCollection<T>(collectionName).InsertOneAsync(document);
        }
        public async Task DeleteDocument<T>(string collectionName, FilterDefinition<T> filter)
        {
            await GetCollection<T>(collectionName).DeleteOneAsync(filter);
        }
        public async Task<List<T>> GetAllDocuments<T>(string collectionName)
        {
            var collection = GetCollection<T>(collectionName);
            return await collection.Find(x => true).ToListAsync();
        }
        public async Task<List<T>> GetFilteredDocuments<T>(string collectionName, FilterDefinition<T> filter)
        {
            return await GetCollection<T>(collectionName).Find(filter).ToListAsync();
        }
        public async Task UpdateDocument<T>(string collectionName, FilterDefinition<T> filter, UpdateDefinition<T> document)
        {
            await GetCollection<T>(collectionName).UpdateOneAsync(filter, document);
        }
    }
}
