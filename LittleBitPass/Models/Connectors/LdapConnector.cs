using System.DirectoryServices.Protocols;
using System.Net;

namespace LittleBitPass.Models.Connectors
{
    public class LdapConnector
    {
        internal LdapConnection Connection;
		private string _base;

        public LdapConnector()
        {
            ConfigReader.Init(config =>
            {
                Connection = new LdapConnection(config.LdapServer)
                {
                    Credential = new NetworkCredential(config.LdapUser, config.LdapPassword, config.LdapDomain)							
                };
				_base = config.LdapTargetOu;
            });
        }

		public void TestQuery() {
			var req = new SearchRequest (_base, "objectClass=*", SearchScope.Subtree, new [] {"cn"});
			var returned = Connection.SendRequest (req);
		}			
    }
}