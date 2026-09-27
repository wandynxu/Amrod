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
            await Create(command,ct);
        });
        
        groupEndpoints.MapPost("/search", async (SearchCustomerCommand command,CancellationToken ct) =>
        {
            await Search(command,ct);
        });
        
        groupEndpoints.MapGet("/{id:guid}", async (Guid id, CancellationToken ct) =>
        {
            await GetById(id, ct);
        });
        
    }
    
    private static async Task<IResult> Create(CreateCustomerCommand command, CancellationToken ct)
    {
        
        return Results.Ok();
    }
    
    private static async Task<IResult> Search(SearchCustomerCommand command, CancellationToken ct)
    {
        return Results.Ok();
    }
    
    private static async Task<IResult> GetById(Guid id, CancellationToken ct)
    {
        return Results.Ok();
    }

    
    
    

    
}