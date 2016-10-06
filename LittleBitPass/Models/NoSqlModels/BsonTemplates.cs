using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;
using MongoDB.Bson;

namespace LittleBitPass.Models.NoSqlModels
{
    public class BsonTemplates
    {
        private static BsonDocument DefaultLogin(string name, string username, SecureString password)
        {
            return new BsonDocument
            {
                {"name", name},
                {"username", username},
                {"password", password.ToString()}
            };
        }

        public static BsonDocument HttpLogin(string lbUsername, string name, string username, SecureString password)
        {
            return new BsonDocument
            {
                { "username", lbUsername },
                { "passwordInfo", DefaultLogin(name, username, password) }
            }; ;
        }
    }
}