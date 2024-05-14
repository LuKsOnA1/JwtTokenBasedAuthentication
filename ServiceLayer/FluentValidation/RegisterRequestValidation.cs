﻿using EntityLayer.DTOs;
using FluentValidation;
using ServiceLayer.Messages;

namespace ServiceLayer.FluentValidation
{
	public class RegisterRequestValidation : AbstractValidator<UserDTO>
	{
        public RegisterRequestValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Name"))
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Name"));

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Email"))
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Email"))
                .EmailAddress().WithMessage(ValidationMessages.EmailMessage("Email"));

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Password"))
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Password"))
                .MinimumLength(7).WithMessage(ValidationMessages.GreaterThanMessage("Password", 7));

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage(ValidationMessages.NullEmptyMessage("Confirm Password"))
                .NotNull().WithMessage(ValidationMessages.NullEmptyMessage("Confirm Password"))
                .Equal(x => x.Password).WithMessage(ValidationMessages.ComparePasswordMessage("Confirm Password", "Password"));

		}
    }
}