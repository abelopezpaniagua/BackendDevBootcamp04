using Domain.Models;
using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserApiMicroservice.Services;

namespace UserApiMicroservice.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        private readonly IUserService _userService;

        public UserValidator(IUserService userService)
        {
            this._userService = userService;

            RuleFor(x => x.Nickname)
                .MinimumLength(5)
                    .WithMessage("The nickname must contain at least 5 characters.")
                .MaximumLength(50)
                    .WithMessage("The nickname must contain a maximium of 50 characters")
                .NotEmpty()
                .MustAsync(UniqueNickName)
                    .WithMessage("The nickname must be unique");
            RuleFor(x => x.FullName).Length(5, 150);
            RuleFor(x => x.Credentials).Length(3, 100);
        }

        private async Task<bool> UniqueNickName(User user, string nickname, CancellationToken cancellationToken)
        {
            var existingUser = await this._userService.GetUserByNickname(nickname);

            return existingUser == null || existingUser.Id == user.Id;
        }
    }
}
