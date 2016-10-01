using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Web.SessionState;

namespace LittleBitPass.Models
{
    internal static class ConfigReader
    {
        private const char ConfigSeperator = ':';

        public const string DbName = "dbName",
            DbUsername = "dbUsername",
            DbPassword = "dbPassword",
            DbAddress = "dbAddress",
            DbPort = "dbPort",
            LdapServer = "ldapServer",
            LdapUser = "ldapUser",
            LdapPassword = "ldapPassword",
            LdapDomain = "ldapDomain",
            LdapTargetOu = "ldapOu";

        private static readonly string[] ConfigKeys =
        {
            DbName, DbUsername, DbPassword, DbAddress, DbPort,
            LdapServer, LdapUser, LdapPassword, LdapDomain, LdapTargetOu
        };

        public static void ParseConfigFile()
        {
            var configFilePath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "config";
            if (File.Exists(configFilePath))
            {
                var dict = new Dictionary<string, string>();
                using (var textReader = File.OpenText(configFilePath))
                {
                    string line;
                    while ((line = textReader.ReadLine()) != null)
                    {
                        var kvp = line.Split(ConfigSeperator);
                        if (kvp.Length == 2)
                            dict[kvp[0]] = kvp[1];
                    }
                }
                foreach (var cKey in ConfigKeys)
                    if (!dict.ContainsKey(cKey))
                    {
                        dict.Add(cKey, ConfigFile.Config[cKey]);
                        Debug.WriteLine(
                            $"Config file is missing '{cKey}', default '{ConfigFile.Config[cKey]}' value is used");
                    }
                ConfigFile.Config = dict;
            }
            else
                Debug.WriteLine("!!! Config file does not exist. Default values are used. !!!");
        }

        public static string DbConnString
            => "Server=" + ConfigFile.Config[DbAddress] + ";Uid=" + ConfigFile.Config[DbUsername] + ";Pwd=" +
               ConfigFile.Config[DbPassword] + ";Database=" +
               ConfigFile.Config[DbName] + ";Port=" + ConfigFile.Config[DbPort] + ";";

        public class ConfigFile : Dictionary<string, string>
        {
            public static Dictionary<string, string> Config = new ConfigFile();

            private ConfigFile()
            {
                Add(ConfigReader.DbName, "database");
                Add(ConfigReader.DbUsername, "root");
                Add(ConfigReader.DbPassword, "toor");
                Add(ConfigReader.DbAddress, "localhost");
                Add(ConfigReader.DbPort, "3306");

                Add(ConfigReader.LdapServer, "localhost");
                Add(ConfigReader.LdapUser, "user");
                Add(ConfigReader.LdapPassword, "password");
                Add(ConfigReader.LdapDomain, "localhost");
                Add(ConfigReader.LdapTargetOu, "DC=test,DC=com");
            }

            internal static void SetConfig(Dictionary<string, string> config)
            {
                Config = config;
            }
        }
    }
}

