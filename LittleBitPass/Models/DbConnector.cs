using System;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace LittleBitPass
{
	public class DbConnector
	{
		// Singleton, prevent multiple DB connections.
		public static readonly DbConnector Instance = new DbConnector();

		public MySqlConnection Connection;

		private DbConnector() {
			Connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["DbConnString"].ConnectionString);
		}

	}
}

