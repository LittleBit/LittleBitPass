using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LittleBitPass.Models;

namespace LittleBitPass
{
    public class Startup
    {
        public static void Init()
        {
            ConfigReader.ParseConfigFile();
        }
    }
}