using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositePatternExample
{
    public class Circle : Dot
    {
        public double Radius { get; set; }

        public Circle(int x, int y, double radius) : base(x, y)
        {
            this.Radius = radius;
        }

        public override void Draw()
        {
            Console.WriteLine($"Drawing circle with coordinates({this.X}, {this.Y}) and with radius {this.Radius}");
        }
    }
}
