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

        private void InitializeDefaultFolders()
        {
            var inboxFolder = new DefaultFolder(DefaultFolder.InboxFolderName, true);
            var sendedFolder = new DefaultFolder(DefaultFolder.SendedFolderName, true);
            var draftsFolder = new DefaultFolder(DefaultFolder.DraftsFolderName, true);
            var spamFolder = new DefaultFolder(DefaultFolder.SpamFolderName, true);

            this._folders = new List<Folder>() { inboxFolder, sendedFolder, draftsFolder, spamFolder };
        }

        public bool IsCorrectPassword(string password)
        {
            return this._password.ToUpper() == password.ToUpper();
        }
    }
}
