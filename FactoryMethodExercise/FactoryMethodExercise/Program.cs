using System;

namespace FactoryMethodExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("XML Parser!!");

            DisplayService displayService = null;

            displayService = new ErrorXMLDisplayService();
            displayService.Display();

            displayService = new FeedbackXMLDisplayService();
            displayService.Display();

            displayService = new OrderXMLDisplayService();
            displayService.Display();

            displayService = new ResponseXMLDisplayService();
            displayService.Display();
        }
    }
}
