using System;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Threading.Tasks;
using System.Data.Common;

namespace LittleBitPass
{
	/// <summary>
	/// This class provides the connection to the database and handles queries to return a datareader.
	/// </summary>
	public class DbConnector
	{
		private ConfigFile _config;

		internal MySqlConnection Connection;
		public DbConnector() {
			ConfigReader.Init(config => _config = config);
			Connection = new MySqlConnection(DbConnString);
			try {
				Connection.Open();
			}
			catch (MySqlException ex) {
				Console.WriteLine (ex.Message);
			}
		}

		private string DbConnString => "Server=" + _config.DbAddress + ";Uid=" + _config.DbUsername + ";Pwd=" + _config.DbPassword + ";Database=" + _config.DbName + ";Port=" + _config.DbPort + ";";


		/// <summary>
		/// Runs the query sync against the database and returns a datareader.
		/// </summary>
		/// <returns>A datareader containing the result of the query.</returns>
		/// <param name="query">Query.</param>
		public MySqlDataReader RunQuery(string query) {
			var cmd = new MySqlCommand(query, Connection);
			return cmd.ExecuteReader();
		}

		/// <summary>
		/// Runs the query sync against the database and returns a datareader.
		/// </summary>
		/// <returns>A task containing a datareader to access the result of the query.</returns>
		/// <param name="query">Query.</param>
		public Task<DbDataReader> RunQueryAsync(string query) {
			var cmd = new MySqlCommand(query, Connection);
			return cmd.ExecuteReaderAsync();
		}
	}
}