namespace OutlookClientExercise.BL
{
    public interface IMessageManager
    {
        void AddMessage(MailAccount mailAccount, Message message, MessageAction messageAction);
        bool DeleteMessage(Message message);
        bool MoveMessage(MailAccount mailAccount, Message message, Folder destinyFolder);

    }
}
