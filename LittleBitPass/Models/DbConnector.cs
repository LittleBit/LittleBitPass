using System;
using Devart.Data.PostgreSql;
using System.Configuration;
using System.Threading.Tasks;
using System.Data.Common;

namespace LittleBitPass
{
	public class DbConnector
	{
		// Singleton, prevent multiple DB connections.
		public static readonly DbConnector Instance = new DbConnector();

		public PgSqlConnection Connection;

		DbConnector() {
			Connection = new PgSqlConnection(ConfigurationManager.ConnectionStrings["DbConnString"].ConnectionString);
			try {				
				Connection.Open();
			} catch (Exception ex) {
				Console.WriteLine (ex.Message);
			}
		}

		public PgSqlDataReader RunQuery(string query) {
			var test = new PgSqlCommand(query, Connection);
			return test.ExecuteReader();
		}

		public Task<DbDataReader> RunQueryAsync(string query) {
			var test = new PgSqlCommand(query, Connection);
			return test.ExecuteReaderAsync();
		}
	}
}