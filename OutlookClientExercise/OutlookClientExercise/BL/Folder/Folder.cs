using OutlookClientExercise.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OutlookClientExercise.BL
{
    public abstract class Folder : IMessageManager
    {
        protected List<Message> _messages;
        protected string _name;

        public Folder(string name)
        {
            this._name = name;

            this._messages = new List<Message>();
        }

        public virtual bool Rename(string newName)
        {
            try
            {
                this._name = newName;

                return true;
            }
            catch (Exception ex)
            {
                ConsoleManager.ShowError(ex.Message);

                return false;
            }
        }

        public List<Message> GetMessages()
        {
            return this._messages
                .OrderBy(m => m.Date)
                .ToList();
        }

        public abstract void AddMessage(MailAccount mailAccount, Message message, MessageAction messageAction);

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

        public bool MoveMessage(MailAccount mailAccount, Message message, Folder destinyFolder)
        {
            try
            {
                var targetMessage = this._messages
                    .Where(m => m == message)
                    .FirstOrDefault();

                if (targetMessage == null)
                    throw new Exception("That message does not exist!");

                destinyFolder.AddMessage(mailAccount, targetMessage, MessageAction.MessageMoved);

                this._messages.Remove(targetMessage);

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
