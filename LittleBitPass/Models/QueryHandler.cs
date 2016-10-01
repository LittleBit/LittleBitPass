using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LittleBitPass.Models.Connectors;
using MySql.Data.MySqlClient;

namespace LittleBitPass.Models
{
    public class QueryHandler
    {

        public static void GetEntry(int index)
        {
            var query = $"SELECT * FROM Entry WHERE entryid = {index}";
            DbConnector.RunQueryAsync(query, reader =>
            {
                reader.Read();
            }, s =>
            {
                
            });
        }
    }
}