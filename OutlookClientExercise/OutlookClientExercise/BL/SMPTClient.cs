using OutlookClientExercise.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutlookClientExercise
{
    public class SMPTClient : IDisposable
    {
        private SMPTServer _server;

        private MailAccount _currentConnectedAccount;
        private List<DefaultFolder> _folders;

        public SMPTServer Server => _server;
        public MailAccount CurrentConnectedAccount => _currentConnectedAccount;

        public bool IsConnected { get; set; }

        public SMPTClient(SMPTServer serverToConnect)
        {
            this._server = serverToConnect;
            IsConnected = false;
        }

        public bool Connect(string username, string password)
        {
            try
            {
                this._currentConnectedAccount = _server.AccessAccount(username, password);

                if (this._currentConnectedAccount == null)
                    throw new Exception("Account does not exists!");

                IsConnected = true;
                Initialize();

                ConsoleManager.ShowSuccess($"Connected to server: {Server.ServerName} with username: {this._currentConnectedAccount.Username} on port: {Server.Port}");

                ConsoleManager.ShowInfo("Wellcome!");
                ConsoleManager.ShowSuccess("Client started successfully.");

                return true;
            }
            catch (Exception ex)
            {
                ConsoleManager.ShowError(ex.Message);
                return false;
            }
        }

        public void Disconnect()
        {
            IsConnected = false;
            this._currentConnectedAccount = null;

            ConsoleManager.ShowInfo($"Disconnected from server {Server.ServerName}");
        }

        private void Initialize()
        {
            this._folders = this._currentConnectedAccount.Folders;
        }

        public bool SendMessage(List<string> toEmails, string body, string subject, List<string> carbonCopyEmails)
        {
            Message newMessage = new(
                            "192.168.0.1", 
                            this._currentConnectedAccount.Username, 
                            toEmails, 
                            body, 
                            DateTime.Now, 
                            subject != string.Empty ? subject : null, 
                            carbonCopyEmails);

            return this._server.ProcessMessage(newMessage);
        }

        public List<DefaultFolder> GetFolders()
        {
            return this._folders
                .OrderBy(f => f.Name)
                .ToList();
        }

        public bool AddFolder(string name)
        {
            try
            {
                if (this._folders.Exists(folder => folder.Name == name))
                    throw new Exception("The current folder already exists!");

                this._folders.Add(new DefaultFolder(name));
                
                return true;
            }
            catch (Exception ex)
            {
                ConsoleManager.ShowError(ex.Message);
                return false;
            }
        }

        public bool RemoveFolder(DefaultFolder folder)
        {
            try
            {
                if (folder.GetMessages().Count > 0)
                    throw new Exception("Can't delete, this folder is not empty");

                if (folder.IsProtected)
                    throw new Exception("Can't delete, this folder is protected");

                this._folders.Remove(folder);

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
            this._folders = null;
            this.Disconnect();
        }
    }
}
