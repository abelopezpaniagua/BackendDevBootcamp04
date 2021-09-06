using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositePatternExample
{
    public interface IGraphic
    {
        public void Move(int x, int y);
        public void Draw();
    }
}
