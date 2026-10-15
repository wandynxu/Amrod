using Amrod.Application.Features.Customers.Commands.Create;
using Amrod.Application.Features.Customers.Commands.Search;
using Amrod.Application.Features.Customers.Queries.GetById;
using Amrod.Application.Features.DTOs;
using Carter;
using Carter.OpenApi;
using FluentValidation;
using MediatR;


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
        
        
        groupEndpoints.MapGet("", async ([AsParameters]SearchDto searchDto, IMediator mediator, CancellationToken ct) =>
        {
            var customers = await mediator.Send(new SearchCustomersCommand
            {
                Search = searchDto.Search,
                Page = searchDto.Page,
                PageSize = searchDto.PageSize,
                Sort = searchDto.Sort,
                OrderBy = searchDto.OrderBy
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