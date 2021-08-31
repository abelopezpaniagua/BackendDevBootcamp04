using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethodExercise
{
    public abstract class DisplayService
    {
        protected XMLParser _parser;

        public DisplayService() {
            this._parser = this.GetParser();
        }

        public void Display() {
            Console.WriteLine("Initializing the parsing to XML: \n{0}", this._parser.Parse());
        }

        public abstract XMLParser GetParser();
    }
}
