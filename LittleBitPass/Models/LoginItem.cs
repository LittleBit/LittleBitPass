namespace LittleBitPass.Models
{
	public class LoginItem
	{
		public LoginItem (string username, string password)
		{
			Username = username;
			Password = password;
		}

		public string Username { private set; get; }
		public string Password { private set; get; }
	}
}