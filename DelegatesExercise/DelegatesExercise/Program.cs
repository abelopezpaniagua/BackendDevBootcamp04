using System;

namespace DelegatesExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            Expression expression = new(20, 10);
            var result = expression.ApplyOperator(Operation.Square);
            Console.WriteLine($"Result: {result}");
        }
    }
}
