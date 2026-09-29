using Amrod.Application.Common;
using Amrod.Domain.Entities;
using Bogus;

namespace Amrod.Application.Utils.SeedData;

public static class CustomerFaker
{
    public static List<Customer> Generate()
    {
        var customerFaker = new Faker<Customer>()
            .RuleFor(x => x.Id, f => f.Random.Guid())
            .RuleFor(x => x.Name, f => f.Name.FullName())
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.CountryCode, (f, c) =>
            {
                var countryCodes =
                    CountryCurrencyCodes.CountryCurrencyCodesMap.Select(x => x.CountryCode).ToList();
                    
                return f.PickRandom(countryCodes);
            });
        var customers = customerFaker.Generate(50);
        
        return customers;
    }
}