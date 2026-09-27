using System.Globalization;
using Amrod.Infrastructure.Persistence;
using CsvHelper;
using CsvHelper.Configuration;

namespace Amrod.Application.Utils;

public static class DbInitializer
{
    public static async Task SeedCsvDataAsync(ApplicationDbContext context)
    {
        var csvFilePath = $"{Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\"))}\\MOCK_DATA.csv";
        var csvPath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "products.csv");
        
        // 1. Ensure the database exists or is migrated
        await context.Database.EnsureCreatedAsync();

        // 2. Idempotency Check: Don't seed if data already exists
        if (await context.Products.AnyAsync())
        {
            return; 
        }

        // 3. Configure and read the CSV
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            MissingFieldFound = null // Ignores missing fields instead of throwing errors
        };

        using var reader = new StreamReader(csvFilePath);
        using var csv = new CsvReader(reader, config);

        // Map CSV headers automatically to Product properties
        var records = csv.GetRecords<Product>();

        // 4. Batch insert into database
        await context.Products.AddRangeAsync(records);
        await context.SaveChangesAsync();
    }

}