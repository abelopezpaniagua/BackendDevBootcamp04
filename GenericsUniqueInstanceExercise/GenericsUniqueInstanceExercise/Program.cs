using System;

namespace GenericsUniqueInstanceExercise
{
    class Program
    {
        class UniqueInstance<T> where T : new()
        {
            private static T unique;
            public static T Unique => unique == null ? unique = new() : unique;

        }

        class Person : UniqueInstance<Person>
        {
            public string Name;
        }

        static void Main(string[] args)
        {
            Person.Unique.Name = "pepe";
            Console.WriteLine(Person.Unique.Name);
        }
    }
}
