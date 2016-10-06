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
            DbPasswordCollectionName = "dbPwdCollName",
            LdapServer = "ldapServer",
            LdapUser = "ldapUser",
            LdapPassword = "ldapPassword",
            LdapDomain = "ldapDomain",
            LdapTargetOu = "ldapOu";

        private static readonly string[] ConfigKeys =
        {
            DbName, DbUsername, DbPassword, DbAddress, DbPort, DbPasswordCollectionName,
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
            => "mongodb://";
        
    }
    public class ConfigFile : Dictionary<string, string>
    {
        public static Dictionary<string, string> Config = new ConfigFile();

        private ConfigFile()
        {
            Add(ConfigReader.DbName, "local");
            Add(ConfigReader.DbUsername, "");
            Add(ConfigReader.DbPassword, "");
            Add(ConfigReader.DbAddress, "localhost");
            Add(ConfigReader.DbPort, "27017");
            Add(ConfigReader.DbPasswordCollectionName, "Passwords");

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

