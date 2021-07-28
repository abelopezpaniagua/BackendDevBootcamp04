using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutlookClientExercise
{
    public class SMPTClient : IDisposable
    {
        public SMPTServer Server;
        private List<Folder> _folders;

        public SMPTClient()
        {
            Server = new("Default SMTP Server", "DefaultServer", "admin", "admin");

            Initialize();
        }


        private void Initialize()
        {
            ConsoleManager.ShowInfo("Wellcome!");
            ConsoleManager.ShowSuccess("Client started successfully.");

            var inboxFolder = new Folder("Inbox", true);
            inboxFolder.AddMessage(
                new Message("192.168.10.2", "dummy.email@gmail.com", new List<string> { "abel.lopez@gmail.com" }, "First test message for dummy", DateTime.Now));
            inboxFolder.AddMessage(
                new Message("192.168.10.2", "dummy.email@gmail.com", new List<string> { "abel.lopez@gmail.com" }, "Second test message for dummy", DateTime.Now));
            var sendedFolder = new Folder("Sended", true);
            sendedFolder.AddMessage(
                new Message("192.168.0.1", "abel.lopez@gmail.com", new List<string> { "dummy.email@gmail.com", "dummy2.email@gmail.com" }, "Broadcast message for dummies", DateTime.Now));
            var draftsFolder = new Folder("Drafts", true);
            draftsFolder.AddMessage(
                new Message(
                    "192.168.0.1", 
                    "abel.lopez@gmail.com", 
                    new List<string> { "dummy.email@gmail.com", "dummy2.email@gmail.com", "dummy3.email@gmail.com" }, 
                    "Draft Broadcast message for dummies", 
                    DateTime.Now, "Test message", 
                    new List<string>() { "my.admin@gmail.com" }, true));

            _folders = new List<Folder>() { inboxFolder, sendedFolder, draftsFolder };
        }

        public void SendMessage(Message message)
        {

        }

        public List<Folder> GetFolders()
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

                this._folders.Add(new Folder(name));
                
                return true;
            }
            catch (Exception ex)
            {
                ConsoleManager.ShowError(ex.Message);
                return false;
            }
        }

        public bool RemoveFolder(Folder folder)
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
            this.Server.Dispose();
        }
    }
}
