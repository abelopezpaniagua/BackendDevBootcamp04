namespace OutlookClientExercise.BL
{
    public interface IActiveRuleExecuter
    {
        void ExecuteActiveRules(MailAccount mailAccount, DefaultFolder folder, Message message, MessageAction messageAction);
    }
}
