using System.Collections.Generic;

namespace Domain.Models
{
    public class User : BaseEntity
    {
        public string Nickname { get; set; }
        public string FullName { get; set; }
        public string Credentials { get; set; }
        public int TotalAttendance { get; set; }
        public List<UserAttendance> Attendances { get; set; }
    }
}
