using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IteratorExample
{
    public class Shape
    {
        public int Id { set; get; }
        public string Name { set; get; }

        public Shape(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public override string ToString()
        {
            return $"Shape: ID {Id} - Name: {Name}";
        }
    }
}
