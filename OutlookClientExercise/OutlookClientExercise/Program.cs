using OutlookClientExercise.UserInterface;
using System;
using System.Collections.Generic;

namespace OutlookClientExercise
{
    class Program
    {
        #region SMTP Server Initialization
        public static SMPTServer mailServer = new SMPTServer("Default SMTP Server", "DefaultServer", new()
        {
            new("juan.gonzales@mail.com", "juan123"),
            new("pedro.almaraz@mail.com", "pedro123"),
            new("abel.lopez@mail.com", "abel123"),
            new("maria.cartagena@mail.com", "maria123"),
            new("gonzalo.lema@mail.com", "gonzalo123")
        });
        #endregion

        #region Main Program

        static void Main(string[] args)
        {
            SMPTClient client = new(mailServer);

            SystemActionChoice systemActionChoice;

            do
            {
                string menuInfo = "Menu \n1.Login \n2.Register New Account \n3.Close Application \nPlease select an option";

                systemActionChoice = ConsoleManager.GetUserInputWithPreInformation<SystemActionChoice>(menuInfo);

                switch (systemActionChoice)
                {
                    case SystemActionChoice.LoginClient:

                        string username = ConsoleManager.GetUserInputWithPreInformation<string>("Username");
                        string password = ConsoleManager.GetUserInputWithPreInformation<string>("Password");

                        if (!client.Connect(username, password))
                        {
                            ConsoleManager.ShowError("Incorrect credentials");
                            break;
                        }

                        ShowClientMenu(client);

                        break;

                    case SystemActionChoice.RegisterAccount:

                        ConsoleManager.ShowInfo("Register new Account");
                        string newUsername = ConsoleManager.GetUserInputWithPreInformation<string>("Insert username");
                        string newPassword = ConsoleManager.GetUserInputWithPreInformation<string>("Insert password");

                        if (!mailServer.RegisterAccount(newUsername, newPassword))
                        {
                            ConsoleManager.ShowError("Account registration failed!");
                            break;
                        }

                        ConsoleManager.ShowSuccess($"Account {newUsername} has been created successfully!");

                        if (client.Connect(newUsername, newPassword))
                        {
                            ConsoleManager.ShowError("Incorrect credentials");
                            break;
                        }

                        ShowClientMenu(client);

                        break;

                    case SystemActionChoice.CloseApplication:
                        ConsoleManager.ShowInfo("Closing application...");
                        break;
                    default:
                        break;
                }

            } while (systemActionChoice != SystemActionChoice.CloseApplication);
        }

        #endregion

        #region Client Submenu

