using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositePatternExample
{
    public class CompoundGraphic : IGraphic
    {
        public List<IGraphic> Children { get; set; }

        public CompoundGraphic()
        {
            this.Children = new List<IGraphic>();
        }

        public void Add(IGraphic child)
        {
            this.Children.Add(child);
        }

        public void Remove(IGraphic child)
        {
            this.Children.Remove(child);
        }

        public void Move(int x, int y)
        {
            foreach (IGraphic child in this.Children)
            {
                child.Move(x, y);
            }
        }

        public void Draw()
        {
            foreach (IGraphic child in this.Children)
            {
                child.Draw();
            }
        }
    }
}
