using OutlookClientExercise.BL;
using OutlookClientExercise.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OutlookClientExercise
{
    public delegate void ExecuteRule(MailAccount mailAccount, DefaultFolder folder, Message message, MessageAction messageAction);

    public class DefaultFolder : Folder, IActiveRuleManager, IActiveRuleExecuter
    {
        private bool _isProtected;

        private List<FolderRule> _availableRules;
        private List<FolderRule> _activeRules;

        private event ExecuteRule ExecuteRuleEvent;

        public const string InboxFolderName = "Inbox";
        public const string SendedFolderName = "Sended";
        public const string DraftsFolderName = "Drafts";
        public const string SpamFolderName = "Spam";

        public bool IsProtected => _isProtected;

        public DefaultFolder(string name, bool isProtected = false) : base (name)
        {
            this._isProtected = isProtected;

            this._activeRules = new List<FolderRule>();

            InitializeAvailableRules();
        }

        private void InitializeAvailableRules()
        {
            this._availableRules = new List<FolderRule>();

            FolderRule moveMessageToSpamRuleOnReceive = new(
                "Move to Spam", 
                "Move messages to Spam Folder automatically, when includes suspicious words",
                (mailAccount, folder, message, messageAction) =>
                {
                    if (messageAction == MessageAction.MessageReceived)
                    {
                        if (message.Body.Contains("promotion") || message.Body.Contains("offer"))
                        {
                            mailAccount.Folders
                                .Find(f => f.Name == DefaultFolder.SpamFolderName)
                                .AddMessage(mailAccount, message, MessageAction.MessageMoved);

                            folder.DeleteMessage(message);

                            ConsoleManager.ShowWarning($"Event Trigger -> Move message: {message.Body} from folder: {folder.Name} to SPAM!");
                        }
                    }
                });
            
            this._availableRules.Add(moveMessageToSpamRuleOnReceive);

            FolderRule deleteMessageWithToManyCopies = new(
                "Delete message with many copies",
                "Delete recieved message when the message, contains to many copies to target",
                (mailAccount, folder, message, messageAction) =>
                {
                    if (messageAction == MessageAction.MessageReceived)
                    {
                        if (message.CarbonCopy.Count > 3)
                        {
                            folder.DeleteMessage(message);
                            ConsoleManager.ShowWarning($"Event Trigger -> Delete message: {message.Body} from folder: {folder.Name} because to many CC targets!");
                        }
                    }
                });

            this._availableRules.Add(deleteMessageWithToManyCopies);

            FolderRule copySendedMessageToDrafts = new(
                "Copy sended message to drafts",
                "Make a copy of sended messages to drafts folder",
                (mailAccount, folder, message, messageAction) =>
                {
                    if (messageAction == MessageAction.MessageSended)
                    {
                        mailAccount.Folders
                               .Find(f => f.Name == DefaultFolder.DraftsFolderName)
                               .AddMessage(mailAccount, (Message)message.Clone(), MessageAction.MessageCopied);

                        ConsoleManager.ShowWarning($"Event Trigger -> Copy sended message: {message.Body} from folder: {folder.Name} to Folder Drafts!");
                    }
                });

            this._availableRules.Add(copySendedMessageToDrafts);
        }

        public override bool Rename(string newName)
        {
            try
            {
                if (this._isProtected)
                    throw new Exception("Can't remane, this folder is protected");

                this._name = newName;

                return true;
            }
            catch (Exception ex)
            {
                ConsoleManager.ShowError(ex.Message);

                return false;
            }
        }

        public override void AddMessage(MailAccount mailAccount, Message message, MessageAction messageAction)
        {
            this._messages.Add(message);

            ExecuteActiveRules(mailAccount, this, message, messageAction);
        }

        public List<FolderRule> GetActiveRules()
        {
            return this._activeRules;
        }

        public List<FolderRule> GetAvailableRules()
        {
            return this._availableRules.Except(this._activeRules).ToList();
        }

        public void AddActiveRule(FolderRule folderRule) 
        {
            this._activeRules.Add(folderRule);

            this.ExecuteRuleEvent += folderRule.HandleEvent;
        }

        public void RemoveActiveRule(FolderRule folderRule)
        {
            this._activeRules.Remove(folderRule);

            this.ExecuteRuleEvent -= folderRule.HandleEvent;
        }

        public void ExecuteActiveRules(MailAccount mailAccount, DefaultFolder folder, Message message, MessageAction messageAction)
        {
            ExecuteRuleEvent?.Invoke(mailAccount, folder, message, messageAction);
        }
    }
}
