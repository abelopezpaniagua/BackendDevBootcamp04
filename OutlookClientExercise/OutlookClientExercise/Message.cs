using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutlookClientExercise
{
    public class Message
    {
        private string _originIP;
        private string _from;
        private List<string> _to;
        private string _subject;
        private List<string> _carbonCopy;
        private string _body;
        private DateTime _date;
        private bool _isFlagged;

        public string OriginIP => _originIP;
        public string From => _from;
        public List<string> To => _to;
        public string Subject => _subject;
        public List<string> CarbonCopy => _carbonCopy;
        public string Body => _body;
        public DateTime Date => _date;
        public bool IsFlagged => _isFlagged;

        public Message(string originIP, string from, List<string> to, string body, DateTime date, string subject = null, List<string> carbonCopy = null, bool isFlagged = false)
        {
            this._originIP = originIP;
            this._from = from;
            this._to = to;
            this._body = body;
            this._date = date;
            this._subject = subject;
            this._carbonCopy = carbonCopy != null ? carbonCopy : new List<string>();
            this._isFlagged = isFlagged;
        }

        public void ToggleFlagged()
        {
            this._isFlagged = !IsFlagged;
        }
    }
}
