using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutlookClientExercise.BL
{
    public interface IMessageProcesser
    {
        public bool ProcessMessage(Message message);
    }
}
