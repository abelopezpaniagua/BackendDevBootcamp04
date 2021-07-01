using System;

namespace TransportClassesExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Transport classes");
            Boeing airplane737 = new Boeing(1200, 100, 8000);
            Console.WriteLine($"Airplane Boeing 737");
            airplane737.Fly();

            Toyota jonhCar = new Toyota(220, 4, 4);
            Console.WriteLine($"Jonh's Toyota Car");
            jonhCar.HandleOnLand();

            ElectricFlyingCar teslaSupeCar = new ElectricFlyingCar(750, 2, 4);
            Console.WriteLine($"Elon Musk Super Car");
            teslaSupeCar.HandleOnLand();
            teslaSupeCar.Fly();

            Boat marcoYate = new Boat(180, 15);
            Console.WriteLine($"Marco's Yate");
            marcoYate.Navigate();
        }
    }

    public class Transport
    {
        public int SpeedLimit { get; set; }
        public int PeopleCapacity { get; set; }

        public Transport(int speedLimit, int peopleCapacity)
        {
            SpeedLimit = speedLimit;
            PeopleCapacity = peopleCapacity;
        }
    }

    interface IFlyable
    {
        void Fly();
    }

    public class AirTransport : Transport, IFlyable
    {
        public int MaxHeight { get; set; }

        public AirTransport(int speedLimit, int peopleCapacity, int maxHeight) : base (speedLimit, peopleCapacity) 
        {
            MaxHeight = maxHeight;
        }

        public void Fly()
        {
            Console.WriteLine("Flying...");
        }
    }

    public class LandTransport : Transport
    {
        public int WheelsNumber { get; set; }

        public LandTransport(int speedLimit, int peopleCapacity, int wheelsNumber) : base(speedLimit, peopleCapacity)
        {
            WheelsNumber = wheelsNumber;
        }

        public void HandleOnLand()
        {
            Console.WriteLine("Handling on ground...");
        }
    }

    public class WaterTransport : Transport
    {
        public WaterTransport(int speedLimit, int peopleCapacity) : base(speedLimit, peopleCapacity)
        {
        }

        public void Navigate()
        {
            Console.WriteLine("Navigate on water...");
        }
    }

    public class Toyota : LandTransport
    {
        public Toyota(int speedLimit, int peopleCapacity, int wheelsNumber) : base(speedLimit, peopleCapacity, wheelsNumber)
        {
        }
    }

    public class ElectricFlyingCar : LandTransport, IFlyable
    {
        public ElectricFlyingCar(int speedLimit, int peopleCapacity, int wheelsNumber) : base(speedLimit, peopleCapacity, wheelsNumber)
        {
        }

        public void Fly()
        {
            Console.WriteLine("Flying after cross the lands...");
        }
    }

    public class Boat : WaterTransport
    {
        public Boat(int speedLimit, int peopleCapacity) : base(speedLimit, peopleCapacity)
        {
        }
    }

    public class Boeing : AirTransport
    {
        public Boeing(int speedLimit, int peopleCapacity, int maxHeight) : base(speedLimit, peopleCapacity, maxHeight)
        {
        }
    }
}
