﻿using FluentValidation;
using Promomash.Trader.UserAccess.Domain.Users;

namespace Promomash.Trader.UserAccess.Application.Users.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MustAsync(async (email, _) => await userRepository.IsEmailUniqueAsync(email))
            .WithMessage("The user with same email already exists.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(2)
            .Matches(@"[A-Za-z]")
            .Matches(@"\d")
            .WithMessage("Password must contain at least 1 letter and 1 number");

        RuleFor(x => x.IsAgreedToWorkForFood).NotNull();

        RuleFor(x => x.ProvinceId)
            .NotEmpty()
            .Matches(@"^[A-Z]{2,3}:[A-Z]{2,3}$")
            .WithMessage("ProvinceId must be in the format 'CountryCode:ProvinceCode'.");
    }
}