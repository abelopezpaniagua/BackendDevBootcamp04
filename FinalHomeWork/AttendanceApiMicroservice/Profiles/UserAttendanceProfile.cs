using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceApiMicroservice.Dtos;
using AutoMapper;
using Domain.Models;

namespace AttendanceApiMicroservice.Profiles
{
    public class UserAttendanceProfile : Profile
    {
        public UserAttendanceProfile()
        {
            CreateMap<UserAttendanceCreateDto, UserAttendance>();
        }
    }
}
