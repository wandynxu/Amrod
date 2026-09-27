using Amrod.Application.Features.Customers.Commands.Create;
using Amrod.Application.Features.Customers.Commands.Search;
using Carter;

namespace Amrod.WebApi.Endpoints.Customer;

public class CustomerEndpoints: ICarterModule
{
    
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var groupEndpoints = app.MapGroup("api/customers");

        groupEndpoints.MapPost("/create", async (CreateCustomerCommand command, CancellationToken ct) =>
        {
            Create(command);
        });
        
        groupEndpoints.MapPost("/search", async (SearchCustomerCommand command,CancellationToken ct) =>
        {
            Search(command);
        });
        
        groupEndpoints.MapGet("/all", async (CancellationToken ct) =>
        {
            GetAll();
        });
        
        groupEndpoints.MapGet("/{id:guid}", async (Guid id, CancellationToken ct) =>
        {
            GetById(id);
        });
        
    }
    
    private static IResult Create(CreateCustomerCommand command)
    {
        
        return Results.Ok();
    }
    
    private static IResult Search(SearchCustomerCommand command)
    {
        return Results.Ok();
    }
    
    private static IResult GetAll()
    {
        return Results.Ok();
    }
    
    private static IResult GetById(Guid id)
    {
        return Results.Ok();
    }

    
    
    

    
}