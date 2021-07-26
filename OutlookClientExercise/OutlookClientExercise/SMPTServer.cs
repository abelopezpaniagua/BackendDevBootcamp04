using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutlookClientExercise
{
    public class SMPTServer : IDisposable
    {
        private string _description;
        private string _serverName;
        private int _port;
        private string _username;
        private string _password;

        public SMPTServer(string description, string serverName, string username, string password, int port = 537)
        {
            this._description = description;
            this._serverName = serverName;
            this._port = port;
            this._username = username;
            this._password = password;

            this.Connect();
        }

        private void Connect()
        {
            ConsoleManager.ShowSuccess($"Connected to server: {_serverName} with username: {_username} on port: {_port}");
        }

        private void Disconnect()
        {
            ConsoleManager.ShowInfo($"Disconnected from server {_serverName}");
        }

        public void Dispose()
        {
            this.Disconnect();
        }
    }
}
