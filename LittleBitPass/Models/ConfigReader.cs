using System;
using System.IO;

namespace LittleBitPass
{
	public static class ConfigReader
	{
		public static ConfigFile Init()
		{
			return null;
//			FindConfigFile(() =>
//			{
//				return new ConfigFile();
//			}, () =>
//			{
//				return new ConfigFile();
//			});
		}

		public static void FindConfigFile(Action<StringReader> found, Action notFound)
		{
			const string ConfigFilePath = "config";
			if (File.Exists(ConfigFilePath))
			{
				File.OpenText(ConfigFilePath);
			}
			else {
				notFound();
			}
		}
	}

	public class ConfigFile
	{
		public readonly string DbUsername, DbPassword, DbAddress;

		public ConfigFile()
		{
			DbUsername = "Fap me baby one more time";
			DbPassword = "Will I Am, it's Britney b1tch!-";
			DbAddress = "8.8.4.4";
		}

		public ConfigFile(string dbUsername, string dbPassword, string dbAdress)
		{
			DbUsername = dbUsername;
			DbPassword = dbPassword;
			DbAddress = dbAdress;
		}
	}
}

