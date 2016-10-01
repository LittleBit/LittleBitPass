using System;
using System.Data.Common;
using System.Diagnostics;
using MySql.Data.MySqlClient;

namespace LittleBitPass.Models.Connectors
{
	/// <summary>
	/// This class provides the connection to the database and handles queries to return a datareader.
	/// </summary>
	public static class DbConnector
	{

		/// <summary>
		/// Runs the query async against the database as readonly and returns a datareader.
		/// </summary>
		/// <param name="query">The query to run</param>
		/// <param name="success">Run input action with as output datareader</param>
		/// <param name="fail">Run input action with as output a string with the error message</param>
		public static void RunQueryAsync(string query, Action<DbDataReader> success, Action<string> fail) {
			try
			{
			    using (var conn = new MySqlConnection(ConfigReader.DbConnString))
			    {
			        conn.OpenAsync();
			        success(new MySqlCommand(query, conn).ExecuteReader());
			        conn.CloseAsync();
			    }
			}
			catch (MySqlException ex)
			{
				fail(ex.Message);
			}
		}			
	}
}