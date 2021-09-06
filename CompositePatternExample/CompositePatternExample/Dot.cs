using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositePatternExample
{
    public class Dot : IGraphic
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Dot(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public void Move(int x, int y)
        {
            this.X += x;
            this.Y += y;
        }

        public virtual void Draw()
        {
            Console.WriteLine($"Drawing Dot in coordinates ({this.X}, {this.Y})");
        }
    }
}
