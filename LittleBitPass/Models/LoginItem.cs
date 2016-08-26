using System;

namespace LittleBitPass
{
	public class LoginItem
	{
		public LoginItem (string username, string password, string[] fileLocations)
		{
			Username = username;
			Password = password;
			FileLocations = fileLocations;
		}

		public string Username { private set; get; }
		public string Password { private set; get; }
		public List<string> FileLocations { private set; get; }
	}
}