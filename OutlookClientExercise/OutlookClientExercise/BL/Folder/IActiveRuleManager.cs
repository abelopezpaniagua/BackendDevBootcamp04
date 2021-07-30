using System.Collections.Generic;

namespace OutlookClientExercise.BL
{
    public interface IActiveRuleManager
    {
        List<FolderRule> GetActiveRules();
        List<FolderRule> GetAvailableRules();
        void AddActiveRule(FolderRule folderRule);
        void RemoveActiveRule(FolderRule folderRule);
    }
}
