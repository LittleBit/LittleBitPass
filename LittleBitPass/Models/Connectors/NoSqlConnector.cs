using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LittleBitPass.Models.Connectors
{
    public class NoSqlConnector
    {
        private static IMongoClient _client;
        private static IMongoDatabase _database;

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
            
            _client = new MongoClient(urlBuilder.ToMongoUrl());
            _database = _client.GetDatabase(ConfigFile.Config[ConfigReader.DbName]);
        }


        public void TestConnector()
        {
            var coll = _database.GetCollection<BsonDocument>("entities");
            var cursor = coll.FindSync(new BsonDocument());
            cursor.MoveNext();
            ;
        }

        public void SavePasswordEntity(string userId)
        {
            var document = new BsonDocument
            {
                { "username", userId },
                {
                    "passwords", new BsonDocument
                    {
                        { "password_id", 1 },
                        { "username", "anusma" },
                        { "password", "jeoma" }
                    }
                }
            };
            var coll = _database.GetCollection<BsonDocument>("entities");
            coll.InsertOne(document);
        }
    }
}