        public static void ShowClientMenu(SMPTClient client)
        {
            ClientActionChoice clientActionChoice;

            do
            {
                string menuInfo = "Menu \n1.Configure SMTP Server \n2.Send Message \n3.List Folders \n4.Add Folder" +
                    "\n5.Logout \nPlease select an option";

                clientActionChoice = ConsoleManager
                    .GetUserInputWithPreInformation<ClientActionChoice>(menuInfo);

                switch (clientActionChoice)
                {
                    case ClientActionChoice.ConfigureSMTP:

                        ConsoleManager.ShowInfo("Actual SMTP Config: ");
                        ConsoleManager.ShowInfo(client.Server.GetInfo());

                        string serverDescription = ConsoleManager.GetUserInputWithPreInformation<string>("Insert Server Description");
                        string serverName = ConsoleManager.GetUserInputWithPreInformation<string>("Insert Server Name");
                        string serverPort = ConsoleManager.GetUserInputWithPreInformation<string>("Insert Port (Default: 537)");

                        bool serverUpdated = client.Server.Update(
                            serverDescription, serverName, !string.IsNullOrEmpty(serverPort) ? int.Parse(serverPort) : null);

                        if (serverUpdated)
                            ConsoleManager.ShowSuccess("SMTP Server Config successfully updated!");
                        else
                            ConsoleManager.ShowError("Error updating SMTP Server Config");

                        break;

                    case ClientActionChoice.SendMessage:

                        ConsoleManager.ShowInfo("The (*) fields are required");

                        List<string> toEmails = GetTargetEmails("To", true);

                        string body = ConsoleManager.GetUserInputWithPreInformation<string>("Body (*)");
                        string subject = ConsoleManager.GetUserInputWithPreInformation<string>("Subject");

                        List<string> carbonCopyEmails = GetTargetEmails("CC", false);

                        if (client.SendMessage(toEmails, body, subject, carbonCopyEmails))
                        {
                            ConsoleManager.ShowSuccess("Message Sended Successfully!");
                        }
                        else
                        {
                            ConsoleManager.ShowError("An error ocurred while sending the message :(");
                        }

                        break;

                    case ClientActionChoice.ListFolders:

                        var folders = client.GetFolders();

                        ConsoleManager.ShowInfo("Folders List: ");
                        for (int i = 0; i < folders.Count; i++)
                        {
                            ConsoleManager.Show($"{i + 1}. {folders[i].Name}");
                        }

                        ConsoleManager.ShowInfo($"Select one folder (1 - {folders.Count}) or -1 for Cancel");
                        var selectFolderOption = ConsoleManager.GetUserInput<int>();
                        if (selectFolderOption == -1)
                        {
                            Console.Clear();
                            break;
                        }

                        var selectedFolder = folders[selectFolderOption - 1];

                        ShowFolderSubmenu(client, selectedFolder);

                        break;

                    case ClientActionChoice.AddFolder:

                        string folderName = ConsoleManager.GetUserInputWithPreInformation<string>("Please insert folder name");

                        if (client.AddFolder(folderName))
                            ConsoleManager.ShowSuccess("Folder created successfully!");
                        else
                            ConsoleManager.ShowError("Folder already exists!");

                        break;

                    case ClientActionChoice.LogoutClient:
                        client.Dispose();

                        break;
                }

                if (clientActionChoice != ClientActionChoice.LogoutClient)
                {
                    Console.ReadKey();
                    Console.Clear();
                }

            } while (clientActionChoice != ClientActionChoice.LogoutClient);
        }

        #endregion

        #region Folder Submenu
        public static void ShowFolderSubmenu(SMPTClient client, Folder folder)
        {
            FolderActionChoice folderActionChoice;

            do
            {
                string menuInfo = $"Menu - Folder: {folder.Name} \n1.Rename Folder \n2.Remove Folder \n3.List Messages \n4.Manage Active Rules" +
                    "\n5.Add Rule \n6.Back \nPlease select an option";

                folderActionChoice = ConsoleManager.GetUserInputWithPreInformation<FolderActionChoice>(menuInfo);

                switch (folderActionChoice)
                {
                    case FolderActionChoice.RenameFolder:

                        string newFolderName = ConsoleManager.GetUserInputWithPreInformation<string>("Insert new folder name");
                        if (folder.Rename(newFolderName))
                        {
                            ConsoleManager.ShowSuccess($"Folder renamed successful to: {folder.Name}!");
                        }
                        else
                        {
                            ConsoleManager.ShowError("Rename folder failed!");
                        }

                        break;
                    case FolderActionChoice.RemoveFolder:

                        if (client.RemoveFolder(folder))
                        {
                            ConsoleManager.ShowSuccess($"Folder removed successfully!");
                            folderActionChoice = FolderActionChoice.Back;
                        }
                        else
                        {
                            ConsoleManager.ShowError("Remove folder failed!");
                        }

                        break;
                    case FolderActionChoice.ListMessages:
                        var messages = folder.GetMessages();

                        ConsoleManager.ShowInfo("Messages: ");
                        for (int i = 0; i < messages.Count; i++)
                        {
                            string flagged = messages[i].IsFlagged ? "F" : "";
                            ConsoleManager.Show($"[{flagged}] {i + 1}. ({messages[i].Date.ToLongDateString()}) {messages[i].From} - {messages[i].Subject ?? "No Subject"}: {messages[i].Body}");
                        }

                        ConsoleManager.ShowInfo($"Select one message (1 - {messages.Count}) or -1 for Cancel");
                        var selectMessageOption = ConsoleManager.GetUserInput<int>();
                        if (selectMessageOption == -1)
                        {
                            Console.Clear();
                            break;
                        }

                        var selectedMessage = messages[selectMessageOption - 1];

                        ShowMessageSubmenu(client, folder, selectedMessage);

                        break;
                    case FolderActionChoice.ManageRules:

                        var currentRules = folder.GetActiveRules();

                        ConsoleManager.ShowInfo("Active Rules: ");
                        for (int i = 0; i < currentRules.Count; i++)
                        {
                            ConsoleManager.Show($"{i + 1}: {currentRules[i].Name} -> {currentRules[i].Description}");
                        }

                        ConsoleManager.ShowInfo($"Select a Rule (1 - {currentRules.Count}) or -1 for Cancel");
                        var selectRuleOption = ConsoleManager.GetUserInput<int>();
                        if (selectRuleOption == -1)
                        {
                            Console.Clear();
                            break;
                        }

                        var selectedRule = currentRules[selectRuleOption - 1];

                        ShowRuleSubmenu(folder, selectedRule);

                        break;

                    case FolderActionChoice.AddRule:

                        var availableRules = folder.GetAvailableRules();

                        ConsoleManager.ShowInfo("Available Rules: ");
                        for (int i = 0; i < availableRules.Count; i++)
                        {
                            ConsoleManager.Show($"{i + 1}: {availableRules[i].Name} -> {availableRules[i].Description}");
                        }

                        ConsoleManager.ShowInfo($"Select a Rule (1 - {availableRules.Count}) or -1 for Cancel");
                        var selectAvailableRuleOption = ConsoleManager.GetUserInput<int>();
                        if (selectAvailableRuleOption == -1)
                        {
                            Console.Clear();
                            break;
                        }

                        var selectedAvailableRule = availableRules[selectAvailableRuleOption - 1];

                        folder.AddActiveRule(selectedAvailableRule);

                        ConsoleManager.ShowSuccess("Rule added successfully!");

                        break;

                    case FolderActionChoice.Back:

                        ConsoleManager.ShowInfo("Closing folder...");

                        break;
                    default:
                        break;
                }
            }
            while (folderActionChoice != FolderActionChoice.Back);
        }

