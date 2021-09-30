using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserApiMicroservice.Dtos
{
    public class UserSimpleDto
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string FullName { get; set; }
        public int TotalAttendance { get; set; }
    }
}
