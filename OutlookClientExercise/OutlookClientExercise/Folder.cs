using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutlookClientExercise
{
    public class Folder
    {
        private List<Message> _messages;
        private string _name;
        private bool _isProtected;

        public const string InboxFolderName = "Inbox";
        public const string SendedFolderName = "Sended";
        public const string DraftsFolderName = "Drafts";
        public const string SpamFolderName = "Spam";

        public string Name => _name;
        public bool IsProtected => _isProtected;

        public Folder(string name, bool isProtected = false)
        {
            _name = name;
            _isProtected = isProtected;

            _messages = new List<Message>();
        }

        public List<Message> GetMessages()
        {
            return this._messages
                .OrderBy(m => m.Date)
                .ToList();
        }

        public void AddMessage(Message message)
        {
            _messages.Add(message);
        }

        public bool DeleteMessage(Message message)
        {
            try
            {
                this._messages.Remove(message);

                return true;
            }
            catch (Exception ex)
            {
                ConsoleManager.ShowError(ex.Message);

                return false;
            }
        }

        public bool MoveMessage(Message message, Folder destinyFolder)
        {
            try
            {
                var targetMessage = this._messages
                    .Where(m => m == message)
                    .FirstOrDefault();

                if (targetMessage == null)
                    throw new Exception("That message does not exist!");

                destinyFolder.AddMessage(targetMessage);

                this._messages.Remove(targetMessage);

                return true;
            }
            catch (Exception ex)
            {
                ConsoleManager.ShowError(ex.Message);

                return false;
            }
        }

        public bool Rename(string newName)
        {
            try
            {
                if (_isProtected)
                    throw new Exception("Can't remane, this folder is protected");

                _name = newName;

                return true;
            } 
            catch(Exception ex)
            {
                ConsoleManager.ShowError(ex.Message);

                return false;
            }
        }
    }
}
