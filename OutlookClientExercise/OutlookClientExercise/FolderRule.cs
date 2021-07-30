using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutlookClientExercise
{
    public class FolderRule
    {
        private string _name;
        private string _description;

        public string Name => _name;
        public string Description => _description;
        public Action<MailAccount, Folder, Message, MessageAction> Trigger;

        public FolderRule(string name, string description, Action<MailAccount, Folder, Message, MessageAction> trigger)
        {
            this._name = name;
            this._description = description;
            this.Trigger = trigger;
        }

        public void HandleEvent(MailAccount mailAccount, Folder folder, Message message, MessageAction messageAction)
        {
            Trigger.Invoke(mailAccount, folder, message, messageAction);
        }
    }
}
