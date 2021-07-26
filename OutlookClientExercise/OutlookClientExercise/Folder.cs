using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutlookClientExercise
{
    class Folder
    {
        private List<Message> _messages;
        private string _name;

        public Folder(string name)
        {
            _name = name;

            _messages = new List<Message>();
        }

        public void AddMessage(Message message)
        {
            _messages.Add(message);
        }
    }
}
