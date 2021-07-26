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
                    "\n5.Rename Folder \n6.Remove Folder \n7.Close Application \nPlease select an option";

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

                        break;

                    case ClientActionChoice.AddFolder:

                        break;

                    case ClientActionChoice.RenameFolder:

                        break;

                    case ClientActionChoice.RemoveFolder:

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

        public enum ClientActionChoice
        {
            ConfigureSMTP = 1,
            SendMessage = 2,
            ListFolders = 3,
            AddFolder = 4,
            RenameFolder = 5,
            RemoveFolder = 6,
            CloseApplication = 7
        }

        public enum FoldersActionChoice
        {
            ListMessages = 1,
            DeleteMessage = 2,
            MoveMessage = 3,
        }
    }
}
