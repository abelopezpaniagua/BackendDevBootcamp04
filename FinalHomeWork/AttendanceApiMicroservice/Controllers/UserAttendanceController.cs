using AttendanceApiMicroservice.Dtos;
using AttendanceApiMicroservice.Services;
using AutoMapper;
using Domain;
using Domain.Models;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceApiMicroservice.Controllers
{
    [Route("api/attendance")]
    [ApiController]
    public class UserAttendanceController : ControllerBase
    {
        private readonly UserAttendanceService _userAttendanceService;
        private readonly IMapper _mapper;
        private readonly IBus _bus;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserAttendanceController(
            UserAttendanceService userAttendanceService, 
            IMapper mapper, 
            IBus bus, 
            IConfiguration configuration,
            IHttpContextAccessor contextAccessor)
        {
            this._userAttendanceService = userAttendanceService;
            this._mapper = mapper;
            this._bus = bus;
            this._configuration = configuration;
            this._httpContextAccessor = contextAccessor;
        }

        [HttpGet]
        public ActionResult<List<UserAttendance>> Get()
        {
            var userAttendancesList = this._userAttendanceService.Get();
            return Ok(userAttendancesList);
        }

        [HttpGet("{id:length(24)}")]
        [ActionName(nameof(GetById))]
        public ActionResult<UserAttendance> GetById(string id)
        {
            var userAttendance = this._userAttendanceService.Get(id);

            if (userAttendance == null)
            {
                return NotFound();
            }

            return Ok(userAttendance);
        }

        [HttpPost]
        public async Task<ActionResult<UserAttendance>> Create(UserAttendanceCreateDto createUserAttendance)
        {
            UserAttendance userAttendance = this._mapper.Map<UserAttendance>(createUserAttendance);
            var newUserAttendance = this._userAttendanceService.Create(userAttendance);

            Uri userAttendanceUri = new Uri($"{_configuration.GetConnectionString("rabbitmq")}/{RabbitMqConsts.CreateAttendanceServiceQueue}");
            var userAttendanceEndpoint = await this._bus.GetSendEndpoint(userAttendanceUri);
            
            await userAttendanceEndpoint.Send(newUserAttendance);

            return CreatedAtAction(nameof(GetById), new { id = newUserAttendance.Id.ToString() }, newUserAttendance);
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult> Delete(string id)
        {
            var userAttendance = this._userAttendanceService.Get(id);

            if (userAttendance == null)
            {
                return NotFound();
            }

            this._userAttendanceService.Remove(id);

            Uri userAttendanceUri = new Uri($"{_configuration.GetConnectionString("rabbitmq")}/{RabbitMqConsts.DeleteAttendanceServiceQueue}");
            var userAttendanceEndpoint = await this._bus.GetSendEndpoint(userAttendanceUri);

            await userAttendanceEndpoint.Send(userAttendance);

            return NoContent();
        }

        [HttpGet("users/{userId:int}/attendances")]
        public ActionResult<List<UserAttendance>> GetUserAttendances(int userId)
        {
            var userAttendances = this._userAttendanceService.GetByUserId(userId);
            return Ok(userAttendances);
        }

        [HttpDelete("users/{userId:int}/attendances")]
        public ActionResult<List<UserAttendance>> DeleteUserAttendances(int userId)
        {
            this._userAttendanceService.RemoveByUserId(userId);
            return NoContent();
        }
    }
}
