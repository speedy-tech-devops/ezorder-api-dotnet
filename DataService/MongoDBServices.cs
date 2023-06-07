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
using System.ComponentModel;
using System.Reflection;
using CommonServices;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System.Collections;
using ZstdSharp;

namespace DataServices.Repository
{
    public class MongoDBServices
    {
        private readonly IMongoDatabase _database;

        public MongoDBServices(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings?.Value.ConnectionString);
            _database = client.GetDatabase(mongoDBSettings?.Value.DatabaseName);
        }
        private IMongoCollection<T> GetCollection<T>()
        {
            string collectionName = string.Empty;
            DisplayNameAttribute? displayNameAttribute = typeof(T).GetCustomAttribute<DisplayNameAttribute>();
            if (displayNameAttribute != null)
                collectionName = displayNameAttribute.DisplayName;
            else
                collectionName = StringExtensions.ConvertToSnakeCase(typeof(T).Name);
            return _database.GetCollection<T>(collectionName);
        }
        public async Task<T> InsertDocumentAsync<T>(T document)
        {
            try
            {
                await GetCollection<T>().InsertOneAsync(document);
                return document;
            }
            catch (Exception ex)
            {
                throw new Exception("ไม่สามารถเพิ่มข้อมูลได้");
            }

        }
        public T InsertDocument<T>(T document)
        {
            try
            {
                GetCollection<T>().InsertOne(document);
                return document;
            }
            catch
            {
                throw new Exception("ไม่สามารถเพิ่มข้อมูลได้");
            }
        }
        public async Task DeleteDocumentAsync<T>(FilterDefinition<T> filter)
        {
            try
            {
                await GetCollection<T>().DeleteOneAsync(filter);
            }
            catch
            {
                throw new Exception("ไม่สามารถเพิ่มข้อมูลได้");
            }
        }
        public void DeleteDocument<T>(FilterDefinition<T> filter)
        {
            try
            {
                GetCollection<T>().DeleteOne(filter);
            }
            catch
            {
                throw new Exception("ไม่สามารถเพิ่มข้อมูลได้");
            }
        }
        public async Task<List<T>> GetAllDocumentsAsync<T>()
        {
            try
            {
                return await GetCollection<T>().Find(x => true).ToListAsync();
            }
            catch
            {
                throw new Exception("ไม่สามารถค้นหาข้อมูลได้");
            }
        }
        public List<T> GetAllDocuments<T>()
        {
            try
            {
                return GetCollection<T>().Find(x => true).ToList();
            }
            catch
            {
                throw new Exception("ไม่สามารถค้นหาข้อมูลได้");
            }
        }
        public async Task<List<T>> GetFilteredDocumentsAsync<T>(FilterDefinition<T> filter)
        {
            try
            {
                return await GetCollection<T>().Find(filter).ToListAsync();
            }
            catch
            {
                throw new Exception("ไม่สามารถค้นหาข้อมูลได้");
            }
        }
        public List<T> GetFilteredDocuments<T>(FilterDefinition<T> filter)
        {
            try
            {
                return GetCollection<T>().Find(filter).ToList();
            }
            catch
            {
                throw new Exception("ไม่สามารถค้นหาข้อมูลได้");
            }
        }
        public async Task UpdateDocumentAsync<T>(FilterDefinition<T> filter, UpdateDefinition<T> document)
        {
            try
            {
                await GetCollection<T>().UpdateOneAsync(filter, document);
            }
            catch
            {
                throw new Exception("ไม่สามารถอัพเดทข้อมูลได้");
            }
        }
        public void UpdateDocument<T>(FilterDefinition<T> filter, UpdateDefinition<T> document)
        {
            try
            {
                GetCollection<T>().UpdateOne(filter, document);
            }
            catch
            {
                throw new Exception("ไม่สามารถอัพเดทข้อมูลได้");
            }
        }
        public async Task<T> FindOneAndUpdateDocumentAsync<T>(FilterDefinition<T> filter, UpdateDefinition<T> document)
        {
            try
            {
                var record = await GetCollection<T>().FindOneAndUpdateAsync(filter, document, options: new FindOneAndUpdateOptions<T>
                {
                    ReturnDocument = ReturnDocument.Before
                }).ConfigureAwait(false);
                return record;
            }
            catch
            {
                throw new Exception("ไม่สามารถอัพเดทข้อมูลได้");
            }
        }

    }
}
