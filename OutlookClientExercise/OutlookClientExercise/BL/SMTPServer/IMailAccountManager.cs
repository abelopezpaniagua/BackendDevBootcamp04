using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutlookClientExercise.BL
{
    public interface IMailAccountManager
    {
        public MailAccount AccessAccount(string username, string password);
        public bool RegisterAccount(string username, string password);
    }
}
