using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethodExercise
{
    public class OrderXMLParser : XMLParser
    {
        public string Parse()
        {
            return string.Format("try to parsing to XML with: {0}", this.GetType().Name);
        }
    }
}
