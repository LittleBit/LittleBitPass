using System;
using System.Collections.Generic;
using System.IO;

namespace LittleBitPass
{
	public static class ConfigReader
	{
		private const string DB_NAME = "dbName", DB_USERNAME = "dbUsername", DB_PASSWORD = "dbPassword", DB_ADDRESS = "dbAddress", DB_PORT = "dbPort";
		private static readonly string[] ConfigKeys = new [] { DB_NAME, DB_USERNAME, DB_PASSWORD, DB_ADDRESS, DB_PORT };

		public static void Init(Action<ConfigFile> ready)
		{
			ParseConfigFile(configFile =>
			{
				ready(configFile);
			}, msg =>
			{
				ready(new ConfigFile());
			});
		}

		public static void ParseConfigFile(Action<ConfigFile> success, Action<string> fail)
		{
			const string ConfigFilePath = "config";
			const char ConfigSeperator = ':';
			if (File.Exists(ConfigFilePath))
			{
				var dict = new Dictionary<string, string>();
				var textReader = File.OpenText(ConfigFilePath);
				var line = "";
				while ((line = textReader.ReadLine()) != null) {
					var kvp = line.Split(ConfigSeperator);
					if (kvp.Length == 2) {
						dict[kvp[0]] = kvp[1];
					}
					else {
						// not a (valid) config entry
					}
				}
				textReader.Dispose ();
				foreach (var cKey in ConfigKeys)
				{
					if (!dict.ContainsKey(cKey))
					{
						fail("Config file is missing " + cKey + "  - ");
						return;
					}
				}
				success(new ConfigFile(dict[DB_NAME],dict[DB_USERNAME],dict[DB_PASSWORD],dict[DB_ADDRESS],dict[DB_PORT]));
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

		public ConfigFile()
		{
			DbName = "database";
			DbUsername = "root";
			DbPassword = "toor";
			DbAddress = "localhost";
			DbPort = "3306";
		}

		public ConfigFile(string dbName, string dbUsername, string dbPassword, string dbAdress, string dbPort)
		{
			DbName = dbName;
			DbUsername = dbUsername;
			DbPassword = dbPassword;
			DbAddress = dbAdress;
			DbPort = dbPort;
		}
	}
}

