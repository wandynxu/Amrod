using Amrod.Application.Features.Customers.Dtos;
using Amrod.Domain.Entities;

namespace Amrod.Application.Mappings;

public static class CustomerMappingExtensions
{
    public static Customer ToCustomer(this CustomerDto customerDto)
    {
        return new Customer
        {
            Name = customerDto.Name,
            Email = customerDto.Email,
            CountryCode = customerDto.CountryCode
        };
    }
    
    public static CustomerDto ToCustomerDto(this Customer customer)
    {
        return new CustomerDto
        {
            Name = customer.Name,
            Email = customer.Email,
            CountryCode = customer.CountryCode
        };
    }
    
    public static List<CustomerDto> ToCustomerDtoList(this IEnumerable<Customer> customers)
    {
        return [.. customers.Select(c => c.ToCustomerDto())];
    }
}