using Amrod.Domain.Entities;
using Bogus;

namespace Amrod.Application.Utils.SeedData;

public static class OrderLineItemFaker
{
    public static Faker<OrderLineItem> Generate()
    {
        
        var orderLineItemFaker = new Faker<OrderLineItem>()
            .RuleFor(x => x.Id, f => f.Random.Guid())
            .RuleFor(x => x.ProductSku, f => f.Random.Replace("???-#####-??"))                
            .RuleFor(x => x.Quantity, f => f.Random.Number(1, 100))
            .RuleFor(x => x.UnitPrice, f => f.Finance.Amount(1000, 1000000));
        
        return orderLineItemFaker;
    }
}