        #endregion

        #region Message Submenu
        public static void ShowMessageSubmenu(SMPTClient client, Folder folder, Message message)
        {
            MessageActionChoice messageActionChoice;

            do
            {
                string messageTitle = $"Message: {message.Date.ToLongDateString()} - {message.From}";
                string menuInfo = $"Menu - {messageTitle} \n1.Read Message \n2.Toggle Flagged \n3.Delete Message \n4.Move Message" +
                    "\n5.Back \nPlease select an option";

                messageActionChoice = ConsoleManager.GetUserInputWithPreInformation<MessageActionChoice>(menuInfo);

                switch (messageActionChoice)
                {
                    case MessageActionChoice.ReadMessage:

                        ConsoleManager.ShowSuccess("================= MESSAGE =================");

                        string flagged = message.IsFlagged ? "F" : "";
                        string subject = !string.IsNullOrEmpty(message.Subject) ? message.Subject : "No Subject";

                        ConsoleManager.ShowInfo($"Date: {message.Date.ToShortDateString()} ----------- Flagged: [{flagged}]");
                        ConsoleManager.ShowInfo($"From: {message.From}");
                        ConsoleManager.ShowInfo($"Subject: { subject }");
                        ConsoleManager.ShowInfo($"To: { string.Join(',', message.To) }");
                        ConsoleManager.ShowInfo($"CC: { string.Join(',', message.CarbonCopy) }");
                        ConsoleManager.ShowInfo("==================================================");
                        ConsoleManager.Show(message.Body);
                        ConsoleManager.ShowInfo("==================================================");

                        Console.ReadKey();

                        break;

                    case MessageActionChoice.MarkFlagMessage:

                        message.ToggleFlagged();
                        ConsoleManager.ShowSuccess("Message flagged changed successfully!");

                        break;
                    case MessageActionChoice.DeleteMessage:

                        if (folder.DeleteMessage(message))
                        {
                            ConsoleManager.ShowSuccess("Message Deleted Successfully!");
                        }
                        else
                        {
                            ConsoleManager.ShowError("Delete Message Failed!");
                        }

                        break;
                    case MessageActionChoice.MoveMessage:

                        var foldersSelection = client.GetFolders()
                            .FindAll(f => f != folder);

                        ConsoleManager.ShowInfo("Folders List: ");
                        for (int i = 0; i < foldersSelection.Count; i++)
                        {
                            ConsoleManager.Show($"{i + 1}. {foldersSelection[i].Name}");
                        }

                        ConsoleManager.ShowInfo($"Select one folder (1 - {foldersSelection.Count}) or -1 for Cancel");
                        var selectFolderOption = ConsoleManager.GetUserInput<int>();
                        if (selectFolderOption == -1)
                        {
                            Console.Clear();
                            break;
                        }

                        var selectedFolder = foldersSelection[selectFolderOption - 1];

                        if (folder.MoveMessage(client.CurrentConnectedAccount, message, selectedFolder))
                        {
                            ConsoleManager.ShowSuccess($"Message moved to: {selectedFolder.Name} successfully!");
                        }
                        else
                        {
                            ConsoleManager.ShowError("Message moved failed!");
                        }

                        break;
                    case MessageActionChoice.Back:

                        ConsoleManager.ShowInfo("Closing message...");

                        break;
                    default:
                        break;
                }

            } while (messageActionChoice != MessageActionChoice.Back);
        }

