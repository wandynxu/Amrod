using System.Data.Entity;
using Amrod.Application.Features.DTOs;
using Amrod.Application.Mappings;
using Amrod.Domain.Entities;
using Amrod.Infrastructure.Models;
using Amrod.Infrastructure.Persistence;
using MediatR;

namespace Amrod.Application.Features.Customers.Commands.Search;

public sealed class SearchCustomersCommandHandler(IRepository<Customer> repository) : IRequestHandler<SearchCustomersCommand, IReadOnlyList<CustomerDto>>
{
    
    public async Task<IReadOnlyList<CustomerDto>> Handle(SearchCustomersCommand request, CancellationToken ct)
    {
        var searchRequest = new SearchRequest
        {
            SearchTerms = !string.IsNullOrWhiteSpace(request.Search) ? [request.Search] : [],
            Page = request.Page ?? 1,
            PageSize = request.PageSize ?? 100,
            Sort = request.Sort ?? "asc",
            OrderBy = !string.IsNullOrWhiteSpace(request.OrderBy) ? request.OrderBy.Split(',') : [],
            Columns = request.Columns
        };
        
        var customers = await repository.SearchEntity(searchRequest).ToListAsync(ct);
        
        return customers.ToCustomerDtoList().Count > 0
            ? customers.ToCustomerDtoList()
            : Enumerable.Empty<CustomerDto>().ToList();    

    }
    
}