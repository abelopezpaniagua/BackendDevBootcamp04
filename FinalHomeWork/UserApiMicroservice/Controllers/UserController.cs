using AutoMapper;
using Domain.Enums;
using Domain.Models;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserApiMicroservice.Dtos;
using UserApiMicroservice.Services;
using UserApiMicroservice.Validators;

namespace UserApiMicroservice.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            this._userService = userService;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserSimpleDto>>> GetAll([FromQuery] string searchFilter = null)
        {
            List<User> usersList = new();

            if (searchFilter != null)
            {
                usersList = await this._userService
                    .SearchUsersByNicknameOrFullName(searchFilter);
            }
            else
            {
                usersList = await this._userService.GetAllUsers();
            }

            var newUsersList = this._mapper.Map<List<UserSimpleDto>>(usersList);

            return Ok(newUsersList);
        }

        [HttpGet("{id:int}")]
        [ActionName(nameof(GetById))]
        public async Task<ActionResult<UserResponseDto>> GetById(int id)
        {
            var existingUser = await this._userService.GetUserById(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            var responseUser = this._mapper.Map<UserResponseDto>(existingUser);

            return Ok(responseUser);
        }

        [HttpPost("{id:int}/total-attendances")]
        public async Task<ActionResult> UpdateTotalAttendances(int id, [FromBody] UserAttendanceUpdateForm userAttendanceUpdate)
        {
            var existingUser = await this._userService.GetUserById(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            var attendanceIsUpdated = false;

            if (userAttendanceUpdate.attendanceUpdateType == AttendanceUpdateType.IncrementAttendance)
            {
                attendanceIsUpdated = await this._userService
                    .IncrementUserTotalAttendance(existingUser);
            }
            else if (userAttendanceUpdate.attendanceUpdateType == AttendanceUpdateType.DecrementAttendance)
            {
                attendanceIsUpdated = await this._userService
                    .DecrementUserTotalAttendance(existingUser);
            }

            if (!attendanceIsUpdated)
            {
                return Conflict();
            }

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create(UserCreateDto user)
        {
            var newUser = this._mapper.Map<User>(user);
            var validator = new UserValidator(this._userService);
            var results = await validator.ValidateAsync(newUser);

            if (!results.IsValid)
            {
                results.AddToModelState(ModelState, null);
                return BadRequest(ModelState);
            }

            var createdUser = await this._userService.CreateUserAsync(newUser);

            if (createdUser == null)
            {
                return Conflict();
            }

            var createdUserResponse = this._mapper.Map<UserResponseDto>(createdUser);

            return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUserResponse);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, UserUpdateDto userUpdated)
        {
            var userToUpdate = this._mapper.Map<User>(userUpdated);
            var validator = new UserValidator(this._userService);
            var results = await validator.ValidateAsync(userToUpdate);

            if (!results.IsValid)
            {
                results.AddToModelState(ModelState, null);
                return BadRequest(ModelState);
            }

            var existingUser = await this._userService.GetUserById(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            var userIsUpdated = await this._userService.UpdateUserAsync(userToUpdate);

            if (!userIsUpdated)
            {
                return Conflict();
            }

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existingUser = await this._userService.GetUserById(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            var userIsDeleted = await this._userService.DeleteUserAsync(existingUser);

            if (!userIsDeleted)
            {
                return Conflict();
            }

            return NoContent();
        }

        [HttpPost("auth")]
        public async Task<ActionResult<User>> AuthenticateUser([FromBody] AuthUser authUser)
        {
            var authenticatedUser = await this._userService
                .Authenticate(authUser.Username, authUser.Password);

            if (authenticatedUser == null)
            {
                return NotFound();
            }

            return Ok(authenticatedUser);
        }
    }
}
