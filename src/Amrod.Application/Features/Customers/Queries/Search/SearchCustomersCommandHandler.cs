using System.Data.Entity;
using Amrod.Application.Features.Customers.Dtos;
using Amrod.Application.Mappings;
using Amrod.Domain.Entities;
using Amrod.Infrastructure.Models;
using Amrod.Infrastructure.Persistence;
using MediatR;

namespace Amrod.Application.Features.Customers.Queries.Search;

public sealed class SearchCustomersCommandHandler(IUnitOfWork unitOfWork): IRequestHandler<SearchCustomerCommand, IReadOnlyList<CustomerDto>>
{
    public async Task<IReadOnlyList<CustomerDto>> Handle(SearchCustomerCommand request, CancellationToken cancellationToken)
    {
        var searchRequest = new SearchRequest
        {
            SearchTerms = !string.IsNullOrWhiteSpace(request.Search) ? [request.Search] :  [],
            Page = request.Page ?? 1,
            PageSize = request.PageSize ?? 100,
            Sort = request.Sort ?? "asc",
            Columns = request.Columns
        };
        
        var customers =  await unitOfWork.GetRepository<Customer>().SearchEntity(searchRequest).ToListAsync(cancellationToken);
        return customers.ToCustomerDtoList().Count > 0 ? customers.ToCustomerDtoList() : Enumerable.Empty<CustomerDto>().ToList();
    }
}