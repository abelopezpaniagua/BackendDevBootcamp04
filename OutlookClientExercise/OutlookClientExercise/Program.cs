using System;

namespace OutlookClientExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            SMPTClient client = new();

            ClientActionChoice clientActionChoice;

            do
            {
                string menuInfo = "Menu \n1.Configure SMTP Server \n2.Send Message \n3.List Folders \n4.Add Folder" + 
                    "\n5.Close Application \nPlease select an option";

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
                        string serverUsername = ConsoleManager.GetUserInputWithPreInformation<string>("Insert Username");
                        string serverPassword = ConsoleManager.GetUserInputWithPreInformation<string>("Insert Password");

                        bool serverUpdated = client.Server.Update(
                            serverDescription, serverName, serverUsername, serverPassword, !string.IsNullOrEmpty(serverPort) ? int.Parse(serverPort) : null);

                        if (serverUpdated)
                            ConsoleManager.ShowSuccess("SMTP Server Config successfully updated!");
                        else
                            ConsoleManager.ShowError("Error updating SMTP Server Config");

                        break;

                    case ClientActionChoice.SendMessage:

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

                    case ClientActionChoice.CloseApplication:
                        client.Dispose();

                        break;
                }

                if (clientActionChoice != ClientActionChoice.CloseApplication)
                {
                    Console.ReadKey();
                    Console.Clear();
                }

            } while (clientActionChoice != ClientActionChoice.CloseApplication);
        }

        public static void ShowFolderSubmenu(SMPTClient client, Folder folder)
        {
            FolderActionChoice folderActionChoice;

            do
            {
                string menuInfo = "Menu \n1.Rename Folder \n2.Remove Folder \n3.List Messages \n4.Manage Rules" +
                    "\n5.Back \nPlease select an option";

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
                        break;
                    case FolderActionChoice.ListMessages:
                        break;
                    case FolderActionChoice.ManageRules:
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

        public enum ClientActionChoice
        {
            ConfigureSMTP = 1,
            SendMessage = 2,
            ListFolders = 3,
            AddFolder = 4,
            CloseApplication = 5
        }

        public enum FolderActionChoice
        {
            RenameFolder = 1,
            RemoveFolder = 2,
            ListMessages = 3,
            ManageRules = 4, //PENDING
            Back = 5
        }

        public enum MessageActionChoice
        {
            DeleteMessage = 1,
            MoveMessage = 2,
            Back = 3
        }

        //TODO: Manage RULES ENUM
    }
}
