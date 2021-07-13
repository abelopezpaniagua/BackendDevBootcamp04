using System;

namespace ExtensionMethodsExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomLogger logger = new();

            logger.ShowLog("Default log message", "Default");

            logger.ErrorLog("Something wrong happened!");
            logger.WarningLog("Notice message");
            logger.SuccessLog("Success message!");
        }
    }

    public class CustomLogger
    {
        public void ShowLog(string message, string typeMessage)
        {
            Console.WriteLine($"{typeMessage}: {message}");
        }
    }


    public static class ExtensionLoggerMethods
    {
        public static void ErrorLog(this CustomLogger logger, string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            logger.ShowLog(message, "Error");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void WarningLog(this CustomLogger logger, string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            logger.ShowLog(message, "Warning");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void SuccessLog(this CustomLogger logger, string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            logger.ShowLog(message, "Success");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
