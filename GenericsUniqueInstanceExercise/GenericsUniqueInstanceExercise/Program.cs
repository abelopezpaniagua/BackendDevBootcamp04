using System;

namespace GenericsUniqueInstanceExercise
{
    class Program
    {
        class UniqueInstance<T> where T : new()
        {
            public static T Unique { get; set; } = new T();

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
