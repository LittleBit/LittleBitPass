using System;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace LittleBitPass.Models.Connectors
{
	/// <summary>
	/// This class provides the connection to the database and handles queries to return a datareader.
	/// </summary>
	public class DbConnector
	{
		internal MySqlConnection Connection;

		public DbConnector() 
		{
			ConfigReader.Init(config => {
				Connection = new MySqlConnection(ConfigReader.DbConnStrFromConfig(config));
			});
			try 
			{
				Connection.Open();
			}
			catch (MySqlException ex) {
				Console.WriteLine ("There is a problem connection to the server: " + ex.Message);
			}
		}

		/// <summary>
		/// Runs the query async against the database as readonly and returns a datareader.
		/// </summary>
		/// <param name="query">The query to run</param>
		/// <param name="succes">Run input action with as output datareader</param>
		/// <param name="fail">Run input action with as output a string with the error message</param>
		public void RunQueryAsync(string query, Action<DbDataReader> success, Action<string> fail) {
			try
			{
				Connection.OpenAsync();
				var cmd = new MySqlCommand(query, Connection);
				success(cmd.ExecuteReader());
				Connection.CloseAsync();
			}
			catch (MySqlException ex)
			{
				fail(ex.Message);
			}
		}			
	}
}