using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutlookClientExercise
{
    public class SMPTServer
    {
        private string _description;
        private string _serverName;
        private int? _port;
        private List<MailAccount> _mailAccounts;

        public string ServerName => _serverName;
        public int? Port => _port;

        public SMPTServer(string description, string serverName, List<MailAccount> mailAccounts, int port = 537)
        {
            this._description = description;
            this._serverName = serverName;
            this._port = port;

            _mailAccounts = mailAccounts;
        }

        public MailAccount AccessAccount(string username, string password)
        {
            return this._mailAccounts
                .Find(c => c.Username == username && c.IsCorrectPassword(password));
        }

        public bool RegisterAccount(string username, string password)
        {
            try
            {
                if (this._mailAccounts.Exists(ma => ma.Username.ToUpper() == username.ToUpper()))
                    throw new Exception("That username, already exists!.");

                MailAccount newMailAccount = new(username, password);

                this._mailAccounts.Add(newMailAccount);

                return true;
            }
            catch (Exception ex)
            {
                ConsoleManager.ShowError(ex.Message);
                return false;
            }
        }

        public string GetInfo()
        {
            return $"Server: {_serverName} \nDescription: {_description} \nPort: {_port}";
        }

        public bool Update(string description, string serverName, int? port = 537)
        {
            try
            {
                if (string.IsNullOrEmpty(description) || string.IsNullOrWhiteSpace(description))
                    throw new ArgumentException("description must not be empty or null");

                if (string.IsNullOrEmpty(serverName) || string.IsNullOrWhiteSpace(serverName))
                    throw new ArgumentException("server name must not be empty or null");

                this._description = description;
                this._serverName = serverName;
                this._port = port;

                return true;
            }
            catch (Exception ex)
            {
                ConsoleManager.ShowError(ex.Message);
                return false;
            }
        }

        public bool ProcessMessage(Message message)
        {
            try
            {
                var originAccount = this._mailAccounts.Where(ma => ma.Username == message.From).FirstOrDefault();

                originAccount?.Folders
                    .Where(f => f.Name == Folder.SendedFolderName)
                    .FirstOrDefault()?
                    .AddMessage(message);

                foreach (var email in message.To)
                {
                    var targetAccount = this._mailAccounts.Where(ma => ma.Username == email).FirstOrDefault();

                    targetAccount?.Folders
                        .Where(f => f.Name == Folder.InboxFolderName)
                        .FirstOrDefault()?
                        .AddMessage(message);
                }

                foreach (var email in message.CarbonCopy)
                {
                    var targetAccount = this._mailAccounts.Where(ma => ma.Username == email).FirstOrDefault();

                    targetAccount?.Folders
                        .Where(f => f.Name == Folder.InboxFolderName)
                        .FirstOrDefault()?
                        .AddMessage(message);
                }

                return true;
            }
            catch (Exception ex)
            {
                ConsoleManager.ShowError(ex.Message);
                return false;
            }
        }
    }
}
