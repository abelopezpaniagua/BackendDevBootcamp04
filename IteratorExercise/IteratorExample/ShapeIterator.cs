namespace IteratorExample
{
    public class ShapeIterator : Iterator<Shape>
    {
        private Shape[] shapes;
        public int position = 0;

        public ShapeIterator(Shape[] shapes)
        {
            this.shapes = shapes;
        }

        public bool HasNext()
        {
            return position < this.shapes.Length;
        }

        public Shape Next()
        {
            Shape shape = this.shapes[position];
            this.position++;
            return shape;
        }

        public void Remove()
        {
            shapes[position] = null;
        }

    }
}
