using System;

namespace CompositePatternExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Wellcome to the Image Editor");

            var all = new CompoundGraphic();
            all.Add(new Dot(1, 2));
            all.Add(new Circle(5, 3, 10));
            all.Add(new Dot(2, 5));
            all.Add(new Circle(10, 5, 3));

            var compoundGraphic1 = new CompoundGraphic();
            compoundGraphic1.Add(new Dot(1, 3));
            compoundGraphic1.Add(new Circle(1, 5, 2));

            all.Add(compoundGraphic1);
            all.Add(new Circle(10, 20, 5));

            all.Move(1, 3);
            all.Draw();
        }
    }
}
