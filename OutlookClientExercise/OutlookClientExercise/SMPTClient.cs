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
        private List<Folder> _folders;

        public SMPTClient()
        {
            _server = new("Default SMTP Server", "DefaultServer", "admin", "admin");

            Initialize();
        }


        private void Initialize()
        {
            ConsoleManager.ShowInfo("Wellcome!");
            ConsoleManager.ShowSuccess("Client started successfully.");

            var inboxFolder = new Folder("Inbox");
            inboxFolder.AddMessage(
                new Message("192.168.10.2", "dummy.email@gmail.com", new List<string> { "abel.lopez@gmail.com" }, "First test message for dummy", DateTime.Now));
            inboxFolder.AddMessage(
                new Message("192.168.10.2", "dummy.email@gmail.com", new List<string> { "abel.lopez@gmail.com" }, "Second test message for dummy", DateTime.Now));
            var sendedFolder = new Folder("Sended");
            sendedFolder.AddMessage(
                new Message("192.168.0.1", "abel.lopez@gmail.com", new List<string> { "dummy.email@gmail.com", "dummy2.email@gmail.com" }, "Broadcast message for dummies", DateTime.Now));
            var draftsFolder = new Folder("Drafts");
            draftsFolder.AddMessage(
                new Message(
                    "192.168.0.1", 
                    "abel.lopez@gmail.com", 
                    new List<string> { "dummy.email@gmail.com", "dummy2.email@gmail.com", "dummy3.email@gmail.com" }, 
                    "Draft Broadcast message for dummies", 
                    DateTime.Now, "Test message", 
                    new List<string>() { "my.admin@gmail.com" }));

            _folders = new List<Folder>() { inboxFolder, sendedFolder, draftsFolder };
        }

        public void SendMessage(Message message)
        {

        }

        public void Dispose()
        {
            this._server.Dispose();
        }
    }
}
