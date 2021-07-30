using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutlookClientExercise.BL
{
    public interface IManageRules
    {
        List<FolderRule> GetActiveRules();
        List<FolderRule> GetAvailableRules();
        void AddActiveRule(FolderRule folderRule);
        void RemoveActiveRule(FolderRule folderRule);
        void ExecuteActiveRules(MailAccount mailAccount, Folder folder, Message message, MessageAction messageAction);
    }
}
