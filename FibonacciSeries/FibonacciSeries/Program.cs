using System;

namespace FibonacciSeries
{
    class Program
    {
        static void Main(string[] args)
        {
            string result = Fibonacci(6);
            Console.WriteLine(result);
            Console.ReadKey(true);
        }

        static string Fibonacci(int n)
        {
            int n1 = 0;
            int n2 = 1;
            int sum = 0;

            string fibonacciSeries = "";

            for (int i = 0; i < n; i++)
            {
                fibonacciSeries += $" {n1}";

                sum = n1 + n2;
                n1 = n2;
                n2 = sum;
            }

            return fibonacciSeries;
        }
    }
}
//0 1 1 2 3 5
