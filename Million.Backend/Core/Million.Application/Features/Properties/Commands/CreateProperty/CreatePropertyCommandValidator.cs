using FluentValidation;
using MediatR;
using Million.Domain.Features.Shared.Entities;

namespace Million.Application.Features.Properties.Commands.CreateProperty
{
    public class CreatePropertyCommandValidator : AbstractValidator<CreatePropertyCommand>
    {
        public CreatePropertyCommandValidator()
        {
            RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Name is required.")
           .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(200).WithMessage("Address must not exceed 200 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(x => x.CodeInternal)
                .NotEmpty().WithMessage("CodeInternal is required.")
                .MaximumLength(50).WithMessage("CodeInternal must not exceed 50 characters.");

            RuleFor(x => x.Year)
                .InclusiveBetween(1900, DateTime.Now.Year).WithMessage($"Year must be between 1900 and {DateTime.Now.Year}.");

            RuleFor(x => x.IdOwner)
                .NotEmpty().WithMessage("IdOwner is required.")
                .Matches(@"^\d+$").WithMessage("IdOwner must be numeric.");

        }
    }
}
