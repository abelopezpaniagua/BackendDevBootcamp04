using System;

namespace IteratorExample
{
    class ShapeStorage
    {
        private Shape[] shapes;
        private int position = 0;

        public ShapeStorage(int capacity = 5)
        {
            if (capacity > 0)
            {
                this.shapes = new Shape[capacity];
            }
            else
            {
                this.shapes = new Shape[5];
            }
        }

        public void AddShape(string shape)
        {
            if (position < shapes.Length)
            {
                shapes[position] = new Shape(position, shape);
                position++;
            }
            else
            {
                Console.WriteLine("Can't add more shapes!");
            }
        }

        public Shape[] GetShapes()
        {
            return shapes;
        }
    }
}
