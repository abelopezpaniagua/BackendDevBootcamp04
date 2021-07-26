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
        private int? _port;
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

        public string GetInfo()
        {
            return $"Server: {_serverName} \nDescription: {_description} \nPort: {_port}";
        }

        public bool Update(string description, string serverName, string username, string password, int? port = 537)
        {
            try
            {
                if (string.IsNullOrEmpty(description) || string.IsNullOrWhiteSpace(description))
                    throw new ArgumentException("description must not be empty or null");

                if (string.IsNullOrEmpty(serverName) || string.IsNullOrWhiteSpace(serverName))
                    throw new ArgumentException("server name must not be empty or null");

                if (string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(username))
                    throw new ArgumentException("username must not be empty or null");

                if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
                    throw new ArgumentException("password must not be empty or null");

                this._description = description;
                this._serverName = serverName;
                this._port = port;
                this._username = username;
                this._password = password;

                return true;
            }
            catch (Exception ex)
            {
                ConsoleManager.ShowError(ex.Message);
                return false;
            }
        }

        public void Dispose()
        {
            this.Disconnect();
        }
    }
}
