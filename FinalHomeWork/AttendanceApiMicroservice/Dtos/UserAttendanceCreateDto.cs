using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceApiMicroservice.Dtos
{
    public class UserAttendanceCreateDto
    {
        public int UserId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public DateTime Date { get; set; }
        public string Observations { get; set; }
    }
}
