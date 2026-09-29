namespace Amrod.Application.Features.Customers.Dtos;

public record CustomerDto
{
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string CountryCode { get; init; } 
}