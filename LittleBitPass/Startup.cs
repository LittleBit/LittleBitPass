using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LittleBitPass.Models;
using LittleBitPass.Models.Connectors;

namespace LittleBitPass
{
    public class Startup
    {
        public static void Init()
        {
            ConfigReader.ParseConfigFile();
            var test = NoSqlConnector.Connector;
            test.TestConnector();
            test.SavePasswordEntity(1);
        }
    }
}