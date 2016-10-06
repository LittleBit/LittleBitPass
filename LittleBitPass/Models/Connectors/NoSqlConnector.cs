using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LittleBitPass.Models.Connectors
{
    public class NoSqlConnector
    {
        private static IMongoDatabase _database;
        private static IMongoCollection<BsonDocument> _entityCollection;

        public static readonly NoSqlConnector Connector = new NoSqlConnector();

        private NoSqlConnector()
        {
            int port;
            var urlBuilder = new MongoUrlBuilder
            {
                Server = int.TryParse(ConfigFile.Config[ConfigReader.DbPort], out port)
                    ? new MongoServerAddress(ConfigFile.Config[ConfigReader.DbAddress], port)
                    : new MongoServerAddress(ConfigFile.Config[ConfigReader.DbAddress])
            };
            if (ConfigFile.Config[ConfigReader.DbUsername] != "" && ConfigFile.Config[ConfigReader.DbPassword] != "")
            {
                urlBuilder.Username = ConfigFile.Config[ConfigReader.DbUsername];
                urlBuilder.Password = ConfigFile.Config[ConfigReader.DbPassword];
            }
            
            var client = new MongoClient(urlBuilder.ToMongoUrl());
            _database = client.GetDatabase(ConfigFile.Config[ConfigReader.DbName]);
            _entityCollection = _database.GetCollection<BsonDocument>(ConfigFile.Config[ConfigReader.DbPasswordCollectionName]);
        }

        public Task InsertOnePasswordAsync(BsonDocument doc)
        {
            return _entityCollection.InsertOneAsync(doc);
        }

        public async void CreatePassword(string username, BsonDocument pass)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("username", username);
            var update = Builders<BsonDocument>.Update.AddToSet("passwords", pass);
            await _entityCollection.FindOneAndUpdateAsync(filter, update);
        }
    }
}