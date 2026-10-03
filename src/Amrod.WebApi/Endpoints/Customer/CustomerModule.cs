using Amrod.Application.Features.Customers.Commands.Create;
using Amrod.Application.Features.Customers.Dtos;
using Amrod.Application.Features.Customers.Queries.GetById;
using Amrod.Application.Features.Customers.Queries.Search;
using Carter;
using Carter.OpenApi;
using FluentValidation;
using MediatR;

namespace Amrod.WebApi.Endpoints.Customer;

public class CustomerModule: ICarterModule
{
    
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var groupEndpoints = app.MapGroup("api/customers").WithTags("Customers");

        groupEndpoints.MapPost("/create", async (CustomerDto customerDto, IValidator<CustomerDto> validator, IMediator mediator, CancellationToken ct) =>
        {
            var validationResult = await validator.ValidateAsync(customerDto,ct);
            if (!validationResult.IsValid)
            { 
                var errors = validationResult.Errors
                    .ToDictionary(e => e.PropertyName, e => e.ErrorMessage);
                return Results.BadRequest(errors);
            }
            
            var result = await mediator.Send(new CreateCustomerCommand
            {
                Name = customerDto.Name, 
                Email = customerDto.Email, 
                CountryCode = customerDto.CountryCode
            },ct);
            
            return Results.Ok(result);
        }).WithName("CreateCustomer").IncludeInOpenApi();
        
        
        groupEndpoints.MapGet("", async ([AsParameters]CustomerSearchDto customerSearchDto, IMediator mediator, CancellationToken ct) =>
        {
            var customers = await mediator.Send(new SearchCustomerCommand
            {
                Search = customerSearchDto.Search,
                Page = customerSearchDto.Page,
                PageSize = customerSearchDto.PageSize,
                Sort = customerSearchDto.Sort
            }, ct);
            
            return Results.Ok(customers);
        }).WithName("SearchCustomer").IncludeInOpenApi();
        
        
        groupEndpoints.MapGet("/{id:guid}", async (Guid id, IMediator mediator, CancellationToken ct) =>
        {
            var customer = await mediator.Send(new GetCustomerByIdQuery
            {
                Id = id
            }, ct);
            
            return Results.Ok(customer);
        }).WithName("GetCustomer").IncludeInOpenApi();
        
    }
    
}