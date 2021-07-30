using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutlookClientExercise
{
    public class MailAccount
    {
        private string _username;
        private string _password;

        private List<Folder> _folders;

        public string Username => _username;
        public List<Folder> Folders => _folders;

        public MailAccount(string username, string password)
        {
            _username = username;
            _password = password;

            InitializeDefaultFolders();
        }

        public MailAccount(string username, string password, MessagesMock messagesMock)
        {
            _username = username;
            _password = password;

            InitializeDefaultFolders();

            var mockMessagesGenerated = messagesMock.GetMockMessages();

            foreach (var message in mockMessagesGenerated)
            {
                this._folders.Find(f => f.Name == Folder.SendedFolderName).AddMessage(message);
            }
        }

        private void InitializeDefaultFolders()
        {
            var inboxFolder = new Folder(Folder.InboxFolderName, true);
            var sendedFolder = new Folder(Folder.SendedFolderName, true);
            var draftsFolder = new Folder(Folder.DraftsFolderName, true);
            var spamFolder = new Folder(Folder.SpamFolderName, true);

            this._folders = new List<Folder>() { inboxFolder, sendedFolder, draftsFolder, spamFolder };
        }

        public bool IsCorrectPassword(string password)
        {
            return this._password.ToUpper() == password.ToUpper();
        }
    }
}
