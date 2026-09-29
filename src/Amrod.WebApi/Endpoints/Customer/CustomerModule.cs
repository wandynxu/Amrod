using Amrod.Application.Features.Customers.Commands.Create;
using Amrod.Application.Features.Customers.Dtos;
using Carter;
using Carter.OpenApi;
using FluentValidation;
using MediatR;

namespace Amrod.WebApi.Endpoints.Customer;

public class CustomerModule: ICarterModule
{
    
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var groupEndpoints = app.MapGroup("api/customers").WithTags("Customers");;

        groupEndpoints.MapPost("/create", async (CustomerDto customerDto, IValidator<CustomerDto> validator, IMediator mediator, CancellationToken ct) =>
        {
            var validationResult = await validator.ValidateAsync(customerDto,ct);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .ToDictionary(e => e.PropertyName, e => e.ErrorMessage);
                return Results.BadRequest(errors);
            }
            
            await mediator.Send(new CreateCustomerCommand(customerDto.Name, customerDto.Email, customerDto.CountryCode),ct);
            
            return Results.Ok();
        }).WithName("CreateCustomer").IncludeInOpenApi();
        
        
        groupEndpoints.MapPost("/search", async (CustomerSearchDto customerSearchDto, IMediator mediator, CancellationToken ct) =>
        {
            
        }).WithName("SearchCustomer").IncludeInOpenApi();
        
        
        groupEndpoints.MapGet("/{id:guid}", async (Guid id, IMediator mediator, CancellationToken ct) =>
        {
            
        }).WithName("GetCustomer").IncludeInOpenApi();
        
    }
    
}