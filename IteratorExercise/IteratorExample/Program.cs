using System;

namespace IteratorExample
{
    class Program
    {
        static void Main(string[] args)
        {
            ShapeStorage storage = new ShapeStorage(5);
            storage.AddShape("Polygon");
            storage.AddShape("Hexagon");
            storage.AddShape("Circle");
            storage.AddShape("Rectangle");
            storage.AddShape("Square");

            ShapeIterator iterator = new ShapeIterator(storage.GetShapes());
            do
            {
                Console.WriteLine(iterator.Next());
            }
            while (iterator.HasNext());

            Console.WriteLine("Apply removing while iterating...");
            iterator = new ShapeIterator(storage.GetShapes());
            do
            {
                iterator.Remove();
                var removedShape = iterator.Next();
                Console.WriteLine("Shape removed: {0}", removedShape is null ? "NULL" : removedShape);
            }
            while (iterator.HasNext());
        }
    }
}
