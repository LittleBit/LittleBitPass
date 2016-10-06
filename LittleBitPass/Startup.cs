using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LittleBitPass.Models;
using LittleBitPass.Models.Connectors;
using LittleBitPass.Models.NoSqlModels;
using MongoDB.Bson;

namespace LittleBitPass
{
    public class Startup
    {
        public static void Init()
        {
            ConfigReader.ParseConfigFile();

            var q = Math.Pow(9, 0.5);

            var test = NoSqlConnector.Connector;
            var item = new List<BsonElement>
            {
                new BsonElement("name", "League"),
                new BsonElement("username", "labtox"),
                new BsonElement("password", "YourAnusWillBeMine"),
            };
            var items = BsonBuilders.AddPassword("labtox", item);
            test.InsertOnePasswordAsync(items);
        }
    }
}