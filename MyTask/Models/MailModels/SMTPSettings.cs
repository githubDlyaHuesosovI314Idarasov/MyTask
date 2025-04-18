using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace MyTaskApp.Models.MailModels
{
    public sealed class SMTPSettings
    {
        private string? _host;
        private int _port;
        private bool _enableSSL;
        private string? _username;
        private string? _password;
        private string _from;



        public string? Host { get { return _host; } set { _host = value; } }
        public int Port { get { return _port; } set { _port = value; } }
        public bool EnableSSL { get { return _enableSSL; } set { _enableSSL = value; } }
        public string? Username { get { return _username; } set { _username = value; } }
        public string? Password { get { return _password; } set { _password = value; } }
        public string From { get { return _from; } set { _from = value; } }
    }
}
