using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutlookClientExercise
{
    public class Message
    {
        public string OriginIP { get; set; }
        public string From { get; set; }
        public List<string> To { get; set; }
        public string Subject { get; set; }
        public List<string>  CarbonCopy { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public bool IsFlagged { get; set; }

        public Message(string originIP, string from, List<string> to, string body, DateTime date, string subject = null, List<string> carbonCopy = null)
        {
            OriginIP = originIP;
            From = from;
            To = to;
            Body = body;
            Date = date;
            Subject = subject;
            CarbonCopy = carbonCopy != null ? carbonCopy : new List<string>();
            IsFlagged = false;
        }
    }
}