        #endregion

        #region Rule Submenu
        public static void ShowRuleSubmenu(Folder folder, FolderRule folderRule)
        {
            RuleActionChoice ruleActionChoice;

            do
            {
                string menuInfo = $"Menu - Rule: {folderRule.Name} \n1.Remove Rule \n2.Back \nPlease select an option";

                ruleActionChoice = ConsoleManager.GetUserInputWithPreInformation<RuleActionChoice>(menuInfo);

                switch (ruleActionChoice)
                {
                    case RuleActionChoice.RemoveRule:

                        folder.RemoveActiveRule(folderRule);

                        ConsoleManager.ShowSuccess("Rule removed successfully!");

                        break;
                    case RuleActionChoice.Back:
                        
                        ConsoleManager.ShowInfo("Returning to folder...");

                        break;
                    default:
                        break;
                }

            } while (ruleActionChoice != RuleActionChoice.Back);
        }

        #endregion

        public static List<string> GetTargetEmails(string fieldName, bool isStrict = false)
        {
            List<string> targetEmails = new List<string>();
            int targetEmailsCount = 0;
            bool isInsertingTargetEmails = true;

            do
            {
                string required = isStrict ? "(*)" : "";
                string toEmail = ConsoleManager.GetUserInputWithPreInformation<string>($"{fieldName} { required }");

                if (isStrict && (string.IsNullOrEmpty(toEmail) || string.IsNullOrWhiteSpace(toEmail) || !toEmail.Contains("@mail.com")))
                {
                    ConsoleManager.ShowError("Invalid Email!, insert again.");
                    continue;
                }

                if (isStrict || !string.IsNullOrWhiteSpace(toEmail) || string.IsNullOrEmpty(toEmail))
                {
                    targetEmails.Add(toEmail);

                    string insertMoreTargetEmails = ConsoleManager.GetUserInputWithPreInformation<string>("You need to add more target emails?: (S/N)");
                    isInsertingTargetEmails = insertMoreTargetEmails == "S";
                } 
                else
                {
                    isInsertingTargetEmails = false;
                }

            } while ((targetEmailsCount == 0 || !isStrict) && isInsertingTargetEmails);

            return targetEmails;
        }

        #region Application Enums

        public enum SystemActionChoice
        {
            LoginClient = 1,
            RegisterAccount = 2,
            CloseApplication = 3
        }

        public enum ClientActionChoice
        {
            ConfigureSMTP = 1,
            SendMessage = 2,
            ListFolders = 3,
            AddFolder = 4,
            LogoutClient = 5
        }

        public enum FolderActionChoice
        {
            RenameFolder = 1,
            RemoveFolder = 2,
            ListMessages = 3,
            ManageRules = 4,
            AddRule = 5,
            Back = 6
        }

        public enum MessageActionChoice
        {
            ReadMessage = 1,
            MarkFlagMessage = 2,
            DeleteMessage = 3,
            MoveMessage = 4,
            Back = 5
        }

        public enum RuleActionChoice
        {
            RemoveRule = 1,
            Back = 2
        }
        #endregion
    }
}
