using Amrod.Application.Features.Customers.Dtos;
using FluentValidation;

namespace Amrod.WebApi.Validators;

public sealed class CustomerValidator: AbstractValidator<CustomerDto>
{
    public CustomerValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Email).NotEmpty().MaximumLength(100);
        RuleFor(x => x.CountryCode).NotEmpty().MaximumLength(2);
    }
    
}