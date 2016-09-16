using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace LittleBitPass
{
	public static class ConfigReader
	{
		private const string DB_NAME = "dbName", DB_USERNAME = "dbUsername", DB_PASSWORD = "dbPassword", DB_ADDRESS = "dbAddress", DB_PORT = "dbPort", LDAP_SERVER = "ldapServer", LDAP_USER = "ldapUser", LDAP_PASSWORD = "ldapPassword", LDAP_DOMAIN = "ldapDomain", LDAP_TARGETOU = "ldapOu";
		private static readonly string[] ConfigKeys = { DB_NAME, DB_USERNAME, DB_PASSWORD, DB_ADDRESS, DB_PORT, LDAP_SERVER, LDAP_USER, LDAP_PASSWORD, LDAP_DOMAIN, LDAP_TARGETOU };

		public static void Init(Action<ConfigFile> ready)
		{
			ParseConfigFile(ready, msg =>
			{
				ready(new ConfigFile());
			});
		}

		public static void ParseConfigFile(Action<ConfigFile> success, Action<string> fail)
		{
			string configFilePath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "config";
			const char configSeperator = ':';
            if (File.Exists(configFilePath))
			{
				var dict = new Dictionary<string, string>();
				var textReader = File.OpenText(configFilePath);
				var line = "";
				while ((line = textReader.ReadLine()) != null) {
					var kvp = line.Split(configSeperator);
					if (kvp.Length == 2) {
						dict[kvp[0]] = kvp[1];
					}
				}
				textReader.Dispose ();
				foreach (var cKey in ConfigKeys)
				{
					if (!dict.ContainsKey(cKey))
					{
						fail("Config file is missing: " + cKey);
						return;
					}
				}
				success(new ConfigFile(dict[DB_NAME],dict[DB_USERNAME],dict[DB_PASSWORD],dict[DB_ADDRESS],dict[DB_PORT], dict[LDAP_SERVER], dict[LDAP_USER], dict[LDAP_PASSWORD], dict[LDAP_DOMAIN], dict[LDAP_TARGETOU]));
			}
			else {
				fail("Config file does not exist!");
			}
		}

		public static string DbConnStrFromConfig(ConfigFile file)
		{			
			return "Server=" + file.DbAddress + ";Uid=" + file.DbUsername + ";Pwd=" + file.DbPassword + ";Database=" + file.DbName + ";Port=" + file.DbPort + ";";
		}
	}

	public class ConfigFile
	{
		public readonly string DbName, DbUsername, DbPassword, DbAddress, DbPort;
		public readonly string LdapServer, LdapUser, LdapPassword, LdapDomain, LdapTargetOu;

		public ConfigFile()
		{
			DbName = "database";
			DbUsername = "root";
			DbPassword = "toor";
			DbAddress = "localhost";
			DbPort = "3306";

			LdapServer = "localhost";
			LdapUser = "user";
			LdapPassword = "password";
			LdapDomain = "localhost";
			LdapTargetOu = "DC=test,DC=com";
		}

		public ConfigFile(string dbName, string dbUsername, string dbPassword, string dbAdress, string dbPort, string ldapServer, string ldapUser, string ldapPassword, string ldapDomain, string ldapTargetOu)
		{
			DbName = dbName;
			DbUsername = dbUsername;
			DbPassword = dbPassword;
			DbAddress = dbAdress;
			DbPort = dbPort;

			LdapServer = ldapServer;
			LdapUser = ldapUser;
			LdapPassword = ldapPassword;
			LdapDomain = ldapDomain;
			LdapTargetOu = ldapTargetOu;
		}
	}
}

