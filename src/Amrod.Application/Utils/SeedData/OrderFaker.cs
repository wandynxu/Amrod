using Amrod.Application.Common;
using Amrod.Domain.Common.Enums;
using Amrod.Domain.Entities;
using Bogus;

namespace Amrod.Application.Utils.SeedData;

public static class OrderFaker
{
    public static List<Order> Generate(List<Customer> customers, Faker<OrderLineItem> orderLineItemFaker)
    {
        
        var orderFaker = new Faker<Order>()
            .RuleFor(x => x.Id, f => f.Random.Guid())
            .RuleFor(x => x.Customer, f => f.PickRandom(customers))
            .RuleFor(x => x.Status, _ => OrderStatus.Pending);
        
        var orders = orderFaker.Generate(200);
        
        foreach (var order in orders)
        {
            var countryCode= order.Customer.CountryCode;
            var currencyCode = CountryCurrencyCodes.CountryCurrencyCodesMap.FirstOrDefault(c => c.CountryCode == countryCode).CurrencyCodes;
            if (currencyCode is not null)
            {
                order.CurrencyCode = currencyCode[Random.Shared.Next(currencyCode.Length)];
                order.LineItems = orderLineItemFaker.Generate(Random.Shared.Next(10, 100));
            }
        }
        
        return orders;
    }

}