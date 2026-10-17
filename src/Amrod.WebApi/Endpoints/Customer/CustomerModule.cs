using Amrod.Application.Features.Customers.Commands.Create;
using Amrod.Application.Features.Customers.Queries.GetCustomerById;
using Amrod.Application.Features.Customers.Queries.GetCustomers;
using Amrod.Application.Features.DTOs;
using Carter;
using Carter.OpenApi;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Amrod.WebApi.Endpoints.Customer;

public class CustomerModule: ICarterModule
{
    
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var groupEndpoints = app.MapGroup("api/customers").WithTags("Customers");

        groupEndpoints.MapPost("/create", async Task<Results<Ok<bool>, UnprocessableEntity<Dictionary<string,string>>>> (CustomerDto customerDto, IValidator<CustomerDto> validator, IMediator mediator, CancellationToken ct) =>
        {
            var validationResult = await validator.ValidateAsync(customerDto,ct);
            if (!validationResult.IsValid)
            { 
                var errors = validationResult.Errors
                    .ToDictionary(e => e.PropertyName, e => e.ErrorMessage);
                return TypedResults.UnprocessableEntity(errors);
            }
            
            var result = await mediator.Send(new CreateCustomerCommand
            {
                Name = customerDto.Name, 
                Email = customerDto.Email, 
                CountryCode = customerDto.CountryCode
            },ct);
            
            
            return TypedResults.Ok(result);
        }).WithName("CreateCustomer").IncludeInOpenApi();
        
        
        groupEndpoints.MapGet("", async ([AsParameters]SearchDto searchDto, IMediator mediator, CancellationToken ct) =>
        {
            var customers = await mediator.Send(new GetCustomersQuery
            {
                Search = searchDto.Search,
                Page = searchDto.Page,
                PageSize = searchDto.PageSize,
                Sort = searchDto.Sort,
                OrderBy = searchDto.OrderBy
            }, ct);

            return TypedResults.Ok(customers);
        }).WithName("SearchCustomer").IncludeInOpenApi();
        
        
        groupEndpoints.MapGet("/{id:guid}", async Task<Results<Ok<CustomerDto>, NotFound>>(Guid id, IMediator mediator, CancellationToken ct) =>
        {
            var customer = await mediator.Send(new GetCustomerByIdQuery
            {
                Id = id
            }, ct);
            
            return customer is not null ? TypedResults.Ok(customer) : TypedResults.NotFound();
        }).WithName("GetCustomer").IncludeInOpenApi();
        
    }
    
}