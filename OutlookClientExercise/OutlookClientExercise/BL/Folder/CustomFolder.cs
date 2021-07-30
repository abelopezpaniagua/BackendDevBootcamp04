using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutlookClientExercise
{
    class CustomFolder : Folder
    {
        public CustomFolder(string name) : base(name)
        {
        }

        public override void AddMessage(MailAccount mailAccount, Message message, MessageAction messageAction)
        {
            this._messages.Add(message);
        }
    }
}
