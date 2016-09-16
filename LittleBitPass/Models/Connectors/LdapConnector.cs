using System.DirectoryServices.Protocols;
using System.Net;

namespace LittleBitPass.Models.Connectors
{
    public class LdapConnector
    {
        internal LdapConnection Connection;

        public LdapConnector()
        {
            ConfigReader.Init(config =>
            {
                Connection = new LdapConnection(config.LdapServer)
                {
                    Credential = new NetworkCredential(config.LdapUser, config.LdapPassword, config.LdapDomain)
                };

            });
        }
    }
}