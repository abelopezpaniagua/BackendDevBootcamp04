using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethodExercise
{
    public class FeedbackXMLDisplayService : DisplayService
    {
        public override XMLParser GetParser()
        {
            return new FeedbackXMLParser();
        }
    }
}
